﻿using EmployeCRUDApp.Models;
using EmployeCRUDApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public TestController(IEmployeeRepository employeeRepository, ILogger<TestController> logger)
        {
            _logger = logger;
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
            //throw new Exception("Error in details view");
            _logger.LogTrace("Trace Log");
            _logger.LogDebug("Debug Log");
            _logger.LogInformation("Information Log");
            _logger.LogWarning("Warning Log");
            _logger.LogError("Error Log");
            _logger.LogCritical("Critical Log");
            EmployeeModel employee = _employeeRepository.GetEmployee(id.Value);

            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
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
                Employee = employee
            };

            return View("Example", testDetailsViewModel);
        }
    }
}
