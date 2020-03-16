using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test_Project.BLL.Models;
using Test_Project.Shared.Models;

namespace Test_Project.BLL.Managers.Interfaces
{
    public interface IPersonManager
    {
        Task<OperationOutcome> AddPersonAsync(PersonModel model);
        Task<PersonViewModel> GetPersonAsync(int Id);
        Task<OperationOutcome> UpdatePerson(PersonModel model);
        Task<OperationOutcome> RemovePersonAsync(int Id);
        Task<OperationOutcome> UploadImageAsync(int personId, string imageName);
        Task<IEnumerable<RelatedPersonCountModel>> GetPersonWithRelatedPersonCount();
        Task<List<PersonModel>> GetPersonByModel(PersonFilterModel filter);
    }
}
