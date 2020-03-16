using AutoMapper;
using System.Threading.Tasks;
using Test_Project.BLL.Managers.Interfaces;
using Test_Project.BLL.Models;
using Test_Project.DAL.Database.Entities;
using Test_Project.DAL.UnitOfWork.Interface;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.BLL.Managers
{
    public class RelatedPersonManager : IRelatedPersonManager
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public RelatedPersonManager(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<OperationOutcome> AddRelatedPersonAsync(RelatedPersonModel model)
        {
            if (model.PersonId.GetValueOrDefault() == 0)
            {
                return new OperationOutcome(false, GlobalResource.PersonIdIsNotDeclared);
            }

            if (model.RelatedpersonId == model.PersonId.GetValueOrDefault())
            {
                return new OperationOutcome(false, GlobalResource.RelatedPersonCanNotBeSamePerson);
            }
            if (await unitOfWork.RelatedRepository.ExistsRelation(model.PersonId.GetValueOrDefault(), model.RelatedpersonId))
            {
                return new OperationOutcome(false, GlobalResource.RelationHaveAlreadyExists);
            }
            var relatedPersonDTO = mapper.Map<RelatedPersonModel, RelatedPerson>(model);

            await unitOfWork.RelatedRepository.AddAsync(relatedPersonDTO);

            return await unitOfWork.CompleteAsync();
        }
        public async Task<OperationOutcome> RemoveRelatedPersonAsync(int Id)
        {
            if (await unitOfWork.RelatedRepository.Exists(Id))
            {
                await unitOfWork.RelatedRepository.RemoveAsync(Id);
                return await unitOfWork.CompleteAsync();
            }
            return new OperationOutcome(false, GlobalResource.ObjectDoesnotExists);
        }
    }
}
