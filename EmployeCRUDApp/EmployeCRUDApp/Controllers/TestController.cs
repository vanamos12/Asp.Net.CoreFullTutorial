using EmployeCRUDApp.Models;
using EmployeCRUDApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeCRUDApp.Controllers
{
    //[Route("Test")]
    public class TestController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public TestController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public string Index()
        {
            //return "hello from test controller.";
            return _employeeRepository.GetEmployee(1).Name;
            
        }

        /*public ObjectResult Details()
        {
            EmployeeModel employee = _employeeRepository.GetEmployee(1);
            return new ObjectResult(employee);
        }*/

        
        public ViewResult Details(int? id)
        {
            EmployeeModel employee = _employeeRepository.GetEmployee(1);
            //return View(employee);
            //return View("Test");
            //return View("Test", employee);
            //return View("MyViews/myviews.cshtml");
            //ViewData["pageTitle"] = "Details";
            //ViewData["employee"] = employee;
            /*ViewBag.pageTitle = "ViewBag Index";
            ViewBag.employee = employee;
            return View("Strongly", employee);*/
            TestDetailsViewModel testDetailsViewModel = new TestDetailsViewModel()
            {
                PageTitle = "Mon titre",
                Employee = _employeeRepository.GetEmployee(id??1)
            };

            return View("Example", testDetailsViewModel);
        }
    }
}
