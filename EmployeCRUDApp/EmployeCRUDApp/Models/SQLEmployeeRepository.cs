using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeCRUDApp.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public EmployeeModel Add(EmployeeModel employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public EmployeeModel Delete(int Id)
        {
            EmployeeModel employee = context.Employees.Find(Id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<EmployeeModel> GetAllEmployees()
        {
            return context.Employees;
        }

        public EmployeeModel GetEmployee(int Id)
        {
            return context.Employees.Find(Id);
        }

        public EmployeeModel Update(EmployeeModel employeeChanges)
        {
            var employee = context.Employees.Attach(employeeChanges);
            // We need a verification of the value of employee before saving his values.
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeeChanges;
        }
    }
}
