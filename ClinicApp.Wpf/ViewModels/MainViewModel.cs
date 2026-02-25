using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using ClinicApp.Wpf.Services;
using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Entities;

namespace ClinicApp.Wpf.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly INavigationService _navigation;
    private readonly IAuthService _authService;

    [ObservableProperty]
    private string _title = "ClinicApp - Учёт пациентов";

    [ObservableProperty]
    private bool _isLoggedIn;

    [ObservableProperty]
    private string _currentUserFullName = string.Empty;

    [ObservableProperty]
    private bool _isAdmin;

    [ObservableProperty]
    private bool _isRegistrar;

    public INavigationService Navigation => _navigation;

    public MainViewModel(INavigationService navigation, IAuthService authService)
    {
        _navigation = navigation;
        _authService = authService;
        
        _navigation.Navigated += OnNavigated;
        
        // Начинаем со Splash
        NavigateToSplash();
    }

    [RelayCommand]
    public void NavigateToSplash() => _navigation.NavigateTo<SplashViewModel>();

    [RelayCommand]
    public void NavigateToLogin() => _navigation.NavigateTo<LoginViewModel>();

    [RelayCommand]
    public void NavigateToDashboard() => _navigation.NavigateTo<DashboardViewModel>();

    [RelayCommand]
    public void NavigateToPatients() => _navigation.NavigateTo<PatientsViewModel>();

    [RelayCommand]
    public void NavigateToDoctors() => _navigation.NavigateTo<DoctorsViewModel>();

    [RelayCommand]
    public void NavigateToSchedule() => _navigation.NavigateTo<ScheduleViewModel>();

    [RelayCommand]
    public void NavigateToReports() => _navigation.NavigateTo<ReportsViewModel>();

    [RelayCommand]
    public void NavigateToSettings() => _navigation.NavigateTo<SettingsViewModel>();

    [RelayCommand]
    public void Logout()
    {
        _authService.Logout();
        UpdateAuthState();
        NavigateToLogin();
    }

    private void OnNavigated(object? sender, Type viewModelType)
    {
        UpdateAuthState();
    }

    private void UpdateAuthState()
    {
        var user = _authService.CurrentUser;
        IsLoggedIn = user != null;
        CurrentUserFullName = user?.FullName ?? string.Empty;
        IsAdmin = _authService.IsInRole(UserRole.Admin);
        IsRegistrar = _authService.IsInRole(UserRole.Admin) || _authService.IsInRole(UserRole.Registrar);
    }
}
