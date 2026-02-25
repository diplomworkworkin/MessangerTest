using CommunityToolkit.Mvvm.ComponentModel;

namespace ClinicApp.Wpf.Services;

public interface INavigationService
{
    ObservableObject? CurrentView { get; }
    event EventHandler<Type>? Navigated;
    void NavigateTo<T>() where T : ObservableObject;
}
