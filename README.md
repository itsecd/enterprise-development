# FitnessClub — лабораторная работа 3

Серверное приложение фитнес-клуба на ASP.NET Core с хранением данных в MySQL. В рамках третьей лабораторной работы InMemory-хранилище заменено на Entity Framework Core, добавлены миграции, начальное заполнение базы данных и оркестрация через .NET Aspire.

## Технологии

- .NET 10 и ASP.NET Core;
- Entity Framework Core с Pomelo.EntityFrameworkCore.MySql;
- MySQL 9.7;
- .NET Aspire 13.6;
- xUnit v3;
- Scalar и OpenAPI.

## Архитектура

```text
FitnessClub
├── aspire
│   └── FitnessClub.AppHost                 # Запуск MySQL, миграций и API
├── src
│   ├── FitnessClub.Api                     # REST API и OpenAPI
│   ├── FitnessClub.Application             # Сервисы, валидация и маппинг
│   ├── FitnessClub.Contracts               # DTO и контракты сервисов
│   ├── FitnessClub.Domain                  # Сущности и начальные данные
│   ├── FitnessClub.Infrastructure.EfCore   # DbContext, репозитории и миграции
│   ├── FitnessClub.MigrationService        # Одноразовое применение миграций
│   └── FitnessClub.Shared                  # Общие типы
└── tests
    └── FitnessClub.Application.Tests       # Модульные тесты
```

### Модель базы данных

Для иерархии `Person → Client/Trainer` используется стратегия TPH: клиенты и тренеры хранятся в общей таблице `people` с дискриминатором. Залы, специализации и бронирования находятся в таблицах `halls`, `specializations` и `bookings`.

Начальные данные берутся из `FitnessClub.Domain/Seeds/SeedData.cs` и добавляются через `UseAsyncSeeding` при создании базы данных. EF Core самостоятельно сохраняет связанный граф сущностей и устанавливает внешние ключи.

## Требования

Перед запуском необходимо установить:

- [.NET 10 SDK](https://dotnet.microsoft.com/download);
- [Docker Desktop](https://www.docker.com/products/docker-desktop/);
- [Aspire CLI](https://aspire.dev/reference/cli/install-script/).

## Запуск

1. Запустить Docker Desktop.
2. Открыть терминал в каталоге `FitnessClub`.
3. Выполнить:

```powershell
aspire run
```

Aspire скачает образ MySQL при необходимости и запустит ресурсы в следующем порядке:

```text
MySQL → MigrationService → API
```

`MigrationService` применяет миграции, выполняет начальное заполнение созданной базы и завершает работу. API запускается только после успешного завершения миграций.

Адрес Aspire Dashboard отображается в терминале. В нём доступны состояние ресурсов, журналы и endpoints.

## Документация API

После запуска перейти по адресу API:

- Scalar: `/scalar/v1`;
- OpenAPI JSON: `/openapi/v1.json`;
- Health check: `/health`.

Точный порт API указан в Aspire Dashboard.

API поддерживает CRUD-операции для клиентов, тренеров, специализаций, залов и бронирований, а также аналитические запросы:

- тренеры с заданным минимальным стажем;
- клиенты с просроченными абонементами;
- самые популярные тренеры;
- доступность зала в указанное время;
- бронирования зала за указанный месяц.

## Работа с миграциями

Миграции находятся в `src/FitnessClub.Infrastructure.EfCore/Migrations` и применяются автоматически при запуске через Aspire.

`FitnessClubDbContextFactory` позволяет EF Core Tools создавать контекст во время проектирования без запуска Aspire и подключения к MySQL. Во время обычного запуска приложения эта фабрика не используется.

## Сборка и тестирование

Из каталога `FitnessClub`:
```powershell
dotnet build FitnessClub.slnx
dotnet test tests/FitnessClub.Application.Tests/FitnessClub.Application.Tests.csproj
```
