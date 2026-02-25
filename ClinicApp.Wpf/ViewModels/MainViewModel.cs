using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClinicApp.Wpf.Services;
using ClinicApp.Domain.Entities;
using ClinicApp.Application.Interfaces;

namespace ClinicApp.Wpf.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "ClinicApp - Учёт пациентов";

    [ObservableProperty]
    private INavigationService _navigation;

    private readonly IAuthService _authService;

    [ObservableProperty] private bool _isLoggedIn;
    [ObservableProperty] private string _currentUserFullName = string.Empty;
    [ObservableProperty] private bool _isAdmin;
    [ObservableProperty] private bool _isRegistrar;
    [ObservableProperty] private bool _isDoctor;

    public MainViewModel(INavigationService navigation, IAuthService authService)
    {
        _navigation = navigation;
        _authService = authService;
        
        // Подписываемся на изменения навигации для обновления состояния авторизации
        _navigation.Navigated += OnNavigated;
        
        // По умолчанию открываем Login
        NavigateToLogin();
    }

    private void OnNavigated(object? sender, Type viewModelType)
    {
        IsLoggedIn = _authService.CurrentUser != null;
        if (IsLoggedIn)
        {
            CurrentUserFullName = _authService.CurrentUser!.FullName;
            IsAdmin = _authService.IsInRole(UserRole.Admin);
            IsRegistrar = _authService.IsInRole(UserRole.Registrar) || IsAdmin;
            IsDoctor = _authService.IsInRole(UserRole.Doctor) || IsAdmin;
        }
        else
        {
            CurrentUserFullName = string.Empty;
            IsAdmin = IsRegistrar = IsDoctor = false;
        }
    }

    [RelayCommand]
    public void NavigateToLogin() => Navigation.NavigateTo<LoginViewModel>();

    [RelayCommand]
    public void Logout()
    {
        _authService.Logout();
        NavigateToLogin();
    }

    [RelayCommand]
    public void NavigateToDashboard() => Navigation.NavigateTo<DashboardViewModel>();

    [RelayCommand]
    public void NavigateToPatients() => Navigation.NavigateTo<PatientsViewModel>();

    [RelayCommand]
    public void NavigateToDoctors() => Navigation.NavigateTo<DoctorsViewModel>();

    [RelayCommand]
    public void NavigateToSchedule() => Navigation.NavigateTo<ScheduleViewModel>();

    [RelayCommand]
    public void NavigateToSettings() => Navigation.NavigateTo<SettingsViewModel>();
}
