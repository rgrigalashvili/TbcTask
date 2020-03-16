using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Project.DAL.Models
{
    public class GroupRelatedPersonByRelationType
    {
        public int PersonId { get; set; }
        public int RelationTypeId { get; set; }
        public int Count { get; set; }
    }
}
