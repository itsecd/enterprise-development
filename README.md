# Лабораторная работа №2 

Учебный проект по дисциплине «Разработка корпоративных приложений». Предметная область - **фитнес-клуб**.

## Описание проекта

Реализован REST API на ASP.NET Core. Сервер поддерживает CRUD-операции и предоставляет результаты пяти аналитических запросов.

В системе используются следующие сущности:

- **Client** - клиент фитнес-клуба, включая данные абонемента.
- **Trainer** - тренер, его специализация и стаж работы.
- **Specialization** - справочник специализаций тренеров.
- **Gym** - спортивный зал.
- **TrainingSession** - запись клиента на персональное занятие.

## Технологии

- C# и .NET 10
- ASP.NET Core Web API
- LINQ
- InMemory-хранение данных в коллекциях
- xUnit v3 для unit-тестов
- OpenAPI

## Структура решения

```text
FitnessClub.slnx
├── FitnessClub.Domain
│   ├── Entities                  # сущности предметной области
│   ├── Enums                     # перечисления
│   ├── Interfaces                # IRepository<T>
│   └── Data                      # FitnessClubDataSeeder
├── FitnessClub.Infrastructure.InMemory
│   └── Repositories              # реализация хранения данных в памяти
├── FitnessClub.Api
│   ├── Controllers               # HTTP-эндпоинты
│   ├── Services                  # логика приложения и аналитика
│   ├── Dtos                      # объекты для приёма и передачи данных
│   ├── Program.cs                # настройка и запуск сервера
│   └── FitnessClub.Api.http      # примеры HTTP-запросов
└── FitnessClub.Tests
    ├── FitnessClubFixture.cs     # подготовка тестовых данных
    └── FitnessClubTests.cs       # тесты аналитических LINQ-запросов
```

## CRUD API

Для каждой из пяти сущностей реализованы операции получения списка, получения по ID, создания, изменения и удаления.

| Сущность | Базовый маршрут |
|---|---|
| Клиенты | `/api/clients` |
| Тренеры | `/api/trainers` |
| Специализации | `/api/specializations` |
| Залы | `/api/gyms` |
| Записи на занятия | `/api/training-sessions` |


## Аналитические запросы

| № | Назначение | HTTP-запрос |
|---|---|---|
| 1 | Клиенты с истёкшим абонементом, отсортированные по ФИО | `GET /api/clients/expired-subscriptions` |
| 2 | Проверка доступности выбранного зала в текущий момент | `GET /api/gyms/{id}/availability` |
| 3 | Занятия за текущий месяц в выбранном зале | `GET /api/training-sessions/current-month?gymId=1` |
| 4 | Тренеры со стажем не менее пяти лет | `GET /api/trainers/experienced` |
| 5 | Пять тренеров с наибольшим количеством записей на занятия | `GET /api/trainers/top-5` |

## Хранение данных

Данные хранятся в оперативной памяти через репозитории, реализующие интерфейс `IRepository<T>`. При запуске приложения `FitnessClubDataSeeder` создаёт исходный набор: по 10 клиентов, тренеров, специализаций и залов, а также 20 записей на занятия.

## Запуск

Требуется установленный **.NET 10 SDK**.

Из корня репозитория выполнить:

```bash
dotnet restore FitnessClub.slnx
dotnet build FitnessClub.slnx
dotnet run --project FitnessClub.Api --launch-profile https
```

При запуске с профилем `https` сервер доступен по адресу `https://localhost:7165`.

## Тестирование

Для запуска существующих unit-тестов:

```bash
dotnet test FitnessClub.slnx
```

В `FitnessClub.Tests` реализованы тесты пяти аналитических LINQ-запросов из первой лабораторной; проверка доступности зала включает два тестовых случая (всего **6**).
