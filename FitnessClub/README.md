# FitnessClub — лабораторная работа №3

Вадим Петров, группа 6412, вариант 22. Фитнес-клуб, PostgreSQL.

Приложение на .NET 10 предоставляет CRUD для клиентов, тренеров, специализаций,
залов и тренировок. Аналитика выполняется запросами EF Core к PostgreSQL.
Контракты и Application не зависят от реализации хранилища.

## Запуск

Установите .NET 10 и запустите Docker Desktop с Linux-контейнерами.
Из этой папки выполните:

```shell
dotnet restore FitnessClub.slnx
dotnet build FitnessClub.slnx
dotnet test FitnessClub.Tests/FitnessClub.Tests.csproj
dotnet run --project FitnessClub.AppHost
```

Откройте Dashboard по адресу из терминала, затем HTTP-адрес ресурса
fitnessclub-api и путь /scalar/v1. Схема доступна по /openapi/v1.json.
Адрес API при запуске через Aspire может отличаться от localhost:5182.

Aspire запускает PostgreSQL 17, сохраняет данные в Docker volume,
передаёт строку подключения API и ожидает готовности базы.
API применяет миграции перед обработкой запросов.
При повторном запуске данные сохраняются, начальные записи повторно не добавляются.

Для отдельного запуска API задайте переменную окружения
ConnectionStrings__fitnessclub с вашей строкой PostgreSQL и выполните
`dotnet run --project FitnessClub.Api --launch-profile http`.

## База данных

Проект FitnessClub.Infrastructure.EfCore содержит DbContext, конфигурации,
репозитории и миграции. У клиентов и тренеров отдельные таблицы.
Связи заданы явными ClientId, TrainerId, HallId и SpecializationId.
Удаление используемой записи запрещено внешним ключом и возвращает HTTP 409.
Сохранение выполняется через SaveChangesAsync.

Начальная миграция создаёт таблицы и добавляет по 10 записей каждой сущности.
Даты начального набора фиксированы в ноябре 2026 года для воспроизводимости
миграций. Поэтому результат аналитики текущего месяца зависит от даты запуска.
Время занятий принимается с часовым смещением (DateTimeOffset) и хранится в UTC (timestamp with time zone). Текущий месяц аналитики определяется по UTC. Даты рождения и абонемента хранятся как date.

## Проверка API

В Scalar доступны разделы Clients, Trainers, Halls, Specializations,
Trainings и Analytics. Примеры запросов находятся в FitnessClub.Api.http.

- GET /api/clients — список клиентов.
- GET /api/analytics/experienced-trainers — стаж от пяти лет.
- GET /api/analytics/halls/1/availability — доступность зала сейчас.
- GET /api/analytics/expired-subscriptions — просроченные абонементы.
- GET /api/analytics/halls/1/current-month-trainings — занятия зала за текущий месяц.
- GET /api/analytics/top-trainers — до пяти наиболее популярных тренеров.

Создание возвращает 201, обновление и удаление — 204, отсутствующая запись — 404.
Неверные поля DTO возвращают 400. Проверяются обязательные поля, длины строк,
пол, положительные идентификаторы, длительность, дата рождения и даты абонемента.
Конфликты связей и конкурентного изменения возвращают 409.
Неожиданные ошибки обрабатываются стандартным обработчиком ASP.NET Core.
Журналируются метод, путь и статус запроса.

## Миграции

Установите dotnet-ef версии 10.0.12 и задайте ConnectionStrings__fitnessclub. Команды из этой папки:

```shell
dotnet ef migrations add MigrationName --project FitnessClub.Infrastructure.EfCore
dotnet ef migrations has-pending-model-changes --project FitnessClub.Infrastructure.EfCore
```

Для применения миграций через CLI задайте ConnectionStrings__fitnessclub и
выполните `dotnet ef database update --project FitnessClub.Infrastructure.EfCore`.
Обычный запуск API применяет миграции автоматически.
