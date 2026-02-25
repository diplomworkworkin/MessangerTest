# Этап 2 — Навигация и каркас UI (сборка 100%)

## Цель этапа

Реализация каркаса приложения (Shell), бокового меню и системы навигации между тремя страницами (Dashboard, Patients, Settings) с использованием Dependency Injection и анимации переходов.

## Файлы (создать/изменить)

1.  **ClinicApp.Wpf/Services/INavigationService.cs** — Интерфейс сервиса навигации.
2.  **ClinicApp.Wpf/Services/NavigationService.cs** — Реализация сервиса навигации.
3.  **ClinicApp.Wpf/ViewModels/DashboardViewModel.cs** — ViewModel для главной страницы.
4.  **ClinicApp.Wpf/ViewModels/PatientsViewModel.cs** — ViewModel для страницы пациентов.
5.  **ClinicApp.Wpf/ViewModels/SettingsViewModel.cs** — ViewModel для страницы настроек.
6.  **ClinicApp.Wpf/ViewModels/MainViewModel.cs** — Обновление: добавление логики навигации.
7.  **ClinicApp.Wpf/Views/DashboardView.xaml** — Разметка главной страницы.
8.  **ClinicApp.Wpf/Views/PatientsView.xaml** — Разметка страницы пациентов.
9.  **ClinicApp.Wpf/Views/SettingsView.xaml** — Разметка страницы настроек.
10. **ClinicApp.Wpf/MainWindow.xaml** — Обновление: добавление Sidebar и ContentControl для навигации.
11. **ClinicApp.Wpf/App.xaml.cs** — Обновление: регистрация новых сервисов и ViewModels в DI.

## Код ключевых файлов

### ClinicApp.Wpf/Services/NavigationService.cs
```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicApp.Wpf.Services;

public partial class NavigationService : ObservableObject, INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private ObservableObject? _currentView;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void NavigateTo<T>() where T : ObservableObject
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();
        CurrentView = viewModel;
    }
}
```

### ClinicApp.Wpf/MainWindow.xaml (фрагмент навигации)
```xml
<ContentControl Content="{Binding Navigation.CurrentView}">
    <ContentControl.Resources>
        <Style TargetType="ContentPresenter">
            <Style.Triggers>
                <EventTrigger RoutedEvent="Loaded">
                    <BeginStoryboard>
                        <Storyboard>
                            <DoubleAnimation Storyboard.TargetProperty="Opacity"
                                             From="0" To="1" Duration="0:0:0.3"/>
                        </Storyboard>
                    </BeginStoryboard>
                </EventTrigger>
            </Style.Triggers>
        </Style>
    </ContentControl.Resources>
</ContentControl>
```

## Действия в Visual Studio

1.  Открыть `ClinicApp.sln`.
2.  Нажать `Ctrl+Shift+B` для сборки.
3.  Нажать `F5` для запуска.

## Проверка работоспособности

1.  **Build succeeded:** Решение собирается без ошибок.
2.  **Навигация:** При нажатии на кнопки "Главная", "Пациенты", "Настройки" содержимое правой части окна меняется.
3.  **Анимация:** Переход между страницами сопровождается плавным появлением (Fade-in).

## Типовые ошибки и исправления

*   **Ошибка привязки (Binding):** Если `CurrentView` не обновляется, проверьте, что `NavigationService` зарегистрирован как `Singleton`.
*   **Отсутствие DataTemplate:** Если вместо страницы отображается название класса (например, `ClinicApp.Wpf.ViewModels.DashboardViewModel`), проверьте наличие `DataTemplate` в `MainWindow.xaml`.

## Переход к следующему этапу

Сборка гарантирована (Build Succeeded). Переходим к Этапу 3 — Подключение EF Core + LocalDB.
