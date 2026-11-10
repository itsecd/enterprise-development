# Лабораторная работа 2. «Сервер» — салон красоты

**ФИО:** _Фамилия Имя Отчество_  
**Группа:** _XXXX_  
**Вариант:** _XX — салон красоты_

REST API на ASP.NET Core (.NET 10) для управления клиентами, мастерами, услугами и записями на приём, плюс аналитические запросы из первой лабораторной. Данные хранятся в памяти (коллекции) и при перезапуске возвращаются к исходному набору. Система запускается через оркестратор **.NET Aspire**.

## Запуск

Требуется .NET SDK 10. Из корня репозитория (запускаем через Aspire AppHost, а не Api напрямую):

```sh
cd BeautySalon
dotnet restore BeautySalon.slnx
dotnet run --project src/BeautySalon.AppHost
```

AppHost поднимает Aspire Dashboard и сервис `beautysalon-api` (в дашборде рядом с ним есть ссылка на Scalar). Документация API открывается через Scalar: `http://localhost:5000/scalar`, описание OpenAPI — `/openapi/v1.json` (только в среде `Development`).

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
│   ├── BeautySalon.Api/                      контроллеры, обработчик исключений, Scalar
│   └── BeautySalon.AppHost/                  Aspire — точка запуска
└── tests/
    ├── BeautySalon.Tests/                    unit-тесты сервисов и аналитики
    └── BeautySalon.Api.Tests/                HTTP-тесты API (WebApplicationFactory)
```

Зависимости направлены внутрь: Shared не знает ни о ком; Domain и Contracts знают только о Shared; Application знает о Domain и Contracts; Infrastructure.InMemory — о Domain; Api — о Contracts и Application (и о Infrastructure.InMemory только для регистрации в DI); AppHost — об Api. Контроллеры тонкие: принимают и возвращают DTO и вызывают сервисы. Сервисы бросают исключения (`NotFoundException`, `ReferenceNotFoundException`), которые в одном месте (`ApplicationExceptionHandler`) превращаются в ответы 404 и 400; неизвестные ошибки дают стандартный ответ 500 без текста исключения. У каждой сущности свой сервис и свой маппер (`ToDto`, `ToEntity`, `UpdateEntity`). Замена хранилища сводится к замене вызова `AddInMemoryStorage()` в `Program.cs`.

## API

CRUD для `/clients`, `/masters`, `/services`, `/appointments`:

| Метод | Путь | Успех | Ошибки |
| --- | --- | --- | --- |
| GET | `/{resource}` | 200, список | — |
| GET | `/{resource}/{id}` | 200 | 404 |
| POST | `/{resource}` | 201 + `Location` | 400 |
| PUT | `/{resource}/{id}` | 200, обновлённый объект | 400, 404 |
| DELETE | `/{resource}/{id}` | 204 | 404 |

Идентификатор назначает сервер. Входные данные проверяются атрибутами валидации DTO. Запись на приём со ссылкой на несуществующего клиента, мастера или услугу даёт 400. Перечисления передаются строками (`"Female"`, `"NailArtist"`, `"Hair"`).

Аналитика (раздел «Unit-тесты» задания):

| Запрос | Пример |
| --- | --- |
| Мастера со стажем от N лет (по умолчанию 5) | `GET /analytics/experienced-masters?minExperience=5` |
| Свободные окна мастера на дату (рабочий день 09:00–18:00) | `GET /analytics/masters/1/free-slots?date=2024-06-01` |
| Топ популярных услуг | `GET /analytics/popular-services?top=5` |
| Клиенты с повторными записями за месяц | `GET /analytics/repeat-clients?year=2024&month=6` |
| Клиенты, записанные к нескольким мастерам, по дате рождения | `GET /analytics/clients-with-multiple-masters` |

Начальный набор данных: по 10 клиентов, мастеров, услуг и записей. Все персональные данные вымышлены.

## Тесты

Тесты запускаются в GitHub Actions при каждом push и pull request (`.github/workflows/tests.yml`).