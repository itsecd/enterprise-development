# Разработка корпоративных приложений

## Лабораторная работа №3

Вариант 60: Предметная область — гостиница

## Используемые технологии

- C#
- .NET 10
- ASP.NET Core
- Entity Framework Core
- MySQL
- .NET Aspire
- Docker
- xUnit v3
- LINQ
- Scalar
- GitHub Actions

## Ход работы

Для выполнения работы к ранее созданным проектам в [л/р №1](https://github.com/DANILA21-3/enterprise-development/tree/lab1) и [л/р №2](https://github.com/DANILA21-3/enterprise-development/tree/lab2) были дополнительно созданы следующие проекты:

- **Hotel.Infrastructure.Database** — для работы с базой данных MySQL через Entity Framework Core (контекст БД, миграции и репозиторий)
- **Hotel.AppHost** — оркестратор на базе Aspire для запуска контейнера с MySQL и сервера API

## Что было сделано

На основе второй лабораторной работы хранение данных было переведено из оперативной памяти в полноценную реляционную базу данных *MySQL*. При этом старая реализация `Hotel.Infrastructure.InMemory` была сохранена, чтобы ранее написанные тесты продолжали функционировать.

### 1. Контекст базы данных и репозиторий (EF Core)

В проекте `Hotel.Infrastructure.Database` реализованы следующие компоненты:

- `HotelDbContext` — контекст базы данных гостиницы для связи сущностей из `Hotel.Domain` с таблицами в MySQL.
- `EfRepository<T>` — обобщенный репозиторий, реализующий интерфейс `IRepository<T>` для выполнения CRUD-операций и фильтрации в БД.
- `DependencyInjection` — регистрация зависимостей слоя базы данных (`AddDatabaseInfrastructure`).

### 2. Миграции и начальное заполнение (Data Seeding)

С помощью инструментов Entity Framework Core создана начальная миграция, которая формирует структуру таблиц для сущностей `Clients`, `RoomTypes`, `Rooms`, `Bookings` и автоматически заполняет их тестовыми данными из `HotelSeed.cs`:
- 10 типов номеров
- 15 номеров
- 10 клиентов
- 20 бронирований

При запуске сервера `Hotel.API` автоматически таблицы и начальные данные разворачиваются в контейнере MySQL.

### 3. Оркестрация через .NET Aspire

В проекте `Hotel.AppHost` настроено совместное развертывание базы данных и веб-сервера:
- Поднимается контейнер с MySQL и создается база данных `hoteldb`.
- Запускается проект `Hotel.API` для обработки запросов пользователей и отображения Scalar.

## Структура проекта

В конце работы получены проекты со следующей структурой:
<p align="center">
  Скриншот №1:<br>
  <img width="278" height="1120" alt="image" src="https://github.com/user-attachments/assets/33b415d6-7ca5-46b7-b72c-77f8d36d21f3" />
  <br><br>
  Скриншот №2:<br>
  <img width="356" height="1114" alt="image" src="https://github.com/user-attachments/assets/cb1d0aa7-b42c-4967-a697-30b27b712151" />
  <br><br>
  Скриншот №3:<br>
  <img width="214" height="148" alt="image" src="https://github.com/user-attachments/assets/09b54a68-b5cc-4231-b0be-ae572d8da502" />
  <br><br>
</p>

## Инструкция к запуску

Для запуска проекта необходимо:

1. Установить **.NET SDK 10** и **Docker Desktop** (скачать можно с официального сайта).
2. Запустить **Docker Desktop** и дождаться, пока он полностью прогрузится. В левом нижнем углу его можно запустить и убедиться, что он работает:

<p align="center">
<img width="418" height="34" alt="image" src="https://github.com/user-attachments/assets/5add306c-4a39-4ed8-b875-a230d724c7eb" />

</p>

3. Открыть солюшен `Hotel.slnx` в `Visual Studio`, выбрать стартовым проектом **`Hotel.AppHost`** и нажать **F5**.
4. После сборки в браузере откроется страница **.NET Aspire**. Нужно подождать несколько секунд, пока `mysql`, `hoteldb` и `hotel-api` перейдут в зеленый статус `Running`.

<p align="center">
<img width="1274" height="323" alt="image" src="https://github.com/user-attachments/assets/6e21484b-bd4d-4a31-93ee-6985f16dcac9" />
</p>

5. Нажать на кликабельную ссылку напротив `hotel-api`. Откроется интерфейс **Scalar**, где можно отправлять запросы к API и убедиться, что данные загружаются из базы данных.

<p align="center">
<img width="1837" height="603" alt="image" src="https://github.com/user-attachments/assets/1fd3a8ec-b434-416e-839e-027af6cd33bd" />
</p>

