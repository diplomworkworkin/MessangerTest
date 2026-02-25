# Этап 0 — Предстарт

## Цель этапа

На данном этапе будет представлен финальный список требований к дипломному проекту, таблица этапов разработки, определена структура папок и проектов (Solution Structure), а также указаны точные параметры TargetFramework и свойства csproj для проектов WPF.

## Финальный список требований

Проект представляет собой программный модуль на C# WPF для автоматизации учёта пациентов и планирования приёма в медицинском учреждении. Основные функциональные требования включают:

*   **Авторизация и роли:** Система должна поддерживать три роли пользователей: Администратор (Admin), Регистратор (Registrar) и Врач (Doctor), с соответствующими правами доступа к разделам приложения.
*   **Учёт пациентов:** Полный CRUD (Create, Read, Update, Delete) функционал для управления данными пациентов, включая поиск.
*   **Учёт врачей:** CRUD функционал для управления данными врачей.
*   **Расписание приёмов:** Возможность создания записей на приём с проверкой на конфликты по времени для врачей.
*   **Посещения:** Функционал для добавления заметок по приёму пациентов.
*   **Отчёты:** Генерация отчётов за определённый период с возможностью экспорта данных в формат CSV.
*   **Настройки:** Возможность изменения темы приложения (светлая/тёмная) и сброса тестовых данных.

## Таблица этапов разработки

| Этап | Название этапа | Цель этапа | Основные задачи |
|---|---|---|---|
| 0 | Предстарт | Определение структуры проекта и требований. | Финальный список требований, таблица этапов, дерево папок/проектов, TargetFramework, свойства csproj. |
| 1 | Скелет решения (сборка 100%) | Создание базовой структуры Solution, обеспечивающей компиляцию и запуск пустого окна. | Создание проектов, добавление ссылок, настройка DI, базовый MVVM, MainWindow с текстом "ClinicApp". |
| 2 | Навигация и каркас UI (сборка 100%) | Реализация навигации между основными разделами приложения. | Shell, боковое меню, навигация между Dashboard, Patients, Settings, простая fade-анимация переходов. |
| 3 | Подключение EF Core + LocalDB (сборка 100%) | Настройка работы с базой данных через EF Core и LocalDB. | DbContext, строка подключения, миграции, кнопка "Проверить БД". |
| 4 | Domain модели + репозитории + CRUD Patients (сборка 100%) | Реализация функционала управления пациентами. | Сущности Patient, репозитории, сервисы Application, валидация, Patients list, Add/Edit/Delete. |
| 5 | CRUD Doctors + справочники (сборка 100%) | Реализация функционала управления врачами и справочниками. | Doctors list, Specialty, CRUD. |
| 6 | Schedule + проверки конфликтов (сборка 100%) | Реализация функционала расписания приёмов с проверкой конфликтов. | Создание записей, детектор конфликтов. |
| 7 | Авторизация + роли (сборка 100%) | Реализация системы авторизации и ролевой модели. | Login, роли, доступ к разделам. |
| 8 | Reports + экспорт CSV (сборка 100%) | Реализация функционала отчётов с экспортом. | Отчёт за период, фильтры, экспорт CSV. |
| 9 | UI-улучшения и анимации (сборка 100%) | Интеграция UI-библиотеки и базовых улучшений интерфейса. | MaterialDesignThemes, темы, skeleton loader, micro-interactions, empty-states. |
| 10 | Lottie и финальный полиш (сборка 100%) | Добавление сложных анимаций и финальная доработка UI. | Lottie на Splash/Login/EmptyState. |
| 11 | Тесты (сборка 100%) | Написание модульных тестов для ключевых функций. | 5–10 unit-тестов (конфликты расписания, валидация, защита от дублей). |

## Дерево папок/проектов (Solution Structure)

Solution будет иметь следующую структуру проектов, соответствующую Clean Architecture:

```
ClinicApp.sln
├── ClinicApp.Domain
│   ├── Entities
│   ├── Enums
│   ├── ValueObjects
│   └── Interfaces
├── ClinicApp.Application
│   ├── Services
│   ├── UseCases
│   └── Interfaces
├── ClinicApp.Infrastructure
│   ├── Data
│   │   ├── Migrations
│   │   └── Repositories
│   ├── Logging
│   └── DI
├── ClinicApp.Wpf
│   ├── App.xaml
│   ├── MainWindow.xaml
│   ├── Views
│   ├── ViewModels
│   ├── Services
│   └── Converters
├── ClinicApp.Tests
│   ├── UnitTests
│   └── IntegrationTests
└── docs
    └── Phase0_Prestart.md
```

## TargetFramework и свойства csproj

Для всех проектов будет использоваться **.NET 10**.

**TargetFramework для WPF проекта:** `net10.0-windows`

**Пример свойств csproj для `ClinicApp.Wpf.csproj`:**

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net10.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UseWPF>true</UseWPF>
  </PropertyGroup>

</Project>
```

**Для остальных проектов (ClinicApp.Domain, ClinicApp.Application, ClinicApp.Infrastructure, ClinicApp.Tests):**

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

</Project>
```

**Примечание:** `ClinicApp.Tests` также будет использовать `net10.0` и соответствующие пакеты для тестирования.
