using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Project.BLL.Managers.Interfaces;
using Test_Project.BLL.Managers.Settings;
using Test_Project.BLL.Models;
using Test_Project.DAL.Database.Entities;
using Test_Project.DAL.Models;
using Test_Project.DAL.UnitOfWork.Interface;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.BLL.Managers
{
    public class PersonManager : IPersonManager
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ApiSettings apiSettings;

        public PersonManager(IMapper mapper, IUnitOfWork unitOfWork, ApiSettings apiSettings)
        {
            this.mapper = mapper;
            this.apiSettings = apiSettings;
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationOutcome> AddPersonAsync(PersonModel model)
        {
            var personDTO = mapper.Map<PersonModel, Person>(model);

            if (model.CityId != null)
            {
                if (!(await unitOfWork.CityRepository.Exists(model.CityId.GetValueOrDefault())))
                {
                    return new OperationOutcome(false, GlobalResource.CityDoesnotExists);
                }
            }
            if (await unitOfWork.PersonRepository.ExistsPrivateNumber(model.PrivateNumber))
            {
                return new OperationOutcome(false, GlobalResource.ObjectAlreadyExists);
            }
            if (model.RelatedPersons != null)
            {
                foreach (var item in model.RelatedPersons)
                {
                    if (!(await unitOfWork.PersonRepository.Exists(item.RelatedpersonId)))
                    {
                        return new OperationOutcome(false, GlobalResource.RelatedPersonDoesnotExists);
                    }
                }
            }

            await unitOfWork.PersonRepository.AddAsync(personDTO);
            await unitOfWork.CompleteAsync();

            if (model.RelatedPersons != null && model.RelatedPersons.Count() > 0)
            {
                var personId = await unitOfWork.PersonRepository.GetPersonByPrivateNumberAsync(model.PrivateNumber);

                var relatedPersons = model.RelatedPersons.Select(x => new RelatedPersonModel { PersonId = personId, RelatedpersonId = x.RelatedpersonId, RelationTypeId = x.RelationTypeId }).ToList();

                var relatedPersonsDTO = mapper.Map<List<RelatedPersonModel>, List<RelatedPerson>>(relatedPersons);

                await unitOfWork.RelatedRepository.AddRangeAsync(relatedPersonsDTO);
            }
            return await unitOfWork.CompleteAsync();
        }
        public async Task<PersonViewModel> GetPersonAsync(int Id)
        {
            var person = await unitOfWork.PersonRepository.GetPersonInfoByIdAsync(Id);

            person.Image = person.ImageName != null ? apiSettings.BaseUrl + "/Images" + person.ImageName : "";

            if (person.RelatedPersonsInfo != null)
            {
                foreach (var item in person.RelatedPersonsInfo)
                {

                    item.Image = item.ImageName != null ? apiSettings.BaseUrl + "/Images" + person.ImageName : "";
                }
            }
            return mapper.Map<Person, PersonViewModel>(person);
        }
        public async Task<OperationOutcome> UpdatePerson(PersonModel model)
        {
            var personDTO = mapper.Map<PersonModel, Person>(model);

            await unitOfWork.PersonRepository.Update(personDTO);

            if (model?.MobileNumbers?.Count > 0)
            {
                await unitOfWork.MobileNumberRepository.AddRangeAsync(personDTO.MobileNumbers);

                await unitOfWork.MobileNumberRepository.RemoveRangeAsync(model.Id);
            }
            return await unitOfWork.CompleteAsync();
        }
        public async Task<OperationOutcome> RemovePersonAsync(int Id)
        {
            if (await unitOfWork.PersonRepository.Exists(Id))
            {
                if (await unitOfWork.MobileNumberRepository.ExistsByPersonId(Id))
                {
                    await unitOfWork.MobileNumberRepository.RemoveRangeAsync(Id);
                }
                await unitOfWork.PersonRepository.RemoveAsync(Id);
                return await unitOfWork.CompleteAsync();
            }
            return new OperationOutcome(false, GlobalResource.ObjectDoesnotExists);
        }
        public async Task<OperationOutcome> UploadImageAsync(int personId, string imageName)
        {
            var personData = await unitOfWork.PersonRepository.GetByIdAsync(personId);

            if (personData?.ImageName != null)
            {
                var filePath = "../Test Project.API/Images/" + personData?.ImageName;

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            await unitOfWork.PersonRepository.UploadImageAsync(personId, imageName);

            return await unitOfWork.CompleteAsync();
        }
        public async Task<IEnumerable<RelatedPersonCountModel>> GetPersonWithRelatedPersonCount()
        {
            var reports = await Task.FromResult(unitOfWork.PersonRepository.GetPersonWithRelatedPersonCount());

            return mapper.Map<IEnumerable<RelatedPersonCount>, IEnumerable<RelatedPersonCountModel>>(reports);
        }
        public async Task<List<PersonModel>> GetPersonByModel(PersonFilterModel model)
        {
            var personDTO = mapper.Map<PersonFilterModel, PersonFilter>(model);

            var persons = await Task.FromResult(unitOfWork.PersonRepository.GetPersonByModel(personDTO).Skip(personDTO.numberOfObjectsPerPage * personDTO.pageNumber).Take(personDTO.numberOfObjectsPerPage));

            return mapper.Map<List<Person>, List<PersonModel>>(persons.ToList());
        }
    }
}
