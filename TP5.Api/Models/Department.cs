using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TP5.Api.Models;

public class Department
{
    public int DepartmentId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
