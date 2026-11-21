# Разработка корпоративных приложений
[Таблица с успеваемостью](https://docs.google.com/spreadsheets/d/1nHewCKHKgS9BqbfIv1GTuropMRdFEIDgqLwYDaufA-U/edit?pli=1&gid=0#gid=0)

## Лабораторная 3. «ORM» - Реализация объектно-реляционной модели. Подключение к базе данных и настройка оркестрации
В третьей лабораторной работе хранение должно быть переделано c инмемори коллекций на базу данных. Должны быть созданы миграции для создания таблиц в бд и их первоначального заполнения.
Также необходимо настроить оркестратор Aspire на запуск сервера и базы данных.

Вариант: Транспортная компания
БД: MS SQL Server

### Запуск
```bash
dotnet run --project src/TransportCompany.AppHost/TransportCompany.AppHost.csproj

# либо

dotnet test
```

### Результаты
Запуск Aspire
![Запуск Aspire](assets/aspire-run.png)