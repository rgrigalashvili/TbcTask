using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Project.DAL.Database.Entities;
using Test_Project.DAL.Models;
using Test_Project.Shared.Models;

namespace Test_Project.DAL.Repositories.Interfaces
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<OperationOutcome> UploadImageAsync(int personId, string imageName);
        IEnumerable<RelatedPersonCount> GetPersonWithRelatedPersonCount();
        Task<int> GetPersonByPrivateNumberAsync(string privateNumber);
        IQueryable<Person> GetPersonByModel(PersonFilter filter);
        Task<bool> ExistsPrivateNumber(string privateNumber);
        Task<Person> GetPersonInfoByIdAsync(int id);
    }
}
