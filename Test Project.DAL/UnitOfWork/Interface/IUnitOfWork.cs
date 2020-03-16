using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test_Project.DAL.Repositories.Interfaces;
using Test_Project.Shared.Models;

namespace Test_Project.DAL.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        IRelatedPersonRepository RelatedRepository { get; }
        IMobileNumberRepository MobileNumberRepository { get; }
        ICityRepository CityRepository { get; }
        Task<OperationOutcome> CompleteAsync();
        void Complete();
        void Dispose();
    }
}
