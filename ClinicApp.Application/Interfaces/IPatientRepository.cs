using ClinicApp.Domain.Entities;

namespace ClinicApp.Application.Interfaces;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(int id);
    Task AddAsync(Patient patient);
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(int id);
    Task<IEnumerable<Patient>> SearchAsync(string searchTerm);
}
