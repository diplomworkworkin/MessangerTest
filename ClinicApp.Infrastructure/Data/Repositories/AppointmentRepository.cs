using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Data.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ClinicDbContext _context;

    public AppointmentRepository(ClinicDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Set<Appointment>()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(int doctorId, DateTime date)
    {
        return await _context.Set<Appointment>()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.DoctorId == doctorId && a.StartTime.Date == date.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(int patientId)
    {
        return await _context.Set<Appointment>()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.PatientId == patientId)
            .ToListAsync();
    }

    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _context.Set<Appointment>()
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Appointment appointment)
    {
        await _context.Set<Appointment>().AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Set<Appointment>().Update(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var appointment = await GetByIdAsync(id);
        if (appointment != null)
        {
            _context.Set<Appointment>().Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> HasConflictAsync(int doctorId, DateTime startTime, DateTime endTime, int? excludeAppointmentId = null)
    {
        var query = _context.Set<Appointment>()
            .Where(a => a.DoctorId == doctorId && !a.IsCancelled);

        if (excludeAppointmentId.HasValue)
        {
            query = query.Where(a => a.Id != excludeAppointmentId.Value);
        }

        return await query.AnyAsync(a => 
            (startTime >= a.StartTime && startTime < a.EndTime) || 
            (endTime > a.StartTime && endTime <= a.EndTime) || 
            (startTime <= a.StartTime && endTime >= a.EndTime));
    }
}
