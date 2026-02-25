using ClinicApp.Domain.Entities;

namespace ClinicApp.Application.Interfaces;

public interface IReportService
{
    Task<string> ExportAppointmentsToCsvAsync(IEnumerable<Appointment> appointments);
    Task<string> ExportPatientsToCsvAsync(IEnumerable<Patient> patients);
}
