using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Test_Project.DAL.Database.Entities
{
    public class MobileNumber : EntityBase
    {
        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public string Number { get; set; }
        public int NumberTypeId { get; set; }
    }
}
