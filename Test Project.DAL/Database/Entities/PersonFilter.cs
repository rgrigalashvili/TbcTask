using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Project.DAL.Database.Entities
{
    public class PersonFilter: EntityBase
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
