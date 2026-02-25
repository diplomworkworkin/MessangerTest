using CommunityToolkit.Mvvm.ComponentModel;

namespace ClinicApp.Wpf.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "ClinicApp - Учёт пациентов";
}
