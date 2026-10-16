using Microsoft.EntityFrameworkCore;
using TP5.Api.Models;

namespace TP5.Api.Services;

public class MoviesService(ApplicationDbContext context) : IMoviesService
{
    public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0) =>
        await context.Movies
            .Where(movie => genreId == 0 || movie.GenreId == genreId)
            .OrderByDescending(movie => movie.Rate)
            .Include(movie => movie.Genre)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Movie?> GetById(int id) =>
        await context.Movies
            .Include(movie => movie.Genre)
            .AsNoTracking()
            .SingleOrDefaultAsync(movie => movie.Id == id);

    public async Task<Movie> Add(Movie movie)
    {
        context.Movies.Add(movie);
        await context.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie?> Update(Movie movie)
    {
        var existing = await context.Movies.FindAsync(movie.Id);
        if (existing is null) return null;
        existing.Title = movie.Title;
        existing.Year = movie.Year;
        existing.Rate = movie.Rate;
        existing.Storeline = movie.Storeline;
        existing.GenreId = movie.GenreId;
        if (movie.Poster.Length > 0) existing.Poster = movie.Poster;
        await context.SaveChangesAsync();
        return await GetById(movie.Id);
    }

    public async Task<Movie?> Delete(Movie movie)
    {
        context.Movies.Remove(movie);
        await context.SaveChangesAsync();
        return movie;
    }
}
