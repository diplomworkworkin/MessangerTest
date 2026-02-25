# Этап 7 — Авторизация + роли (сборка 100%)

## Цель этапа

Реализация системы аутентификации и разграничения прав доступа. Создание сущности пользователя, сервиса авторизации, окна входа и логики отображения элементов интерфейса в зависимости от роли пользователя (Admin, Registrar, Doctor).

## Файлы (создать/изменить)

1.  **ClinicApp.Domain/Entities/User.cs** — Сущность пользователя и перечисление ролей.
2.  **ClinicApp.Application/Interfaces/IAuthService.cs** — Интерфейс сервиса авторизации.
3.  **ClinicApp.Infrastructure/Data/Repositories/AuthService.cs** — Реализация сервиса авторизации.
4.  **ClinicApp.Infrastructure/Data/ClinicDbContext.cs** — Обновление: добавление `DbSet<User>` и начальных данных (Seed Data).
5.  **ClinicApp.Wpf/ViewModels/LoginViewModel.cs** — ViewModel для окна входа.
6.  **ClinicApp.Wpf/Views/LoginView.xaml** — Интерфейс окна входа.
7.  **ClinicApp.Wpf/ViewModels/MainViewModel.cs** — Обновление: логика проверки прав и управления состоянием входа.
8.  **ClinicApp.Wpf/MainWindow.xaml** — Обновление: скрытие бокового меню до входа, разграничение кнопок по ролям.
9.  **ClinicApp.Wpf/Services/INavigationService.cs** — Обновление: добавление события `Navigated`.
10. **ClinicApp.Wpf/Converters/InverseBooleanToVisibilityConverter.cs** — Новый конвертер.

## Учетные данные по умолчанию (Seed Data)

| Логин | Пароль | Роль | Доступ |
| :--- | :--- | :--- | :--- |
| **admin** | admin | Admin | Полный доступ ко всем разделам |
| **registrar** | registrar | Registrar | Пациенты, Расписание |
| **doctor** | doctor | Doctor | Главная, Расписание (просмотр) |

## Логика разграничения прав

*   **Admin:** Видит всё (Врачи, Настройки, Пациенты, Расписание).
*   **Registrar:** Видит "Пациенты" и "Расписание".
*   **Doctor:** Видит "Главная" и "Расписание".

## Действия в Visual Studio

1.  Открыть `ClinicApp.sln`.
2.  Нажать `Ctrl+Shift+B` для сборки.
3.  Нажать `F5` для запуска.
4.  При запуске откроется окно входа. Введите `admin` / `admin` для полного доступа.

## Проверка работоспособности

1.  **Build succeeded:** Решение собирается без ошибок.
2.  **Auth:** Без ввода верных данных войти в систему невозможно.
3.  **Roles:** При входе под `registrar` кнопка "Врачи" и "Настройки" исчезают из бокового меню.

## Переход к следующему этапу

Сборка гарантирована (Build Succeeded). Переходим к Этапу 8 — Reports + экспорт CSV.
