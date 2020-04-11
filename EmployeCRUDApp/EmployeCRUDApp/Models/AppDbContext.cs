using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeCRUDApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
        }

        public DbSet<EmployeeModel> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            /*modelBuilder.Entity<EmployeeModel>().HasData(
                    new EmployeeModel
                    {
                        Id = 1,
                        Name="Mary",
                        Departement = Dept.IT,
                        Email = "mary@pragimtech.com"
                    },
                    new EmployeeModel
                    {
                        Id = 2,
                        Name = "John",
                        Departement = Dept.HR,
                        Email = "john@pragimtech.com"
                    }
                );*/
            modelBuilder.Seed();
        }
    }
}
