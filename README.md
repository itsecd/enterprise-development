# Лабораторная работа №3 — ORM и оркестрация

## Цель работы

Перевести серверное веб-приложение предметной области «Фитнес-клуб» с хранения данных в оперативной памяти на реляционную СУБД **MS SQL Server** с использованием **Entity Framework Core**, реализовать миграции для создания и начального заполнения БД, настроить оркестрацию контейнеров и сервисов через **.NET Aspire**, а также покрыть приложение интеграционными тестами.

## Архитектура решения

Решение на базе **.NET 10** разделено на шесть проектов:

- `FitnesClub.Domain` — доменная модель (`Person`, `Member`, `Trainer`, `Booking`, `Specialization`, `Gender`), интерфейс `IEntity` и обобщенный контракт `IRepository<TEntity, TKey>`;
- `FitnesClub.Data` — слой доступа к данным (EF Core): контекст `FitnesClubDbContext`, универсальный репозиторий `EfRepository<TEntity>`, начальные данные `DataSeeder` и миграция `InitialCreate`;
- `FitnesClub.Api` — веб-приложение ASP.NET Core с REST-контроллерами (`Trainers`, `Members`, `Specializations`, `Bookings`, `Analytics`), сервисом валидации `BookingValidationService` и автоприменением миграций при старте (`Database.Migrate()`);
- `FitnesClub.AppHost` — оркестратор **.NET Aspire 13.6.5**, управляющий Docker-контейнером MS SQL Server (`WithDataVolume`), прокидыванием строки подключения (`WithReference`) и порядком запуска (`WaitFor`);
- `FitnesClub.ServiceDefaults` — конфигурация OpenTelemetry, логирования и проверок состояния здоровья (`HealthChecks`);
- `FitnesClub.Tests` — модульные и интеграционные тесты на базе **xUnit v3** и `Aspire.Hosting.Testing`.

## Реализованный функционал

### 1. Объектно-реляционная модель и миграции
- Отдельные `InMemory`-репозитории заменены на единый обобщенный `EfRepository<TEntity>`, зарегистрированный в DI-контейнере как `Scoped`.
- Сгенерирована миграция `InitialCreate`, создающая таблицы, связи и наполняющая БД записями из `DataSeeder` (по 10 экземпляров каждой сущности).

### 2. Оркестрация и документация API
- Настроен запуск БД и сервера через `FitnesClub.AppHost` с мониторингом в **Aspire Dashboard**.
- Подключен интерфейс **Scalar API Reference** (`/scalar/v1` с редиректом со `/swagger`).

## Тестирование

В проекте `FitnesClub.Tests` реализованы два уровня тестирования на **xUnit v3**:

1. **Модульные тесты (`FitnesTests`)** — проверка 5 аналитических LINQ-выборок в памяти на эталонных данных `FitnesFixtures` (6 тестов).
2. **Интеграционные тесты .NET Aspire (`AspireIntegrationTests`)** — программный запуск `FitnesClub.AppHost` с реальным контейнером MS SQL Server и проверка HTTP-эндпоинтов `AnalyticsController` через `HttpClient` (6 тестов):
   - выборка тренеров со стажем не менее 5 лет;
   - проверка доступности зала (свободен / занят);
   - выборка клиентов с просроченным абонементом с сортировкой по ФИО;
   - выборка занятий за выбранный месяц в выбранном зале;
   - расчет топ-5 наиболее популярных тренеров.
