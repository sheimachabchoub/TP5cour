using Microsoft.EntityFrameworkCore;

namespace TP5.Api.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Movie> Movies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>()
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies)
            .HasForeignKey(movie => movie.GenreId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Movie>()
            .Property(movie => movie.Poster)
            .HasColumnType("varbinary(max)");
    }
}
