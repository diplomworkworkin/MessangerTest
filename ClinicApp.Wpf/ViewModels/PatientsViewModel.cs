using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Entities;

namespace ClinicApp.Wpf.ViewModels;

public partial class PatientsViewModel : ObservableObject
{
    private readonly IPatientRepository _patientRepository;

    [ObservableProperty]
    private ObservableCollection<Patient> _patients = new();

    [ObservableProperty]
    private Patient? _selectedPatient;

    [ObservableProperty]
    private string _searchQuery = string.Empty;

    [ObservableProperty]
    private bool _isBusy;

    // Form fields
    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string _phone = string.Empty;
    [ObservableProperty] private DateTime _dateOfBirth = DateTime.Now.AddYears(-30);

    public PatientsViewModel(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
        LoadPatientsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadPatients()
    {
        IsBusy = true;
        var patients = await _patientRepository.GetAllAsync();
        Patients = new ObservableCollection<Patient>(patients);
        IsBusy = false;
    }

    [RelayCommand]
    private async Task Search()
    {
        IsBusy = true;
        var patients = await _patientRepository.SearchAsync(SearchQuery);
        Patients = new ObservableCollection<Patient>(patients);
        IsBusy = false;
    }

    [RelayCommand]
    private async Task AddPatient()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName)) return;

        var patient = new Patient
        {
            FirstName = FirstName,
            LastName = LastName,
            Phone = Phone,
            DateOfBirth = DateOfBirth
        };

        await _patientRepository.AddAsync(patient);
        await LoadPatients();
        ClearForm();
    }

    [RelayCommand]
    private async Task DeletePatient()
    {
        if (SelectedPatient == null) return;
        await _patientRepository.DeleteAsync(SelectedPatient.Id);
        await LoadPatients();
    }

    private void ClearForm()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Phone = string.Empty;
        DateOfBirth = DateTime.Now.AddYears(-30);
    }
}
