using Microsoft.EntityFrameworkCore;
using TP5.Api.Models;

namespace TP5.Api.Services;

public class GenresService(ApplicationDbContext context) : IGenresService
{
    public async Task<IEnumerable<Genre>> GetAll() =>
        await context.Genres.OrderBy(genre => genre.Name).AsNoTracking().ToListAsync();

    public async Task<Genre?> GetById(byte id) =>
        await context.Genres.AsNoTracking().SingleOrDefaultAsync(genre => genre.Id == id);

    public async Task<Genre> Add(Genre genre)
    {
        context.Genres.Add(genre);
        await context.SaveChangesAsync();
        return genre;
    }

    public async Task<Genre?> Update(Genre genre)
    {
        var existing = await context.Genres.FindAsync(genre.Id);
        if (existing is null) return null;
        existing.Name = genre.Name;
        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<Genre?> Delete(Genre genre)
    {
        context.Genres.Remove(genre);
        await context.SaveChangesAsync();
        return genre;
    }

    public Task<bool> IsValidGenre(byte id) => context.Genres.AnyAsync(genre => genre.Id == id);
}
