using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Domain.Entities;

public class Appointment
{
    public int Id { get; set; }

    [Required]
    public int PatientId { get; set; }
    public Patient? Patient { get; set; }

    [Required]
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }

    public bool IsCancelled { get; set; }
}
