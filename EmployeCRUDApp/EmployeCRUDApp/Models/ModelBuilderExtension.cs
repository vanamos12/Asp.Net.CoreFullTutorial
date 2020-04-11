using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeCRUDApp.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>().HasData(
                    new EmployeeModel
                    {
                        Id = 1,
                        Name = "Mary",
                        Departement = Dept.IT,
                        Email = "mary@pragimtech.com"
                    },
                    new EmployeeModel
                    {
                        Id = 2,
                        Name = "John",
                        Departement = Dept.HR,
                        Email = "john@pragimtech.com"
                    },
                    new EmployeeModel
                    {
                        Id = 3,
                        Name = "vandam",
                        Departement = Dept.Payroll,
                        Email = "vandam@pragimtech.com"
                    }
                );
        }
    }
}
