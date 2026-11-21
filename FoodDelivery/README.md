# Food Delivery

Лабораторная работа 3 по дисциплине «Разработка корпоративных приложений».

## вариант

78

## предметная область

Служба доставки еды.

## Технологии

- C#
- .NET 10
- ASP.NET Core
- Entity Framework Core
- xUnit v3
- MySQL
- .NET Aspire

## Запуск

Установите .NET 10 SDK и запустите Docker. Из корня репозитория выполните:

```powershell
dotnet run --project FoodDelivery/aspire/FoodDelivery.AppHost --launch-profile http
```

В Aspire Dashboard откройте ресурс `food-delivery-api`. Документация API доступна по пути `/scalar`.

При первом запуске API применяет миграцию `InitialCreate`, которая создаёт таблицы и заполняет их начальными данными. MySQL сохраняет данные в томе Docker между запусками.
