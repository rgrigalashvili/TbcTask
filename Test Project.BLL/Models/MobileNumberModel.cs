using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using Test_Project.BLL.Enums;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.BLL.Models
{
    public class MobileNumberModel : BaseModel
    {
        public int? PersonId { get; set; }
        public string Number { get; set; }
        public int NumberTypeId { get; set; }
        public override OperationOutcome Validate()
        {
            if (string.IsNullOrEmpty(Number) || Number.Length < 4 || Number.Length > 50 || !Regex.IsMatch(Number, ("^[0-9]+$")))
            {
                return new OperationOutcome(false, GlobalResource.MobileNumberIsNotCorrect);
            }
            if (string.IsNullOrEmpty(Number) || !Enum.IsDefined(typeof(MobileNumberTypeEnum), NumberTypeId))
            {
                return new OperationOutcome(false, GlobalResource.MobileNumberTypeIsNotCorrect);
            }
            return new OperationOutcome(true, GlobalResource.Success);
        }
    }
}
