using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Entities;

namespace ClinicApp.Wpf.ViewModels;

public partial class ScheduleViewModel : ObservableObject
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;

    [ObservableProperty] private ObservableCollection<Appointment> _appointments = new();
    [ObservableProperty] private ObservableCollection<Patient> _patients = new();
    [ObservableProperty] private ObservableCollection<Doctor> _doctors = new();

    [ObservableProperty] private Patient? _selectedPatient;
    [ObservableProperty] private Doctor? _selectedDoctor;
    [ObservableProperty] private DateTime _appointmentDate = DateTime.Today;
    [ObservableProperty] private string _startTimeStr = "09:00";
    [ObservableProperty] private int _durationMinutes = 30;
    [ObservableProperty] private string _notes = string.Empty;
    [ObservableProperty] private string _errorMessage = string.Empty;
    [ObservableProperty] private bool _isBusy;

    public ScheduleViewModel(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        
        LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadData()
    {
        IsBusy = true;
        var patients = await _patientRepository.GetAllAsync();
        var doctors = await _doctorRepository.GetAllAsync();
        var appointments = await _appointmentRepository.GetAllAsync();

        Patients = new ObservableCollection<Patient>(patients);
        Doctors = new ObservableCollection<Doctor>(doctors);
        Appointments = new ObservableCollection<Appointment>(appointments);
        IsBusy = false;
    }

    [RelayCommand]
    private async Task AddAppointment()
    {
        ErrorMessage = string.Empty;

        if (SelectedPatient == null || SelectedDoctor == null)
        {
            ErrorMessage = "Выберите пациента и врача";
            return;
        }

        if (!TimeSpan.TryParse(StartTimeStr, out var startTimeSpan))
        {
            ErrorMessage = "Неверный формат времени (ЧЧ:ММ)";
            return;
        }

        var startDateTime = AppointmentDate.Date.Add(startTimeSpan);
        var endDateTime = startDateTime.AddMinutes(DurationMinutes);

        if (startDateTime < DateTime.Now)
        {
            ErrorMessage = "Нельзя назначить прием на прошедшее время";
            return;
        }

        var hasConflict = await _appointmentRepository.HasConflictAsync(SelectedDoctor.Id, startDateTime, endDateTime);
        if (hasConflict)
        {
            ErrorMessage = "У врача уже есть запись на это время";
            return;
        }

        var appointment = new Appointment
        {
            PatientId = SelectedPatient.Id,
            DoctorId = SelectedDoctor.Id,
            StartTime = startDateTime,
            EndTime = endDateTime,
            Notes = Notes
        };

        await _appointmentRepository.AddAsync(appointment);
        await LoadData();
        ClearForm();
    }

    [RelayCommand]
    private async Task CancelAppointment(Appointment? appointment)
    {
        if (appointment == null) return;
        appointment.IsCancelled = true;
        await _appointmentRepository.UpdateAsync(appointment);
        await LoadData();
    }

    private void ClearForm()
    {
        SelectedPatient = null;
        SelectedDoctor = null;
        StartTimeStr = "09:00";
        DurationMinutes = 30;
        Notes = string.Empty;
        ErrorMessage = string.Empty;
    }
}
