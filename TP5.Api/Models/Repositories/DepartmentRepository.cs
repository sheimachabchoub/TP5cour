using Microsoft.EntityFrameworkCore;

namespace TP5.Api.Models.Repositories;

public class DepartmentRepository(AppDbContext appDbContext) : IDepartmentRepository
{
    public async Task<IEnumerable<Department>> GetDepartments() =>
        await appDbContext.Departments
            .AsNoTracking()
            .ToListAsync();

    public async Task<Department?> GetDepartment(int departmentId) =>
        await appDbContext.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(department => department.DepartmentId == departmentId);
}
