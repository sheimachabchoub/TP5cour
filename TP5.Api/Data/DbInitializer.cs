using Microsoft.EntityFrameworkCore;
using TP5.Api.Models;

namespace TP5.Api.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        if (!await context.Departments.AnyAsync())
        {
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
                new Employee { FirstName = "Ahmed", LastName = "Ben Ali", Email = "ahmed.benali@example.com", DateOfBirth = new DateTime(1995, 3, 15), DepartmentId = departments[0].DepartmentId },
                new Employee { FirstName = "Sonia", LastName = "Trabelsi", Email = "sonia.trabelsi@example.com", DateOfBirth = new DateTime(1992, 7, 22), DepartmentId = departments[0].DepartmentId },
                new Employee { FirstName = "Yassine", LastName = "Mansour", Email = "yassine.mansour@example.com", DateOfBirth = new DateTime(1988, 11, 8), DepartmentId = departments[1].DepartmentId },
                new Employee { FirstName = "Mariem", LastName = "Gharbi", Email = "mariem.gharbi@example.com", DateOfBirth = new DateTime(1997, 1, 30), DepartmentId = departments[1].DepartmentId },
                new Employee { FirstName = "Karim", LastName = "Jaziri", Email = "karim.jaziri@example.com", DateOfBirth = new DateTime(1990, 9, 12), DepartmentId = departments[2].DepartmentId }
            };

            context.Employees.AddRange(employees);
            await context.SaveChangesAsync();
        }

        if (!await context.Genres.AnyAsync())
        {
            var genres = new[]
            {
                new Genre { Name = "Action" },
                new Genre { Name = "Comédie" },
                new Genre { Name = "Science-fiction" },
                new Genre { Name = "Drame" }
            };

            context.Genres.AddRange(genres);
            await context.SaveChangesAsync();

            context.Movies.AddRange(
                new Movie { Title = "Inception", Year = 2010, Rate = 8.8, Storeline = "Un voleur infiltre les rêves pour implanter une idée.", GenreId = genres[2].Id, Poster = Array.Empty<byte>() },
                new Movie { Title = "Intouchables", Year = 2011, Rate = 8.5, Storeline = "Une amitié inattendue change deux vies.", GenreId = genres[1].Id, Poster = Array.Empty<byte>() },
                new Movie { Title = "The Dark Knight", Year = 2008, Rate = 9.0, Storeline = "Batman affronte un criminel qui plonge Gotham dans le chaos.", GenreId = genres[0].Id, Poster = Array.Empty<byte>() });
            await context.SaveChangesAsync();
        }
    }
}
