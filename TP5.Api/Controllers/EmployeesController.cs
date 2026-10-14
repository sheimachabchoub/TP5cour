using Microsoft.AspNetCore.Mvc;
using TP5.Api.Models;
using TP5.Api.Models.Repositories;

namespace TP5.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController(IEmployeeRepository employeeRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
    {
        try
        {
            return Ok(await employeeRepository.GetEmployees());
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Employee>> GetEmployee(int id)
    {
        try
        {
            var employee = await employeeRepository.GetEmployee(id);
            return employee is null ? NotFound() : Ok(employee);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<Employee>>> Search([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("The search term is required");
        }

        return Ok(await employeeRepository.Search(name));
    }

    [HttpPost]
    public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
    {
        if (employee is null)
        {
            return BadRequest();
        }

        if (await employeeRepository.GetEmployeeByEmail(employee.Email) is not null)
        {
            ModelState.AddModelError("email", "Employee email already in use");
            return BadRequest(ModelState);
        }

        var createdEmployee = await employeeRepository.AddEmployee(employee);
        return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId }, createdEmployee);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
    {
        if (id != employee.EmployeeId)
        {
            return BadRequest("Employee id mismatch");
        }

        var employeeWithSameEmail = await employeeRepository.GetEmployeeByEmail(employee.Email);
        if (employeeWithSameEmail is not null && employeeWithSameEmail.EmployeeId != id)
        {
            ModelState.AddModelError("email", "Employee email already in use");
            return BadRequest(ModelState);
        }

        var updatedEmployee = await employeeRepository.UpdateEmployee(employee);
        return updatedEmployee is null ? NotFound() : Ok(updatedEmployee);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Employee>> DeleteEmployee(int id)
    {
        var deletedEmployee = await employeeRepository.DeleteEmployee(id);
        return deletedEmployee is null ? NotFound() : Ok(deletedEmployee);
    }
}
