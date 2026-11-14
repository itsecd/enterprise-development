# Лабораторная работа 3. «ORM» — салон красоты

**ФИО:** _Астафьев Вадим Алексеевич_  
**Группа:** _6411_  
**Вариант:** _2 — салон красоты, PostgreSQL_

REST API на ASP.NET Core (.NET 10) для управления клиентами, мастерами, услугами и записями на приём, плюс аналитические запросы из первой лабораторной. Хранение данных — **PostgreSQL** через Entity Framework Core: схема и начальное заполнение создаются миграциями. Система запускается через оркестратор **.NET Aspire**, который поднимает сервер и базу данных.

## Запуск

Требуется .NET SDK 10 и Docker (контейнер PostgreSQL запускает Aspire). Из корня репозитория:

```sh
cd BeautySalon
dotnet restore BeautySalon.slnx
dotnet run --project src/BeautySalon.AppHost
```

AppHost поднимает контейнер PostgreSQL с постоянным томом, применяет к базе миграции (схема + сид) и сервис `beautysalon-api`. Документация API открывается через Scalar: `http://localhost:5000/scalar`, описание OpenAPI — `/openapi/v1.json` (только в среде `Development`).

![Scalar](docs/api.png)

## Архитектура

```
BeautySalon/BeautySalon.slnx
├── src/
│   ├── BeautySalon.Shared/                   общие перечисления (Gender, Category, Specialization)
│   ├── BeautySalon.Domain/                   сущности, сид, интерфейсы репозиториев
│   ├── BeautySalon.Contracts/                DTO (чтение / изменение) с валидацией, интерфейсы сервисов
│   ├── BeautySalon.Application/              сервисы, мапперы Domain ↔ DTO, исключения, аналитика
│   ├── BeautySalon.Infrastructure.InMemory/  репозитории поверх коллекций в памяти
│   ├── BeautySalon.Infrastructure.PostgreSQL/ DbContext, миграции (схема + сид), репозитории EF Core
│   ├── BeautySalon.Api/                      контроллеры, обработчик исключений, Scalar
│   ├── BeautySalon.ServiceDefaults/          телеметрия и проверка готовности для Aspire
│   └── BeautySalon.AppHost/                  Aspire — запуск сервера и PostgreSQL
└── tests/
    └── BeautySalon.Tests/                    unit-тесты сервисов и аналитики
```

Зависимости направлены внутрь: Shared не знает ни о ком; Domain и Contracts знают только о Shared; Application знает о Domain и Contracts; Infrastructure.InMemory и Infrastructure.PostgreSQL — о Domain; Api — о Contracts, Application и Infrastructure (только для регистрации в DI); AppHost — об Api. Контроллеры тонкие: принимают и возвращают DTO и вызывают сервисы. Сервисы бросают исключения (`NotFoundException`, `ReferenceNotFoundException`), которые в одном месте (`ApplicationExceptionHandler`) превращаются в ответы 404 и 400; неизвестные ошибки дают стандартный ответ 500 без текста исключения.

Хранилище выбирается конфигурацией `Storage:Provider` (`PostgreSQL` по умолчанию, `InMemory` — прежнее поведение лабораторной 2):

```csharp
var provider = builder.Configuration["Storage:Provider"] ?? "PostgreSQL";
```

При старте с PostgreSQL применяются неприменённые миграции (`Database.MigrateAsync()`), поэтому схема и начальные данные появляются автоматически.

### Модель базы данных

Иерархия `Person` (TPC): у клиентов и мастеров общий абстрактный базовый класс, но в базе — отдельные таблицы `clients` и `masters` со всеми полями; идентификаторы клиентов и мастеров независимы (общая последовательность `PersonSequence`).

- `clients`, `masters` — люди салона; перечисления (`Gender`, `Specialization`) хранятся строками;
- `Services` — услуги: название, категория (строка), цена `numeric(10,2)`, длительность `interval`;
- `Appointments` — записи: внешние ключи `client_id`, `master_id`, `service_id` (удаление ограничено `Restrict`), дата-время приёма, признак постоянного клиента;
- уникальный индекс `ux_appointments_master_datetime` запрещает две записи одного мастера в одно время.

Миграции:

| Миграция | Содержимое |
| --- | --- |
| `InitialSchema` | Последовательность и таблицы с внешними ключами и индексами |
| `SeedSalon` | Первоначальное заполнение данными из сида первой лабораторной (по 10 клиентов, мастеров, услуг, записей) и сдвиг счётчиков за максимальный сидированный идентификатор |

## API

CRUD для `/clients`, `/masters`, `/services`, `/appointments`:

| Метод | Путь | Успех | Ошибки |
| --- | --- | --- | --- |
| GET | `/{resource}` | 200, список | — |
| GET | `/{resource}/{id}` | 200 | 404 |
| POST | `/{resource}` | 201 + `Location` | 400 |
| PUT | `/{resource}/{id}` | 200, обновлённый объект | 400, 404 |
| DELETE | `/{resource}/{id}` | 204 | 404 |

Идентификатор назначает база данных. Входные данные проверяются атрибутами валидации DTO. Запись на приём со ссылкой на несуществующего клиента, мастера или услугу даёт 400. Перечисления передаются строками (`"Female"`, `"NailArtist"`, `"Hair"`).

Аналитика (раздел «Unit-тесты» задания):

| Запрос | Пример |
| --- | --- |
| Мастера со стажем от N лет (по умолчанию 5) | `GET /analytics/experienced-masters?minExperience=5` |
| Свободные окна мастера на дату (рабочий день 09:00–18:00) | `GET /analytics/masters/1/free-slots?date=2024-06-01` |
| Топ популярных услуг | `GET /analytics/popular-services?top=5` |
| Клиенты с повторными записями за месяц | `GET /analytics/repeat-clients?year=2024&month=6` |
| Клиенты, записанные к нескольким мастерам, по дате рождения | `GET /analytics/clients-with-multiple-masters` |

Все персональные данные вымышлены.

## Тесты

Тесты запускаются в GitHub Actions при каждом push и pull request (`.github/workflows/tests.yml`). Unit-тесты работают с in-memory реализациями репозиториев и не требуют запущенной базы данных.
