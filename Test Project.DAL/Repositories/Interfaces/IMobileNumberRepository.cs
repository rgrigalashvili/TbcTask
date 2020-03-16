using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test_Project.DAL.Database.Entities;
using Test_Project.Shared.Models;

namespace Test_Project.DAL.Repositories.Interfaces
{
    public interface IMobileNumberRepository : IGenericRepository<MobileNumber>
    {
        Task<OperationOutcome> RemoveRangeAsync(int PersonId);
        Task<bool> ExistsByPersonId(int PersonId);
    }
}
