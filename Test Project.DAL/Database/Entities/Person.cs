using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test_Project.DAL.Database.Entities
{
    public class Person : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GenderName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? CityId { get; set; }
        public City City { get; set; }
        public ICollection<MobileNumber> MobileNumbers { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public string Image { get; set; }
        [NotMapped]
        public IEnumerable<Person> RelatedPersonsInfo { get; set; }
    }
}
