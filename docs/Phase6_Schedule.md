# Этап 6 — Schedule + проверки конфликтов (сборка 100%)

## Цель этапа

Реализация функционала расписания приёмов. Создание сущности записи на приём, реализация логики проверки конфликтов (пересечений по времени) для врачей и разработка пользовательского интерфейса для управления расписанием.

## Файлы (создать/изменить)

1.  **ClinicApp.Domain/Entities/Appointment.cs** — Сущность записи на приём.
2.  **ClinicApp.Application/Interfaces/IAppointmentRepository.cs** — Интерфейс репозитория расписания.
3.  **ClinicApp.Infrastructure/Data/Repositories/AppointmentRepository.cs** — Реализация репозитория с логикой проверки конфликтов.
4.  **ClinicApp.Infrastructure/Data/ClinicDbContext.cs** — Обновление: добавление `DbSet<Appointment>` и связей.
5.  **ClinicApp.Wpf/ViewModels/ScheduleViewModel.cs** — ViewModel для управления расписанием.
6.  **ClinicApp.Wpf/Views/ScheduleView.xaml** — Интерфейс расписания.
7.  **ClinicApp.Wpf/Converters/Converters.cs** — Конвертеры для отображения статуса и управления кнопками.
8.  **ClinicApp.Wpf/App.xaml** — Регистрация конвертеров.
9.  **ClinicApp.Wpf/App.xaml.cs** — Регистрация репозитория и ViewModel в DI.
10. **ClinicApp.Wpf/MainWindow.xaml** — Добавление кнопки "Расписание".

## Код ключевых файлов

### Логика проверки конфликтов (AppointmentRepository.cs)
```csharp
public async Task<bool> HasConflictAsync(int doctorId, DateTime startTime, DateTime endTime, int? excludeAppointmentId = null)
{
    var query = _context.Set<Appointment>()
        .Where(a => a.DoctorId == doctorId && !a.IsCancelled);

    return await query.AnyAsync(a => 
        (startTime >= a.StartTime && startTime < a.EndTime) || 
        (endTime > a.StartTime && endTime <= a.EndTime) || 
        (startTime <= a.StartTime && endTime >= a.EndTime));
}
```

### Интерфейс (ScheduleView.xaml)
Использует `ComboBox` для выбора пациента и врача, `DatePicker` для даты и текстовые поля для времени и длительности.

## Действия в Visual Studio

1.  Открыть `ClinicApp.sln`.
2.  Нажать `Ctrl+Shift+B` для сборки.
3.  Нажать `F5` для запуска.
4.  Перейти в раздел "Расписание", выбрать пациента, врача и время. Попробовать создать две записи на одно и то же время для одного врача — система должна выдать ошибку.

## Проверка работоспособности

1.  **Build succeeded:** Решение собирается без ошибок.
2.  **Конфликты:** При попытке записи на занятое время отображается сообщение "У врача уже есть запись на это время".
3.  **Отмена:** Кнопка "Отменить" меняет статус записи на "Отменено" и освобождает время врача.

## Типовые ошибки и исправления

*   **Ошибка формата времени:** Если введено некорректное время, ViewModel выдаст ошибку "Неверный формат времени".
*   **Отсутствие данных:** Если списки пациентов или врачей пусты, запись создать невозможно. Сначала добавьте их в соответствующих разделах.

## Переход к следующему этапу

Сборка гарантирована (Build Succeeded). Переходим к Этапу 7 — Авторизация + роли.
