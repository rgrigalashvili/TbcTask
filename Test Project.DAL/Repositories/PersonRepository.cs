using System.Collections.Generic;
using System.Threading.Tasks;
using Test_Project.DAL.Database.Entities;
using Test_Project.DAL.Repositories.Interfaces;
using System.Linq;
using Test_Project.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Test_Project.Shared.Models;
using System.IO;
using System;
using Test_Project.Shared.Resources;

namespace Test_Project.DAL.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(TestProjectDataContext context) : base(context)
        {

        }
        public async Task<OperationOutcome> UploadImageAsync(int personId, string imageName)
        {
            var personEntity = await GetByIdAsync(personId);

            personEntity.ImageName = imageName;
            await Update(personEntity);

            return new OperationOutcome(true, GlobalResource.Success);
        }
        public IEnumerable<RelatedPersonCount> GetPersonWithRelatedPersonCount()
        {
            var relatedGroup = (from x in context.RelatedPersons
                                group x by new { x.PersonId, x.RelationTypeId }
                                into grouped
                                select new GroupRelatedPersonByRelationType
                                {
                                    PersonId = grouped.Key.PersonId,
                                    RelationTypeId = grouped.Key.RelationTypeId,
                                    Count = grouped.Count()
                                }).ToList();

            var personsWithRelationPersons = (from person in context.Persons
                                              select new RelatedPersonCount
                                              {
                                                  PersonId = person.Id,
                                                  PrivateNumber = person.PrivateNumber,
                                                  FirstName = person.FirstName,
                                                  LastName = person.LastName,
                                                  RelatedPersons = new List<GroupRelatedPersonByRelationType>()
                                              }).ToList();

            foreach (var item in personsWithRelationPersons)
            {
                var range = relatedGroup.Where(x => x.PersonId == item.PersonId);

                item.RelatedPersons.AddRange(range);
            }

            return personsWithRelationPersons;
        }
        public Task<Person> GetPersonInfoByIdAsync(int id)
        {
            var result = from person in context.Persons
                         where person.Id == id
                         select new Person
                         {
                             Id = person.Id,
                             PrivateNumber = person.PrivateNumber,
                             FirstName = person.FirstName,
                             LastName = person.LastName,
                             ImageName = person.ImageName,
                             DateOfBirth = person.DateOfBirth,
                             DateCreated = person.DateCreated,
                             CityId = person.CityId,
                             City = person.City,
                             GenderName = person.GenderName,
                             MobileNumbers = (from item in context.MobileNumbers
                                              where item.PersonId == id
                                              select new MobileNumber
                                              {
                                                  Id = item.Id,
                                                  PersonId = id,
                                                  Number = item.Number,
                                                  NumberTypeId = item.NumberTypeId
                                              }).ToList(),
                             RelatedPersonsInfo = (from related in context.RelatedPersons
                                                   where related.PersonId == id
                                                   select new Person
                                                   {
                                                       Id = related.Relatedperson.Id,
                                                       PrivateNumber = related.Relatedperson.PrivateNumber,
                                                       FirstName = related.Relatedperson.FirstName,
                                                       LastName = related.Relatedperson.LastName,
                                                       ImageName = related.Relatedperson.ImageName,
                                                       DateOfBirth = related.Relatedperson.DateOfBirth,
                                                       DateCreated = related.Relatedperson.DateCreated,
                                                       CityId = related.Relatedperson.CityId,
                                                       City = related.Relatedperson.City,
                                                       GenderName = related.Relatedperson.GenderName,
                                                       MobileNumbers = (from item in context.MobileNumbers
                                                                        where item.PersonId == id
                                                                        select new MobileNumber
                                                                        {
                                                                            Id = item.Id,
                                                                            PersonId = id,
                                                                            Number = item.Number,
                                                                            NumberTypeId = item.NumberTypeId
                                                                        }).ToList()
                                                   }).ToList()
                         };

            return result.FirstOrDefaultAsync();
        }
        public Task<int> GetPersonByPrivateNumberAsync(string privateNumber)
        {
            return context.Persons.Where(x => x.PrivateNumber == privateNumber).Select(p => p.Id).FirstOrDefaultAsync();
        }
        public IQueryable<Person> GetPersonByModel(PersonFilter filter)
        {
            var query = context.Persons.Select(x => x);

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                query = query.Where(x => x.FirstName.Contains(filter.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                query = query.Where(x => x.LastName.Contains(filter.LastName));
            }
            if (!string.IsNullOrWhiteSpace(filter.GenderName))
            {
                query = query.Where(x => x.GenderName.Contains(filter.GenderName));
            }
            if (!string.IsNullOrWhiteSpace(filter.PrivateNumber))
            {
                query = query.Where(x => x.PrivateNumber.Contains(filter.PrivateNumber));
            }
            if (filter.DateOfBirth != null)
            {
                query = query.Where(x => x.DateOfBirth.Value.Date == filter.DateOfBirth.Value.Date);
            }
            if (filter.CityId != null)
            {
                query = query.Where(x => x.CityId == filter.CityId);
            }
            if (!string.IsNullOrWhiteSpace(filter.MobileNumber))
            {
                query = query.Where(x => x.MobileNumbers.Any(y => y.Number.Contains(filter.MobileNumber)));
            }
            return query;
        }
        public Task<bool> ExistsPrivateNumber(string privateNumber)
        {
            return context.Persons.AnyAsync(x => x.PrivateNumber == privateNumber);
        }
    }
}
