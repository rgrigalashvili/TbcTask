using System;
using System.Collections.Generic;
using System.Text;
using Test_Project.Shared.Models;

namespace Test_Project.BLL.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GenderName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? CityId { get; set; }
        public ICollection<MobileNumberModel> MobileNumbers { get; set; }
        public string ImageName { get; set; }
        public string Image { get; set; }
        public IEnumerable<PersonViewModel> RelatedPersonsInfo { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateChanged { get; set; }
        public DateTime? DateDelated { get; set; }
    }
}
