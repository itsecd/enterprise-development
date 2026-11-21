# DryCleaner — Лабораторная работа 2

REST API для химчистки на ASP.NET Core (.NET 10). Данные хранятся в памяти.

## Структура

- `DryCleaner.Domain` — сущности, перечисления, интерфейс репозитория, тестовые данные
- `DryCleaner.Application.Contracts` — DTO и интерфейсы сервисов
- `DryCleaner.Application` — сервисы и мапперы
- `DryCleaner.Infrastructure.InMemory` — репозитории в памяти
- `DryCleaner.Api` — контроллеры и запуск приложения
- `DryCleaner.Tests` — тесты запросов к данным

## Возможности

- CRUD для клиентов, изделий, категорий изделий и заказов: `api/Client`, `api/Product`, `api/ProductCategory`, `api/Order`
- Аналитика `api/Analitic`:
  - `in-progress-orders` — заказы в обработке по дате приема
  - `top-active-clients` — топ 5 активных клиентов за период
  - `clients-average-processing-days` — средний срок выполнения заказов по клиентам
  - `categories-popularity` — самые и наименее популярные категории за период
  - `top-spending-client` — клиент, потративший больше всех