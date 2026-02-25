# Этап 4 — Domain модели + репозитории + CRUD Patients (сборка 100%)

## Цель этапа

Реализация базовой доменной модели пациента, создание инфраструктуры репозиториев и полноценного пользовательского интерфейса для управления списком пациентов (CRUD) с сохранением в базу данных.

## Файлы (создать/изменить)

1.  **ClinicApp.Domain/Entities/Patient.cs** — Сущность пациента.
2.  **ClinicApp.Application/Interfaces/IPatientRepository.cs** — Интерфейс репозитория пациентов.
3.  **ClinicApp.Infrastructure/Data/Repositories/PatientRepository.cs** — Реализация репозитория.
4.  **ClinicApp.Infrastructure/Data/ClinicDbContext.cs** — Обновление: добавление `DbSet<Patient>` и конфигурации.
5.  **ClinicApp.Wpf/ViewModels/PatientsViewModel.cs** — Обновление: логика CRUD, поиск, загрузка данных.
6.  **ClinicApp.Wpf/Views/PatientsView.xaml** — Обновление: интерфейс со списком (DataGrid), формой добавления и поиском.
7.  **ClinicApp.Wpf/App.xaml.cs** — Обновление: регистрация репозитория в DI.
8.  **ClinicApp.Wpf/App.xaml** — Обновление: добавление `BooleanToVisibilityConverter`.

## Код ключевых файлов

### ClinicApp.Domain/Entities/Patient.cs
```csharp
public class Patient
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; } = string.Empty;
}
```

### ClinicApp.Wpf/ViewModels/PatientsViewModel.cs (фрагмент Add)
```csharp
[RelayCommand]
private async Task AddPatient()
{
    var patient = new Patient { FirstName = FirstName, LastName = LastName, Phone = Phone, DateOfBirth = DateOfBirth };
    await _patientRepository.AddAsync(patient);
    await LoadPatients();
}
```

## Действия в Visual Studio

1.  Открыть `ClinicApp.sln`.
2.  Нажать `Ctrl+Shift+B` для сборки.
3.  Нажать `F5` для запуска.
4.  Перейти в раздел "Пациенты", заполнить форму и нажать "Добавить".

## Проверка работоспособности

1.  **Build succeeded:** Решение собирается без ошибок.
2.  **CRUD:** Пациенты добавляются в список, отображаются в DataGrid и удаляются при нажатии кнопки.
3.  **Поиск:** При вводе фамилии в поле поиска и нажатии "Поиск" список фильтруется.

## Типовые ошибки и исправления

*   **Ошибка CS0118 (Application):** Возникает из-за конфликта имен между пространством имен `ClinicApp.Application` и классом `System.Windows.Application`.
    *   *Исправление:* Использовать полное имя `public partial class App : System.Windows.Application`.
*   **Отсутствие ссылок:** Если проекты не видят классы друг друга, проверьте Project References.

## Переход к следующему этапу

Сборка гарантирована (Build Succeeded). Переходим к Этапу 5 — CRUD Doctors + справочники.
