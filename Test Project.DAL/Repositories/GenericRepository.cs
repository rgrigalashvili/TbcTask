using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test_Project.DAL.Database.Entities;
using Test_Project.DAL.Repositories.Interfaces;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        internal TestProjectDataContext context;
        internal DbSet<TEntity> dbSet;
        public GenericRepository(TestProjectDataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual async Task<TEntity> GetByIdAsync(int Id)
        {
            return await dbSet.FindAsync(Id);
        }
        public virtual async Task<OperationOutcome> AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            return new OperationOutcome(true, GlobalResource.Success);
        }
        public virtual Task<OperationOutcome> Update(TEntity entity)
        {
            dbSet.Update(entity);
            entity.DateChanged = DateTime.Now;

            return Task.FromResult(new OperationOutcome(true, GlobalResource.Success));
        }
        public virtual async Task<OperationOutcome> RemoveAsync(int Id)
        {
            var entity = await dbSet.FindAsync(Id);

            dbSet.Remove(entity);

            return new OperationOutcome(true, GlobalResource.Success);
        }
        public virtual Task<OperationOutcome> AddRangeAsync(ICollection<TEntity> entity)
        {
            dbSet.AddRange(entity);

            return Task.FromResult( new OperationOutcome(true, GlobalResource.Success));
        }
        public virtual async Task<bool> Exists(int Id)
        {
            return await dbSet.AnyAsync(x => x.Id == Id);
        }
    }
}
