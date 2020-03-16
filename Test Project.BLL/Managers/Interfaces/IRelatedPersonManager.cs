using System.Threading.Tasks;
using Test_Project.BLL.Models;
using Test_Project.Shared.Models;

namespace Test_Project.BLL.Managers.Interfaces
{
    public interface IRelatedPersonManager
    {
        Task<OperationOutcome> AddRelatedPersonAsync(RelatedPersonModel model);
        Task<OperationOutcome> RemoveRelatedPersonAsync(int Id);
    }
}
