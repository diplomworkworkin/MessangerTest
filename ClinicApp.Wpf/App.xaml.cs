using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ClinicApp.Wpf.ViewModels;
using ClinicApp.Wpf.Services;
using ClinicApp.Infrastructure.Data;
using ClinicApp.Infrastructure.Data.Repositories;
using ClinicApp.Application.Interfaces;
using ClinicApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Wpf;

public partial class App : System.Windows.Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        // Принудительная инициализация базы данных при старте
        try
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ClinicDbContext>();
                // Создает базу данных, если она не существует
                dbContext.Database.EnsureCreated();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка инициализации базы данных: {ex.Message}\n\nУбедитесь, что LocalDB установлен и запущен.", 
                            "Ошибка БД", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Database с логикой повторных попыток (EnableRetryOnFailure)
        var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ClinicDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        services.AddDbContext<ClinicDbContext>(options =>
            options.UseSqlServer(connectionString, sqlOptions => 
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null)));

        // Services
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IReportService, ReportService>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();

        // ViewModels
        services.AddSingleton<MainViewModel>();
        services.AddTransient<SplashViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<PatientsViewModel>();
        services.AddTransient<DoctorsViewModel>();
        services.AddTransient<ScheduleViewModel>();
        services.AddTransient<ReportsViewModel>();
        services.AddTransient<SettingsViewModel>();

        // Windows
        services.AddSingleton<MainWindow>(s => new MainWindow
        {
            DataContext = s.GetRequiredService<MainViewModel>()
        });
    }
}
