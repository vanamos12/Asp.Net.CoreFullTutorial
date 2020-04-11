using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeCRUDApp.Models
{
    public interface IEmployeeRepository
    {
        EmployeeModel GetEmployee(int Id);
        IEnumerable<EmployeeModel> GetAllEmployees();
        EmployeeModel Add(EmployeeModel employee);
        EmployeeModel Update(EmployeeModel employeeChanges);
        EmployeeModel Delete(int Id);
    }
}
