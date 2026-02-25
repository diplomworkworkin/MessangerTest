using System.Windows;
using System.Windows.Controls;
using ClinicApp.Wpf.ViewModels;

namespace ClinicApp.Wpf;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
    {
        if (sender is ListBoxItem item && DataContext is MainViewModel viewModel)
        {
            var tag = item.Tag?.ToString();
            switch (tag)
            {
                case "Dashboard": viewModel.NavigateToDashboardCommand.Execute(null); break;
                case "Patients": viewModel.NavigateToPatientsCommand.Execute(null); break;
                case "Doctors": viewModel.NavigateToDoctorsCommand.Execute(null); break;
                case "Schedule": viewModel.NavigateToScheduleCommand.Execute(null); break;
                case "Reports": viewModel.NavigateToReportsCommand.Execute(null); break;
                case "Settings": viewModel.NavigateToSettingsCommand.Execute(null); break;
            }
        }
    }
}
