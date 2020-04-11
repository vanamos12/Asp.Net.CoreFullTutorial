using EmployeCRUDApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeCRUDApp.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required(ErrorMessage = "Le nom est obligatoire")]
        [MaxLength(50, ErrorMessage = "Le nom ne doit pas avoir plus de 50 characteres.")]
        public string Name { get; set; }
        [Required]
        public Dept? Departement { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }
        public IFormFile Photo { get; set; }
    }
}
