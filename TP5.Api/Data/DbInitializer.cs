using Microsoft.EntityFrameworkCore;
using TP5.Api.Models;

namespace TP5.Api.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        if (await context.Departments.AnyAsync())
        {
            return;
        }

        var departments = new[]
        {
            new Department { Name = "Informatique" },
            new Department { Name = "Ressources humaines" },
            new Department { Name = "Finance" }
        };

        context.Departments.AddRange(departments);
        await context.SaveChangesAsync();

        var employees = new[]
        {
            new Employee
            {
                FirstName = "Ahmed",
                LastName = "Ben Ali",
                Email = "ahmed.benali@example.com",
                DateOfBirth = new DateTime(1995, 3, 15),
                DepartmentId = departments[0].DepartmentId
            },
            new Employee
            {
                FirstName = "Sonia",
                LastName = "Trabelsi",
                Email = "sonia.trabelsi@example.com",
                DateOfBirth = new DateTime(1992, 7, 22),
                DepartmentId = departments[0].DepartmentId
            },
            new Employee
            {
                FirstName = "Yassine",
                LastName = "Mansour",
                Email = "yassine.mansour@example.com",
                DateOfBirth = new DateTime(1988, 11, 8),
                DepartmentId = departments[1].DepartmentId
            },
            new Employee
            {
                FirstName = "Mariem",
                LastName = "Gharbi",
                Email = "mariem.gharbi@example.com",
                DateOfBirth = new DateTime(1997, 1, 30),
                DepartmentId = departments[1].DepartmentId
            },
            new Employee
            {
                FirstName = "Karim",
                LastName = "Jaziri",
                Email = "karim.jaziri@example.com",
                DateOfBirth = new DateTime(1990, 9, 12),
                DepartmentId = departments[2].DepartmentId
            }
        };

        context.Employees.AddRange(employees);
        await context.SaveChangesAsync();
    }
}
