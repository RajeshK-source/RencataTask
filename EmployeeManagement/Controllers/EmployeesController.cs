using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.DBContext;
using EmployeeManagement.Models;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _context;
        private readonly ILogger _logger;

        public EmployeesController(EmployeeContext context, ILogger<EmployeesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployee()
        {
            var getEmployees = new List<Employee>();
            try
            {
                getEmployees = _context.Employee.ToList();
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message, ex.InnerException + " " + DateTime.Now);
            }
            return getEmployees;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            var employee = new Employee();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                employee = await _context.Employee.FindAsync(id);

                if (employee == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, ex.InnerException + " " + DateTime.Now);
            }
            

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != employee.EmpId)
                {
                    return BadRequest();
                }

                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogInformation(ex.Message, ex.InnerException + " " + DateTime.Now);
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, ex.InnerException + " " + DateTime.Now);
            }                      

            return NoContent();
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, ex.InnerException + " " + DateTime.Now);
            }
            

            return CreatedAtAction("GetEmployee", new { id = employee.EmpId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var employee = new Employee();
            try
            {
                employee = await _context.Employee.FindAsync(id);
                if (employee == null)
                {
                    return NotFound();
                }

                _context.Employee.Remove(employee);
                await _context.SaveChangesAsync();

                
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, ex.InnerException + " " + DateTime.Now);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(employee);
        }

        private bool EmployeeExists(int id)
        {
            var employeeExist = false;
            try
            {
               employeeExist = _context.Employee.Any(e => e.EmpId == id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message, ex.InnerException + " " + DateTime.Now);
            }
            return employeeExist;
        }
    }
}