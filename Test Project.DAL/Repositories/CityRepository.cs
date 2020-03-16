using System;
using System.Collections.Generic;
using System.Text;
using Test_Project.DAL.Database.Entities;
using Test_Project.DAL.Repositories.Interfaces;

namespace Test_Project.DAL.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(TestProjectDataContext context) : base(context)
        {

        }
    }
}
