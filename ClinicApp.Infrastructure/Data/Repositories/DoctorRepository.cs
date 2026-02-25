using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Data.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ClinicDbContext _context;

    public DoctorRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return await _context.Set<Doctor>().ToListAsync();
    }

    public async Task<Doctor?> GetByIdAsync(int id)
    {
        return await _context.Set<Doctor>().FindAsync(id);
    }

    public async Task AddAsync(Doctor doctor)
    {
        await _context.Set<Doctor>().AddAsync(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Doctor doctor)
    {
        _context.Set<Doctor>().Update(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var doctor = await GetByIdAsync(id);
        if (doctor != null)
        {
            _context.Set<Doctor>().Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Doctor>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllAsync();

        searchTerm = searchTerm.ToLower();
        return await _context.Set<Doctor>()
            .Where(p => p.FirstName.ToLower().Contains(searchTerm) || 
                        p.LastName.ToLower().Contains(searchTerm) || 
                        p.Specialty.ToLower().Contains(searchTerm))
            .ToListAsync();
    }
}
