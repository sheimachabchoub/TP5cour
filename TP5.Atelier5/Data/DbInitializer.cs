using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using TP5.Api.Models;

namespace TP5.Api.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // LocalDB peut mettre quelques secondes à démarrer avec Visual Studio.
        // On retente la connexion pour éviter que l'API s'arrête pendant ce délai.
        for (var attempt = 1; ; attempt++)
        {
            try
            {
                await context.Database.MigrateAsync();
                break;
            }
            catch (SqlException) when (attempt < 5)
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
        }

        if (await context.Genres.AnyAsync()) return;

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
