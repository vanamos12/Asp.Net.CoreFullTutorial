using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeCRUDApp.Models;
using EmployeCRUDApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeCRUDApp.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        public IActionResult Details()
        {
            //return View();
            return Json("Hello World from MVC");
        }

        [AllowAnonymous]
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

        [HttpGet]
        public ViewResult Edit(int id)
        {
            EmployeeModel employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Departement = employee.Departement,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "img", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    uniqueFileName = ProcessPhoto(model);
                }
                else
                {
                    uniqueFileName = model.ExistingPhotoPath;
                }

                EmployeeModel newEmployee = new EmployeeModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    Departement = model.Departement,
                    PhotoPath = uniqueFileName
                };
                EmployeeModel employeeSaved = _employeeRepository.Update(newEmployee);
                return Redirect("/Test/Details/" + employeeSaved.Id);
                //EmployeeModel employeeSaved = _employeeRepository.Add(employee);
                //return Redirect("/Test/Details/" + employeeSaved.Id);
            }

            return View();
        }

        private string ProcessPhoto(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                    
            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    uniqueFileName = ProcessPhoto(model);
                }
                /*if (model.Photos != null && model.Photos.Count >= 1)
                {
                    foreach (IFormFile photo in model.Photos)
                    {
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    
                }*/
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