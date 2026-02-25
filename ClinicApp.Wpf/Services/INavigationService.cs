using CommunityToolkit.Mvvm.ComponentModel;

namespace ClinicApp.Wpf.Services;

public interface INavigationService
{
    ObservableObject? CurrentView { get; }
    void NavigateTo<T>() where T : ObservableObject;
}
