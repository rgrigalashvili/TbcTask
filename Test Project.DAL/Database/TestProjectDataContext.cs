using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Project.DAL.Database.Entities
{
    public class TestProjectDataContext: DbContext
    {
        public TestProjectDataContext(DbContextOptions<TestProjectDataContext> options): base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<MobileNumber> MobileNumbers { get; set; }
        public DbSet<RelatedPerson> RelatedPersons { get; set; }
        public DbSet<City> Cities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelatedPerson>()
                .HasOne(s => s.Relatedperson)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
