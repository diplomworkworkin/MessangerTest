using System.Text;
using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Entities;

namespace ClinicApp.Infrastructure.Data.Repositories;

public class ReportService : IReportService
{
    public async Task<string> ExportAppointmentsToCsvAsync(IEnumerable<Appointment> appointments)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ID;Patient;Doctor;StartTime;EndTime;Status;Notes");

        foreach (var app in appointments)
        {
            sb.AppendLine($"{app.Id};{app.Patient?.FullName};{app.Doctor?.FullName};{app.StartTime:dd.MM.yyyy HH:mm};{app.EndTime:HH:mm};{(app.IsCancelled ? "Cancelled" : "Active")};{app.Notes}");
        }

        return await Task.FromResult(sb.ToString());
    }

    public async Task<string> ExportPatientsToCsvAsync(IEnumerable<Patient> patients)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ID;LastName;FirstName;Phone;DateOfBirth");

        foreach (var p in patients)
        {
            sb.AppendLine($"{p.Id};{p.LastName};{p.FirstName};{p.Phone};{p.DateOfBirth:dd.MM.yyyy}");
        }

        return await Task.FromResult(sb.ToString());
    }
}
