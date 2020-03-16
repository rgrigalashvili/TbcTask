using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Project.DAL.Models
{
    public class RelatedPersonCount
    {
        public int PersonId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PrivateNumber { get; set; }

        public List<GroupRelatedPersonByRelationType> RelatedPersons { get; set; }
    }
}
