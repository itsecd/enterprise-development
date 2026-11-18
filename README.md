# Лабораторная работа №3 — «ORM»

В рамках третьей лабораторной работы для предметной области реализована объектно-реляционная модель, подключение к базе данных PostgreSQL и настройка оркестрации через .NET Aspire.

Хранилище переведено с InMemory-коллекций на **PostgreSQL 16+** через **Entity Framework Core 10**. Контракты слоёв Domain/Contracts/Application не изменились, замена хранилища по-прежнему сводится к одной строке в DI.

Вариант: 53

## Архитектура решения

| Проект | Назначение |
| --- | --- |
| `FitnessClub.Domain` | Сущности, enum, интерфейсы репозиториев |
| `FitnessClub.Contracts` | Read/Edit-DTO + интерфейсы сервисов |
| `FitnessClub.Application` | Реализация сервисов: маппинг сущностей в DTO, бизнес-логика, аналитические запросы |
| `FitnessClub.Infrastructure.PostgreSql` | `FitnessClubDbContext`, конфигурация модели, EF-репозитории, design-time фабрика, миграции |
| `FitnessClub.Api` | ASP.NET Core Web API: контроллеры, HTTP-маршруты и конфигурация приложения |
| `FitnessClub.ServiceDefaults` | Телеметрия Aspire: OpenTelemetry, health-checks, service discovery |
| `FitnessClub.AppHost` | оркестратор Aspire: поднимает PostgreSQL и API |
| `FitnessClub.Tests` | Юнит-тесты, адаптированные под новое хранилище |

## Модель данных

- `Trainer.SpecializationId -> Specializations.Id`, `OnDelete: Restrict`: нельзя удалить специализацию, назначенную тренеру;
- `Lesson.ClientId -> Clients.Id`, `Lesson.TrainerId -> Trainers.Id`, `OnDelete: Restrict`: нельзя удалить клиента или тренера, у которых есть записи на занятия;
- enum `Gender` хранится как `integer`;
- все `DateTime`-колонки `timestamp without time zone`: в предметной области часовые пояса не используются, JSON и seed-данные содержат даты без смещения UTC;
- ограничение длины строк задано в `OnModelCreating`.

## Запуск

Требуется установленный Docker Desktop: PostgreSQL запускается в контейнере.

```bash
dotnet run --project FitnessClub.AppHost
```

Откроется дашборд Aspire. В нём видны ресурсы `postgres`, `fitnessclub` (БД) и `api`, их логи, трейсы и health-статусы. Swagger API — по ссылке на ресурсе `api` (или `/swagger`). AppHost передаёт строку подключения в API автоматически, а `WaitFor(db)` гарантирует, что API стартует после готовности БД.

<img width="1849" height="894" alt="изображение" src="https://github.com/user-attachments/assets/4659e610-f9da-4ec0-a2e3-6465dd47babf" />

Миграции применяются автоматически при старте API (`db.Database.MigrateAsync()`): создаются 4 таблицы и выполняется первоначальное заполнение.

