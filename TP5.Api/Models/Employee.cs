using System.ComponentModel.DataAnnotations;

namespace TP5.Api.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public int DepartmentId { get; set; }

    public Department? Department { get; set; }
}
