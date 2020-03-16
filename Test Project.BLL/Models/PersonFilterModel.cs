using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Test_Project.BLL.Enums;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.BLL.Models
{
    public class PersonFilterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GenderName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? CityId { get; set; }
        public string MobileNumber { get; set; }
        public string NumberTypeName { get; set; }
        public int numberOfObjectsPerPage { get; set; }
        public int pageNumber { get; set; }
    }
}
