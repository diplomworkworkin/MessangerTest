using CommunityToolkit.Mvvm.ComponentModel;
using ClinicApp.Wpf.Services;

namespace ClinicApp.Wpf.ViewModels;

public partial class SplashViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public SplashViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        StartLoading();
    }

    private async void StartLoading()
    {
        // Имитация загрузки ресурсов
        await Task.Delay(3000);
        
        // Переход на экран входа
        _navigationService.NavigateTo<LoginViewModel>();
    }
}
