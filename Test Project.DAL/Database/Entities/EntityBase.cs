using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Project.DAL.Database.Entities
{
    public class EntityBase<T>
    {
        public T Id { get; set; }

        public DateTime DateCreated
        {
            get { return dateCreated ?? DateTime.Now; }
            set { dateCreated = value; }
        }

        private DateTime? dateCreated = null;

        public DateTime? DateChanged { get; set; }
        public DateTime? DateDelated { get; set; }
    }
    public class EntityBase : EntityBase<int>
    {
    }
}
