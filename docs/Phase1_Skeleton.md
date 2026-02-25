# Этап 1 — Скелет решения (сборка 100%)

## Цель этапа

Создание базовой структуры Solution, обеспечивающей компиляцию и запуск пустого окна с использованием Dependency Injection (DI) и базового MVVM.

## Файлы (создать/изменить)

1.  **ClinicApp.sln** — Файл решения.
2.  **ClinicApp.Domain/ClinicApp.Domain.csproj** — Проект доменных моделей.
3.  **ClinicApp.Application/ClinicApp.Application.csproj** — Проект логики приложения.
4.  **ClinicApp.Infrastructure/ClinicApp.Infrastructure.csproj** — Проект инфраструктуры.
5.  **ClinicApp.Wpf/ClinicApp.Wpf.csproj** — Проект UI (WPF).
6.  **ClinicApp.Tests/ClinicApp.Tests.csproj** — Проект тестов.
7.  **ClinicApp.Wpf/ViewModels/MainViewModel.cs** — Базовая ViewModel для главного окна.
8.  **ClinicApp.Wpf/MainWindow.xaml** — Разметка главного окна.
9.  **ClinicApp.Wpf/App.xaml** — Настройка запуска приложения (удалён StartupUri).
10. **ClinicApp.Wpf/App.xaml.cs** — Настройка DI контейнера и запуск MainWindow.

## Код ключевых файлов

### ClinicApp.Wpf/ViewModels/MainViewModel.cs
```csharp
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClinicApp.Wpf.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "ClinicApp - Учёт пациентов";
}
```

### ClinicApp.Wpf/App.xaml.cs
```csharp
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ClinicApp.Wpf.ViewModels;

namespace ClinicApp.Wpf;

public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>(s => new MainWindow
        {
            DataContext = s.GetRequiredService<MainViewModel>()
        });
    }
}
```

## Действия в Visual Studio

1.  Открыть `ClinicApp.sln`.
2.  Убедиться, что все проекты загружены.
3.  Нажать `Ctrl+Shift+B` для сборки всего решения.
4.  Нажать `F5` для запуска проекта `ClinicApp.Wpf`.

## Проверка работоспособности

1.  **Build succeeded:** Решение собирается без ошибок.
2.  **Запуск:** При запуске открывается окно с заголовком "MainWindow" (или привязанным из VM) и текстом "ClinicApp" в центре.
3.  **DI:** Окно создаётся через ServiceProvider, что подтверждает работу DI.

## Типовые ошибки и исправления

*   **Ошибка NETSDK1100:** "To build a project targeting Windows on this operating system, set the EnableWindowsTargeting property to true."
    *   *Исправление:* Добавить `<EnableWindowsTargeting>true</EnableWindowsTargeting>` в `.csproj` файл WPF проекта.
*   **Ошибка при запуске (StartupUri):** Если оставить `StartupUri` в `App.xaml` и одновременно запускать окно в `App.xaml.cs`, откроется два окна.
    *   *Исправление:* Удалить `StartupUri="MainWindow.xaml"` из `App.xaml`.

## Переход к следующему этапу

Сборка гарантирована (Build Succeeded). Переходим к Этапу 2 — Навигация и каркас UI.
