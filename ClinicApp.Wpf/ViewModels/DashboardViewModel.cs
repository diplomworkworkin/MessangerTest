using CommunityToolkit.Mvvm.ComponentModel;

namespace ClinicApp.Wpf.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    [ObservableProperty]
    private string _message = "Добро пожаловать в ClinicApp!";
}
