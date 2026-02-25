using CommunityToolkit.Mvvm.ComponentModel;

namespace ClinicApp.Wpf.ViewModels;

public partial class PatientsViewModel : ObservableObject
{
    [ObservableProperty]
    private string _message = "Список пациентов (будет реализован на этапе 4)";
}
