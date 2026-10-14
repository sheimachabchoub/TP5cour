namespace TP5.Api.Models.Repositories;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetDepartments();
    Task<Department?> GetDepartment(int departmentId);
}
