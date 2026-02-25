using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClinicApp.Application.Interfaces;
using System.IO;
using Microsoft.Win32;

namespace ClinicApp.Wpf.ViewModels;

public partial class ReportsViewModel : ObservableObject
{
    private readonly IReportService _reportService;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;

    [ObservableProperty] private bool _isBusy;
    [ObservableProperty] private string _statusMessage = string.Empty;

    public ReportsViewModel(
        IReportService reportService,
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository)
    {
        _reportService = reportService;
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
    }

    [RelayCommand]
    private async Task ExportAppointments()
    {
        IsBusy = true;
        StatusMessage = "Подготовка отчета по приемам...";

        try
        {
            var appointments = await _appointmentRepository.GetAllAsync();
            var csv = await _reportService.ExportAppointmentsToCsvAsync(appointments);
            
            SaveToFile("appointments_report.csv", csv);
            StatusMessage = "Отчет по приемам успешно экспортирован!";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Ошибка: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ExportPatients()
    {
        IsBusy = true;
        StatusMessage = "Подготовка отчета по пациентам...";

        try
        {
            var patients = await _patientRepository.GetAllAsync();
            var csv = await _reportService.ExportPatientsToCsvAsync(patients);
            
            SaveToFile("patients_report.csv", csv);
            StatusMessage = "Отчет по пациентам успешно экспортирован!";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Ошибка: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void SaveToFile(string defaultFileName, string content)
    {
        var saveFileDialog = new SaveFileDialog
        {
            FileName = defaultFileName,
            DefaultExt = ".csv",
            Filter = "CSV documents (.csv)|*.csv"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            File.WriteAllText(saveFileDialog.FileName, content, System.Text.Encoding.UTF8);
        }
    }
}
