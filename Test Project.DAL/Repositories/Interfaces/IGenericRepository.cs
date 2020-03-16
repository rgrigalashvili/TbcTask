using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test_Project.DAL.Database.Entities;
using Test_Project.Shared.Models;

namespace Test_Project.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetByIdAsync(int Id);
        Task<OperationOutcome> AddAsync(TEntity entity);
        Task<OperationOutcome> Update(TEntity entity);
        Task<OperationOutcome> RemoveAsync(int Id);
        Task<OperationOutcome> AddRangeAsync(ICollection<TEntity> entity);
        Task<bool> Exists(int Id);
    }
}
