using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClinicApp.Application.Interfaces;
using ClinicApp.Wpf.Services;

namespace ClinicApp.Wpf.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly INavigationService _navigationService;

    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private string _errorMessage = string.Empty;
    [ObservableProperty] private bool _isBusy;

    public LoginViewModel(IAuthService authService, INavigationService navigationService)
    {
        _authService = authService;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task Login()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Введите логин и пароль";
            return;
        }

        IsBusy = true;
        ErrorMessage = string.Empty;

        var success = await _authService.LoginAsync(Username, Password);
        
        IsBusy = false;

        if (success)
        {
            _navigationService.NavigateTo<DashboardViewModel>();
        }
        else
        {
            ErrorMessage = "Неверный логин или пароль";
        }
    }
}
