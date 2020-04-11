using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeCRUDApp.Models;
using EmployeCRUDApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EmployeCRUDApp.Controllers
{
    
    public class HomeController : Controller
    {
        public IEmployeeRepository _employeeRepository;
        public IHostingEnvironment _hostingEnvironment;
        public HomeController(IEmployeeRepository employeeRepository,
                                IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Details()
        {
            //return View();
            return Json("Hello World from MVC");
        }
        
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                EmployeeModel newEmployee = new EmployeeModel
                {
                    Name = model.Name,
                    Email = model.Email,
                    Departement = model.Departement,
                    PhotoPath = uniqueFileName
                };
                EmployeeModel employeeSaved = _employeeRepository.Add(newEmployee);
                return Redirect("/Test/Details/" + employeeSaved.Id);
                //EmployeeModel employeeSaved = _employeeRepository.Add(employee);
                //return Redirect("/Test/Details/" + employeeSaved.Id);
            }
            
            return View();
        }
    }
}