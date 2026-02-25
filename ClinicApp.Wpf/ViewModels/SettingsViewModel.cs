using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClinicApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Wpf.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly ClinicDbContext _dbContext;

    [ObservableProperty]
    private string _message = "Настройки приложения (будут реализованы на этапе 9)";

    [ObservableProperty]
    private string _dbStatus = "Статус БД: Не проверено";

    public SettingsViewModel(ClinicDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [RelayCommand]
    public async Task CheckDbConnection()
    {
        try
        {
            DbStatus = "Статус БД: Проверка...";
            var canConnect = await _dbContext.Database.CanConnectAsync();
            DbStatus = canConnect ? "Статус БД: Подключено" : "Статус БД: Ошибка подключения";
        }
        catch (Exception ex)
        {
            DbStatus = $"Статус БД: Ошибка - {ex.Message}";
        }
    }
}
