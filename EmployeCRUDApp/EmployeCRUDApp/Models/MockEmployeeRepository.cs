using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeCRUDApp.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        public List<EmployeeModel> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<EmployeeModel>()
            {
                new EmployeeModel(){ Id=1, Departement=Dept.IT, Name="Steve", Email="Steve@gmail.com" },
                new EmployeeModel(){ Id=2, Departement=Dept.IT, Name="Edmond", Email="Edmond@gmail.com" },
                new EmployeeModel(){ Id=3, Departement=Dept.Payroll, Name="Jeannot", Email="Jeannot@gmail.com" },
            };
        }

        public EmployeeModel Add(EmployeeModel employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public EmployeeModel Delete(int Id)
        {
            EmployeeModel employee = _employeeList.FirstOrDefault(e => e.Id == Id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public EmployeeModel Update(EmployeeModel employeeChanges)
        {
            EmployeeModel employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                //_employeeList.Remove(employee);
                //_employeeList.Add(employeeChanges);
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Departement = employeeChanges.Departement;
            }
            return employeeChanges;
            //return employee;
        }

        IEnumerable<EmployeeModel> IEmployeeRepository.GetAllEmployees()
        {
            return _employeeList;
        }

        EmployeeModel IEmployeeRepository.GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
