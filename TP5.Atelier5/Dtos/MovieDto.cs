using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TP5.Api.Dtos;

public class MovieDto
{
    [Required]
    [MaxLength(250)]
    public string Title { get; set; } = string.Empty;

    [Range(1888, 2100)]
    public int Year { get; set; }

    [Range(0, 10)]
    public double Rate { get; set; }

    [MaxLength(2500)]
    public string Storeline { get; set; } = string.Empty;

    [Required]
    public IFormFile? Poster { get; set; }

    [Range(1, 255)]
    public byte GenreId { get; set; }
}
