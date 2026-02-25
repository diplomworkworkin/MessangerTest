using ClinicApp.Domain.Entities;

namespace ClinicApp.Application.Interfaces;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor>> GetAllAsync();
    Task<Doctor?> GetByIdAsync(int id);
    Task AddAsync(Doctor doctor);
    Task UpdateAsync(Doctor doctor);
    Task DeleteAsync(int id);
    Task<IEnumerable<Doctor>> SearchAsync(string searchTerm);
}
