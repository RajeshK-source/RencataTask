using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int EmpId
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Employee Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public String EmpName
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Employee Address is required")]
        [StringLength(300)]
        public string Address
        {
            get;
            set;
        }
        [Required(ErrorMessage = "Salary is required")]
        [Range(3000, 1000000, ErrorMessage = "Salary must be between 3000 and 1000000")]
        public int Salary
        {
            get;
            set;
        }
        [Required]
        public string Department
        {
            get;
            set;
        }
    }
}
