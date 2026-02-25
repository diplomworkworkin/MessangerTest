using ClinicApp.Domain.Entities;

namespace ClinicApp.Application.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId, DateTime date);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId);
    Task<Appointment?> GetByIdAsync(int id);
    Task AddAsync(Appointment appointment);
    Task UpdateAsync(Appointment appointment);
    Task DeleteAsync(int id);
    Task<bool> HasConflictAsync(int doctorId, DateTime startTime, DateTime endTime, int? excludeAppointmentId = null);
}
