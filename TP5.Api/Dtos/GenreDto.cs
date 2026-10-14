using System.ComponentModel.DataAnnotations;

namespace TP5.Api.Dtos;

public class GenreDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
