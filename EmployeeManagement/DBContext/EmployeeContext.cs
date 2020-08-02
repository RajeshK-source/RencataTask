using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManagement;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DBContext
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options):base(options)
        {

        }
        public DbSet<Employee> Employee { get; set; }

        
    }
}
