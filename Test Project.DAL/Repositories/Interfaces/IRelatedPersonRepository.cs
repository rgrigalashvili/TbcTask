using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test_Project.DAL.Database.Entities;

namespace Test_Project.DAL.Repositories.Interfaces
{
    public interface IRelatedPersonRepository : IGenericRepository<RelatedPerson>
    {
        Task<bool> ExistsRelation(int personId, int relatedPersonId);
    }
}
