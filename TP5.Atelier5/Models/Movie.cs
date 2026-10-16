using System.ComponentModel.DataAnnotations;

namespace TP5.Api.Models;

public class Movie
{
    public int Id { get; set; }

    [Required]
    [MaxLength(250)]
    public string Title { get; set; } = string.Empty;

    [Range(1888, 2100)]
    public int Year { get; set; }

    [Range(0, 10)]
    public double Rate { get; set; }

    [MaxLength(2500)]
    public string Storeline { get; set; } = string.Empty;

    public byte[] Poster { get; set; } = Array.Empty<byte>();

    public byte GenreId { get; set; }
    public Genre? Genre { get; set; }
}
