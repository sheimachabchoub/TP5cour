using Microsoft.EntityFrameworkCore;

namespace TP5.Api.Models.Repositories;

public class EmployeeRepository(AppDbContext appDbContext) : IEmployeeRepository
{
    public async Task<IEnumerable<Employee>> GetEmployees() =>
        await appDbContext.Employees
            .Include(employee => employee.Department)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Employee?> GetEmployee(int employeeId) =>
        await appDbContext.Employees
            .Include(employee => employee.Department)
            .AsNoTracking()
            .FirstOrDefaultAsync(employee => employee.EmployeeId == employeeId);

    public async Task<Employee?> GetEmployeeByEmail(string email) =>
        await appDbContext.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(employee => employee.Email == email);

    public async Task<Employee> AddEmployee(Employee employee)
    {
        appDbContext.Employees.Add(employee);
        await appDbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<Employee?> UpdateEmployee(Employee employee)
    {
        var existingEmployee = await appDbContext.Employees
            .FirstOrDefaultAsync(item => item.EmployeeId == employee.EmployeeId);

        if (existingEmployee is null)
        {
            return null;
        }

        existingEmployee.FirstName = employee.FirstName;
        existingEmployee.LastName = employee.LastName;
        existingEmployee.Email = employee.Email;
        existingEmployee.DateOfBirth = employee.DateOfBirth;
        existingEmployee.DepartmentId = employee.DepartmentId;
        await appDbContext.SaveChangesAsync();

        return await GetEmployee(existingEmployee.EmployeeId);
    }

    public async Task<Employee?> DeleteEmployee(int employeeId)
    {
        var employee = await appDbContext.Employees
            .FirstOrDefaultAsync(item => item.EmployeeId == employeeId);

        if (employee is null)
        {
            return null;
        }

        appDbContext.Employees.Remove(employee);
        await appDbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<IEnumerable<Employee>> Search(string name)
    {
        var query = name.Trim();

        return await appDbContext.Employees
            .Include(employee => employee.Department)
            .AsNoTracking()
            .Where(employee => employee.FirstName.Contains(query) || employee.LastName.Contains(query))
            .ToListAsync();
    }
}
