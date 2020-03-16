using System;
using System.Collections.Generic;
using System.Text;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.BLL.Models
{
    public class CityModel : BaseModel
    {
        public string Name { get; set; }

        public override OperationOutcome Validate()
        {
            return new OperationOutcome(true, GlobalResource.Success);
        }
    }
}
