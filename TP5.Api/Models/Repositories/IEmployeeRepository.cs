namespace TP5.Api.Models.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployees();
    Task<Employee?> GetEmployee(int employeeId);
    Task<Employee?> GetEmployeeByEmail(string email);
    Task<Employee> AddEmployee(Employee employee);
    Task<Employee?> UpdateEmployee(Employee employee);
    Task<Employee?> DeleteEmployee(int employeeId);
    Task<IEnumerable<Employee>> Search(string name);
}
