# FitnessGym

Учебный проект по дисциплине «Разработка корпоративных приложений».

## О проекте

Приложение для управления фитнес-клубом. Включает в себя объектную модель данных и набор unit-тестов для проверки аналитических запросов.

**Вариант:** 7

- **Предметная область:** Фитнес-клуб
  - Учёт клиентов, тренеров и записей на персональные занятия.
  - Клиент характеризуется паспортными данными, ФИО, полом, датой рождения, телефоном и сроком действия абонемента.
  - Тренер имеет специализацию и стаж работы.
- **База данных:** MySQL
- **Шина данных:** gRPC

## Технологии

- **Платформа:** .NET 10
- **Тестирование:** xUnit v3
- **Язык:** C#
- **Веб-фреймворк:** ASP.NET Core Web API
- **Документация:** Swagger / OpenAPI

## Структура проекта
```text
FitnessGym/
├── FitnessGym.Domain/                  # Объектная модель данных и интерфейсы репозиториев
│   ├── Entities/                       # Сущности (Person, Client, Trainer, TrainingSession, Specialization)
│   ├── Enums/                          # Перечисления (Gender, Hall)
│   ├── Interfaces/                     # Интерфейсы репозиториев (IClientRepository и т.д.)
│   └── Data/                           # DataSeeder (тестовые данные)
├── FitnessGym.Contracts/               # DTO (Read/Edit) и интерфейсы сервисов
├── FitnessGym.Application/             # Бизнес-логика (CRUD-сервисы, AnalyticsService)
├── FitnessGym.Infrastructure.InMemory/ # Реализация репозиториев в памяти
├── FitnessGym.Api/                     # Контроллеры, Swagger, настройка DI и JWT
└── FitnessGym.Tests/                   # Unit-тесты (используют общий DataSeeder)
```

## Запуск
```bash
dotnet restore
dotnet build
dotnet run --project FitnessGym.Api
```
Открыть Swagger в браузере по адресу: http://localhost:<порт>/swagger

## Тестирование и Авторизация

- Для запуска unit-тестов: dotnet test
- Для получения JWT-токена в Swagger использовать эндпоинт POST /api/Auth/login:
  - Логин: admin
  - Пароль: password
- Скопировать полученный токен, нажать кнопку Authorize в Swagger и вставить токен