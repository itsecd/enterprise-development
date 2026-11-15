# MuseumSystem

Корпоративное приложение для управления музеем на платформе .NET 10.

## Задание

**Вариант 20:**
*   **Доменная область:** Музей
*   **База данных:** PostgreSQL (Лаб. 3)
*   **Брокер сообщений:** RabbitMQ (Лаб. 4)

### Предметная область

В системе хранятся сведения об экскурсиях, посетителях и билетах.
*   **Экскурсия** характеризуется датой, временем начала, продолжительностью, посещаемыми выставками.
*   **Выставка** содержит сведения о названии, тематике, номере зала, дате начала и дате окончания.
*   **Посетитель** характеризуется ФИО, телефоном, датой рождения.
*   **Билет** содержит информацию об экскурсии и посетителе, тип билета и цену.

---

## Что реализовано в Лабораторной №3 (ORM и Оркестрация)

1. **Переход на PostgreSQL:** Хранение данных переведено с in-memory коллекций на реляционную БД (Entity Framework Core + Npgsql). При этом архитектура сохранена: интерфейс `IRepository<TEntity>` имеет две реализации (`InMemoryRepository` и `EfRepository`), переключение между которыми настраивается через `appsettings.json` (`UseInMemoryStorage`).
2. **Миграции и Сидинг:** Настроено соглашение об именовании `snake_case` для таблиц и колонок. Созданы миграции для генерации схемы БД и первоначального заполнения данными (`HasData`), включая связи «многие-ко-многим» через сущность `ExcursionExhibition`.
3. **Оркестратор .NET Aspire:** Добавлены проекты `AppHost` и `ServiceDefaults`. Aspire автоматически запускает PostgreSQL в Docker, передает строку подключения в API и ожидает полной готовности БД перед стартом сервера (`.WaitFor()`).
4. **Аутентификация через хэш-таблицу (Факультатив):** Реализовано потокобезопасное хранилище пользователей (`HashTableUserStore`), где пароли хранятся в виде SHA-256 хэшей. JWT-аутентификация теперь использует эту хэш-таблицу для валидации.
5. **Интеграционные тесты (Факультатив):** Создан проект `MuseumSystem.IntegrationTests` (xUnit v3). Тесты с использованием `Aspire.Hosting.Testing` поднимают оркестратор, запускают Docker, применяют миграции и проверяют HTTP-эндпоинты (включая авторизацию).

---

## Структура решения

*   **MuseumSystem.Shared** — общие перечисления (Enums), используемые в Domain и Contracts.
*   **MuseumSystem.Domain** — сущности, датасидер, интерфейсы репозиториев (`IRepository<TEntity>`).
*   **MuseumSystem.Contracts** — DTO-модели для запросов/ответов и интерфейсы сервисов.
*   **MuseumSystem.Services** — бизнес-логика, мапперы.
*   **MuseumSystem.Infrastructure.InMemory** — реализация обобщенного репозитория для хранения данных в памяти.
*   **MuseumSystem.Infrastructure.EfCore** — реализация репозитория на базе Entity Framework Core.
*   **MuseumSystem.Api** — ASP.NET Core Web API (контроллеры, DI, Scalar UI, JWT, `MuseumDbContext`).
*   **MuseumSystem.AppHost** — оркестратор .NET Aspire.
*   **MuseumSystem.ServiceDefaults** — общие настройки телеметрии и health-check для Aspire.
*   **MuseumSystem.Tests** — unit-тесты (xUnit v3).
*   **MuseumSystem.IntegrationTests** — интеграционные тесты с Aspire (xUnit v3).

---

## Запуск проекта

Для запуска требуется **Docker Desktop** (для поднятия контейнера PostgreSQL через Aspire).

**1. Запуск всей системы через оркестратор:**
```bash
dotnet run --project MuseumSystem.AppHost
```

После запуска откройте дашборд Aspire (ссылка появится в консоли)
и перейдите по ссылке на museum-api, чтобы открыть Scalar UI.

**2. Запуск Unit-тестов:**
```bash
dotnet test MuseumSystem.Tests
```

**3. Запуск Интеграционных тестов:**
```bash
cd MuseumSystem.IntegrationTests
dotnet run
```

## Документация API

Для документации используется нативный Microsoft.AspNetCore.OpenApi
и современный интерфейс Scalar.
После запуска через Aspire, перейдите по ссылке на museum-api в дашборде
(https://localhost:XXXX/scalar/v1).

## Аутентификация (Факультатив)
Реализована JWT-аутентификация.
*   **Эндпоинт для получения токена:** `POST /api/auth/login`
*	**Тестовые данные:**
	- Логин: admin, Пароль: password
	- Логин: manager, Пароль: password2
	- Логин: visitor, Пароль: password3
*   В интерфейсе Scalar нажмите кнопку **Authorize** (замок) и вставьте полученный токен.

## Основные эндпоинты

### Выставки (`/api/exhibitions`)
*   `GET /api/exhibitions` — список всех выставок
*   `GET /api/exhibitions/{id}` — выставка по ID
*   `POST /api/exhibitions` — создать выставку
*   `PUT /api/exhibitions/{id}` — обновить выставку
*   `DELETE /api/exhibitions/{id}` — удалить выставку (идемпотентно)

### Экскурсии (`/api/excursions`)
*   `GET /api/excursions` — список всех экскурсий
*   `GET /api/excursions/{id}` — экскурсия по ID
*   `POST /api/excursions` — создать экскурсию
*   `PUT /api/excursions/{id}` — обновить экскурсию
*   `DELETE /api/excursions/{id}` — удалить экскурсию

### Посетители (`/api/visitors`)
*   `GET /api/visitors` — список всех посетителей
*   `GET /api/visitors/{id}` — посетитель по ID
*   `POST /api/visitors` — создать посетителя
*   `PUT /api/visitors/{id}` — обновить посетителя
*   `DELETE /api/visitors/{id}` — удалить посетителя

### Билеты (`/api/tickets`)
*   `GET /api/tickets` — список всех билетов
*   `GET /api/tickets/{id}` — билет по ID
*   `POST /api/tickets` — создать билет
*   `PUT /api/tickets/{id}` — обновить билет
*   `DELETE /api/tickets/{id}` — удалить билет

### Аналитика (`/api/analytics`)
*   `GET /api/analytics/top-exhibitions` — топ-5 выставок по количеству посетителей
*   `GET /api/analytics/minimal-participants` — экскурсии с минимальным числом участников
*   `GET /api/analytics/theme-summary?from={date}&to={date}` — сводка по тематикам за период
*   `GET /api/analytics/excursions-in-hall?hallNumber={number}&from={date}&to={date}` — экскурсии в зале за период
*   `GET /api/analytics/visitors-of-excursion/{excursionId}` — посетители выбранной экскурсии