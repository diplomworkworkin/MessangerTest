using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Domain.Entities;

public class Patient
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? MiddleName { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Address { get; set; }

    public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
}
