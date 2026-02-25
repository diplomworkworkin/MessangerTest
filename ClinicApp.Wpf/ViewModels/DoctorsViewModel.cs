using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Entities;

namespace ClinicApp.Wpf.ViewModels;

public partial class DoctorsViewModel : ObservableObject
{
    private readonly IDoctorRepository _doctorRepository;

    [ObservableProperty]
    private ObservableCollection<Doctor> _doctors = new();

    [ObservableProperty]
    private Doctor? _selectedDoctor;

    [ObservableProperty]
    private string _searchQuery = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    // Form fields
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string _specialty = string.Empty;
    [ObservableProperty] private string _phone = string.Empty;

    public ObservableCollection<string> Specialties { get; } = new()
    {
        "Терапевт", "Хирург", "Офтальмолог", "Невролог", "Стоматолог", "Кардиолог"
    };

    public DoctorsViewModel(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
        LoadDoctorsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadDoctors()
    {
        IsBusy = true;
        var doctors = await _doctorRepository.GetAllAsync();
        Doctors = new ObservableCollection<Doctor>(doctors);
        IsBusy = false;
    }

    [RelayCommand]
    private async Task Search()
    {
        IsBusy = true;
        var doctors = await _doctorRepository.SearchAsync(SearchQuery);
        Doctors = new ObservableCollection<Doctor>(doctors);
        IsBusy = false;
    }

    [RelayCommand]
    private async Task AddDoctor()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || string.IsNullOrWhiteSpace(Specialty)) return;

        var doctor = new Doctor
        {
            FirstName = FirstName,
            LastName = LastName,
            Specialty = Specialty,
            Phone = Phone
        };

        await _doctorRepository.AddAsync(doctor);
        await LoadDoctors();
        ClearForm();
    }

    [RelayCommand]
    private async Task DeleteDoctor()
    {
        if (SelectedDoctor == null) return;
        await _doctorRepository.DeleteAsync(SelectedDoctor.Id);
        await LoadDoctors();
    }

    private void ClearForm()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Specialty = string.Empty;
        Phone = string.Empty;
    }
}
