using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Project.DAL.Database.Entities;
using Test_Project.DAL.Repositories.Interfaces;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.DAL.Repositories
{
    public class MobileNumberRepository : GenericRepository<MobileNumber>, IMobileNumberRepository
    {
        public MobileNumberRepository(TestProjectDataContext context) : base(context)
        {
        }
        public virtual Task<OperationOutcome> RemoveRangeAsync(int PersonId)
        {
            context.MobileNumbers.RemoveRange(context.MobileNumbers.Where(x => x.PersonId == PersonId));

            return  Task.FromResult(new OperationOutcome(true, GlobalResource.Success));
        }
        public Task<bool> ExistsByPersonId(int PersonId)
        {
            return  Task.FromResult(context.MobileNumbers.Any(x => x.PersonId == PersonId));
        }
    }
}
