using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test_Project.DAL.Database.Entities
{
    public class RelatedPerson : EntityBase
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int RelatedpersonId { get; set; }
        public Person Relatedperson { get; set; }
        public int RelationTypeId { get; set; }
    }
}
