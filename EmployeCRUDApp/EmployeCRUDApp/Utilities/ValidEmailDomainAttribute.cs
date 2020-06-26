using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeCRUDApp.Utilities
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        public ValidEmailDomainAttribute(string allowedDomain)
        {
            _allowedDomain = allowedDomain;
        }

        public string _allowedDomain { get; }

        public override bool IsValid(object value)
        {
            string[] strings = value.ToString().Split('@');
            return strings[1].ToUpper() == _allowedDomain.ToUpper();
        }
    }
}
