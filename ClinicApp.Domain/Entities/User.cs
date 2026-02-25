using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Domain.Entities;

public enum UserRole
{
    Admin,
    Registrar,
    Doctor
}

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; }

    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
}
