# DryCleaner — Лабораторная работа 3

**«ORM» — Реализация объектно-реляционной модели. Подключение к базе данных и настройка оркестрации**

REST API для химчистки на ASP.NET Core (.NET 10). Данные хранятся в реляционной базе данных PostgreSQL. Для взаимодействия с базой данных используется Entity Framework Core.

## Структура

- `DryCleaner.Domain` — сущности, интерфейс репозитория, тестовые данные 
- `DryCleaner.Shared` — общие данные
- `DryCleaner.Application.Contracts` — DTO и интерфейсы сервисов
- `DryCleaner.Application` — сервисы и мапперы
- `DryCleaner.Infrastructure.EfCore` — контекст базы данных, репозитории и миграции
- `DryCleaner.Api` — контроллеры эндпоинтов
- `DryCleaner.AppHost` — оркестрация приложения и базы данных с помощью .NET Aspire
- `DryCleaner.ServiceDefaults` — общие настройки сервисов Aspire
- `DryCleaner.Tests` — тесты запросов к данным

## Возможности

- CRUD для клиентов, изделий, категорий изделий и заказов:
  - `api/Client`
  - `api/Product`
  - `api/ProductCategory`
  - `api/Order`
- Аналитика `api/Analitic`:
  - `in-progress-orders` — заказы в обработке по дате приема
  - `top-active-clients` — топ 5 активных клиентов за период
  - `clients-average-processing-days` — средний срок выполнения заказов по клиентам
  - `categories-popularity` — самые и наименее популярные категории за период
  - `top-spending-client` — клиент, потративший больше всех

## Реализация ORM

- Хранение данных реализовано в PostgreSQL вместо InMemory-коллекций.
- Для взаимодействия с базой данных используется Entity Framework Core и провайдер Npgsql.
- Создан `DryCleanerDbContext` с наборами сущностей `Client`, `Product`, `ProductCategory` и `Order`.
- Настроены первичные и внешние ключи, а также связи между сущностями.
- Реализовано первоначальное заполнение базы тестовыми данными с помощью `HasData`.
- Созданы миграции EF Core для создания таблиц и первоначального заполнения базы данных.
- Настроено автоматическое применение миграций при запуске API в режиме разработки.

## Оркестрация

- Настроен проект `DryCleaner.AppHost` для централизованного запуска приложения.
- Добавлен ресурс PostgreSQL с базой данных `DryCleaner`.
- Настроена передача строки подключения из Aspire в API.
- Настроена зависимость запуска API от готовности базы данных с помощью `WaitFor`.
- Обеспечен совместный запуск API и PostgreSQL с использованием .NET Aspire Dashboard.