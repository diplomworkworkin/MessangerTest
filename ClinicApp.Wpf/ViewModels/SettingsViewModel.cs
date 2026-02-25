using CommunityToolkit.Mvvm.ComponentModel;

namespace ClinicApp.Wpf.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _message = "Настройки приложения (будут реализованы на этапе 9)";
}
