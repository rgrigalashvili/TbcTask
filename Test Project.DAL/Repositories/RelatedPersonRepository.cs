using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test_Project.DAL.Database.Entities;
using Test_Project.DAL.Repositories.Interfaces;

namespace Test_Project.DAL.Repositories
{
    public class RelatedPersonRepository : GenericRepository<RelatedPerson>, IRelatedPersonRepository
    {
        public RelatedPersonRepository(TestProjectDataContext context) : base(context)
        {

        }
        public Task<bool> ExistsRelation(int personId, int relatedPersonId)
        {
            return context.RelatedPersons.AnyAsync(x => x.PersonId == personId && x.RelatedpersonId == relatedPersonId);
        }
    }
}
