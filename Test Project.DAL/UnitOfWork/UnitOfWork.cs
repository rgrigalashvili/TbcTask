using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test_Project.DAL.Database.Entities;
using Test_Project.DAL.Repositories.Interfaces;
using Test_Project.DAL.UnitOfWork.Interface;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.DAL.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private TestProjectDataContext context;
        private bool disposed = false;

        private IPersonRepository personRepository;
        private IRelatedPersonRepository relatedRepository;
        private IMobileNumberRepository mobileNumberRepository;
        private ICityRepository cityRepository;
        public UnitOfWork(TestProjectDataContext context)
        {
            this.context = context;
        }
        public IPersonRepository PersonRepository
        {
            get { return personRepository = personRepository ?? new PersonRepository(context); }
        }
        public IRelatedPersonRepository RelatedRepository
        {
            get { return relatedRepository = relatedRepository ?? new RelatedPersonRepository(context); }
        }
        public IMobileNumberRepository MobileNumberRepository
        {
            get { return mobileNumberRepository = mobileNumberRepository ?? new MobileNumberRepository(context); }
        }
        public ICityRepository CityRepository
        {
            get { return cityRepository = cityRepository ?? new CityRepository(context); }
        }
        public void Complete()
        {
            context.SaveChanges();
        }
        public async Task<OperationOutcome> CompleteAsync()
        {
            await context.SaveChangesAsync();
            return new OperationOutcome(true, GlobalResource.Success);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
    }
}
