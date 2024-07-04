using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public EmployeeController(NorthwindContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return BadRequest("Employee ID is required.");
            }
            var employeeHierarchy = GetEmployeeHierarchy(id.Value);
            return Ok(employeeHierarchy);
        }

        private List<EmployeeDto> GetEmployeeHierarchy(int id)
        {
            //建立空的DTO清單
            var result = new List<EmployeeDto>();
            var currentEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);

            while (currentEmployee != null)
            {
                var employeeDto = new EmployeeDto
                {
                    EmpId = currentEmployee.EmployeeId,
                    FirstName = currentEmployee.FirstName,
                    LastName = currentEmployee.LastName,
                    ReportsTo = currentEmployee.ReportsTo
                };
                result.Add(employeeDto);

                currentEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeId == currentEmployee.ReportsTo);
            }

            return result;
        }


    }
}
