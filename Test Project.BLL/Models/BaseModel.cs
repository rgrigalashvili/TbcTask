using System;
using System.Collections.Generic;
using System.Text;
using Test_Project.Shared.Models;

namespace Test_Project.BLL.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public DateTime? DateCreated
        {
            get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }
        }

        private DateTime? dateCreated = null;
        public DateTime? DateChanged { get; set; }
        public DateTime? DateDelated { get; set; }
        public abstract OperationOutcome Validate();
    }
}
