using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Data.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ClinicDbContext _context;

    public PatientRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await _context.Set<Patient>().ToListAsync();
    }

    public async Task<Patient?> GetByIdAsync(int id)
    {
        return await _context.Set<Patient>().FindAsync(id);
    }

    public async Task AddAsync(Patient patient)
    {
        await _context.Set<Patient>().AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Patient patient)
    {
        _context.Set<Patient>().Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var patient = await GetByIdAsync(id);
        if (patient != null)
        {
            _context.Set<Patient>().Remove(patient);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Patient>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllAsync();

        searchTerm = searchTerm.ToLower();
        return await _context.Set<Patient>()
            .Where(p => p.FirstName.ToLower().Contains(searchTerm) || 
                        p.LastName.ToLower().Contains(searchTerm) || 
                        p.Phone.Contains(searchTerm))
            .ToListAsync();
    }
}
