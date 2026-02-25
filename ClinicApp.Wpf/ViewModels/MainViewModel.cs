using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClinicApp.Wpf.Services;

namespace ClinicApp.Wpf.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "ClinicApp - Учёт пациентов";

    public INavigationService Navigation { get; }

    public MainViewModel(INavigationService navigation)
    {
        Navigation = navigation;
        Navigation.NavigateTo<DashboardViewModel>();
    }

    [RelayCommand]
    public void NavigateToDashboard() => Navigation.NavigateTo<DashboardViewModel>();

    [RelayCommand]
    public void NavigateToPatients() => Navigation.NavigateTo<PatientsViewModel>();

    [RelayCommand]
    public void NavigateToDoctors() => Navigation.NavigateTo<DoctorsViewModel>();

    [RelayCommand]
    public void NavigateToSettings() => Navigation.NavigateTo<SettingsViewModel>();
}
