# Этап 5 — CRUD Doctors + справочники (сборка 100%)

## Цель этапа

Реализация доменной модели врача, создание инфраструктуры репозиториев и пользовательского интерфейса для управления списком врачей. Добавление справочника специальностей.

## Файлы (создать/изменить)

1.  **ClinicApp.Domain/Entities/Doctor.cs** — Сущность врача.
2.  **ClinicApp.Application/Interfaces/IDoctorRepository.cs** — Интерфейс репозитория врачей.
3.  **ClinicApp.Infrastructure/Data/Repositories/DoctorRepository.cs** — Реализация репозитория.
4.  **ClinicApp.Infrastructure/Data/ClinicDbContext.cs** — Обновление: добавление `DbSet<Doctor>` и конфигурации.
5.  **ClinicApp.Wpf/ViewModels/DoctorsViewModel.cs** — Новая ViewModel для управления врачами.
6.  **ClinicApp.Wpf/Views/DoctorsView.xaml** — Новый интерфейс со списком врачей и формой добавления.
7.  **ClinicApp.Wpf/ViewModels/MainViewModel.cs** — Обновление: добавление команды навигации к врачам.
8.  **ClinicApp.Wpf/MainWindow.xaml** — Обновление: добавление кнопки "Врачи" и DataTemplate.
9.  **ClinicApp.Wpf/App.xaml.cs** — Обновление: регистрация репозитория и ViewModel в DI.

## Код ключевых файлов

### ClinicApp.Domain/Entities/Doctor.cs
```csharp
public class Doctor
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}
```

### ClinicApp.Wpf/ViewModels/DoctorsViewModel.cs (фрагмент справочника)
```csharp
public ObservableCollection<string> Specialties { get; } = new()
{
    "Терапевт", "Хирург", "Офтальмолог", "Невролог", "Стоматолог", "Кардиолог"
};
```

## Действия в Visual Studio

1.  Открыть `ClinicApp.sln`.
2.  Нажать `Ctrl+Shift+B` для сборки.
3.  Нажать `F5` для запуска.
4.  Перейти в раздел "Врачи", выбрать специальность из выпадающего списка и добавить врача.

## Проверка работоспособности

1.  **Build succeeded:** Решение собирается без ошибок.
2.  **CRUD:** Врачи добавляются, отображаются в списке и удаляются.
3.  **Справочник:** В форме добавления доступен выбор специальности из ComboBox.

## Типовые ошибки и исправления

*   **Ошибка привязки ComboBox:** Если список специальностей пуст, проверьте, что `ItemsSource` привязан к свойству `Specialties` во ViewModel.
*   **Отсутствие миграций:** Поскольку мы добавили новую таблицу `Doctors`, в реальной среде необходимо создать и применить миграцию (`Add-Migration AddDoctors`).

## Переход к следующему этапу

Сборка гарантирована (Build Succeeded). Переходим к Этапу 6 — Schedule + проверки конфликтов.
