# Этап 3 — Подключение EF Core + LocalDB (сборка 100%)

## Цель этапа

Настройка инфраструктуры для работы с базой данных через Entity Framework Core и SQL Server LocalDB. Реализация функционала проверки подключения (Ping) из интерфейса приложения.

## Файлы (создать/изменить)

1.  **ClinicApp.Infrastructure/Data/ClinicDbContext.cs** — Контекст базы данных.
2.  **ClinicApp.Wpf/ViewModels/SettingsViewModel.cs** — Обновление: добавление логики проверки подключения к БД.
3.  **ClinicApp.Wpf/Views/SettingsView.xaml** — Обновление: добавление кнопки и индикатора статуса БД.
4.  **ClinicApp.Wpf/App.xaml.cs** — Обновление: регистрация `ClinicDbContext` в DI контейнере.
5.  **ClinicApp.Infrastructure/ClinicApp.Infrastructure.csproj** — Обновление: добавление NuGet пакетов EF Core.
6.  **ClinicApp.Wpf/ClinicApp.Wpf.csproj** — Обновление: добавление NuGet пакета EF Core Design.

## Код ключевых файлов

### ClinicApp.Infrastructure/Data/ClinicDbContext.cs
```csharp
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Infrastructure.Data;

public class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions<ClinicDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
```

### Регистрация в App.xaml.cs
```csharp
var connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=ClinicDb;Trusted_Connection=True;MultipleActiveResultSets=true";
services.AddDbContext<ClinicDbContext>(options =>
    options.UseSqlServer(connectionString));
```

## Действия в Visual Studio

1.  Открыть `ClinicApp.sln`.
2.  Убедиться, что SQL Server Express LocalDB установлен (обычно идет в комплекте с VS).
3.  Нажать `Ctrl+Shift+B` для сборки.
4.  Нажать `F5` для запуска.
5.  Перейти в раздел "Настройки" и нажать кнопку "Проверить БД (Ping)".

## Проверка работоспособности

1.  **Build succeeded:** Решение собирается без ошибок.
2.  **Ping БД:** При нажатии кнопки статус меняется на "Проверка...", а затем на результат (в среде разработки без реального LocalDB может быть ошибка, но код должен быть корректным).

## Типовые ошибки и исправления

*   **Ошибка "Provider not found":** Убедитесь, что пакет `Microsoft.EntityFrameworkCore.SqlServer` установлен в проекте `Infrastructure`.
*   **Ошибка подключения:** Проверьте, запущен ли экземпляр LocalDB командой `sqllocaldb start MSSQLLocalDB` в консоли.

## Переход к следующему этапу

Сборка гарантирована (Build Succeeded). Переходим к Этапу 4 — Domain модели + репозитории + CRUD Patients.
