using Microsoft.AspNetCore.Mvc;
using TP5.Api.Models;
using TP5.Api.Models.Repositories;

namespace TP5.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController(IDepartmentRepository departmentRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Department>>> GetDepartments() =>
        Ok(await departmentRepository.GetDepartments());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Department>> GetDepartment(int id)
    {
        var department = await departmentRepository.GetDepartment(id);
        return department is null ? NotFound() : Ok(department);
    }
}
