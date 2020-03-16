using System;
using System.Collections.Generic;
using System.Text;
using Test_Project.BLL.Enums;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.BLL.Models
{
    public class RelatedPersonModel : BaseModel
    {
        public int? PersonId { get; set; }
        public int RelatedpersonId { get; set; }
        public PersonModel Relatedperson { get; set; }
        public int? RelationTypeId { get; set; }
        public override OperationOutcome Validate()
        {
            if (RelationTypeId == null || !Enum.IsDefined(typeof(RelationTypeEnum), RelationTypeId))
            {
                return new OperationOutcome(false, GlobalResource.RelationTypeNameIsNotCorrect);
            }
            return new OperationOutcome(true, GlobalResource.Success);
        }
    }
}
