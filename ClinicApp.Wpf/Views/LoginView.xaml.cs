using System.Windows.Controls;
using ClinicApp.Wpf.ViewModels;

namespace ClinicApp.Wpf.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
        PasswordBox.PasswordChanged += (s, e) =>
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        };
    }
}
