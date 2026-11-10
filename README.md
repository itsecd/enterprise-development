# Разработка корпоративных приложений
[Таблица с успеваемостью](https://docs.google.com/spreadsheets/d/1nHewCKHKgS9BqbfIv1GTuropMRdFEIDgqLwYDaufA-U/edit?pli=1&gid=0#gid=0)

## Лабораторная 2. «Сервер» - Реализация серверного приложения с использованием REST API
Во второй лабораторной работе необходимо реализовать серверное приложение, которое должно: - Осуществлять базовые CRUD-операции с реализованными в первой лабораторной сущностями - Предоставлять результаты аналитических запросов (раздел «Unit-тесты» задания)
Хранение данных на этом этапе допускается осуществлять в памяти в виде коллекций

### Запуск
```bash
dotnet run --project src/TransportCompany.AppHost/TransportCompany.AppHost.csproj

# либо

dotnet test
```

### Результаты
Запуск Aspire
![Запуск Aspire](assets/cli-aspire-start.png)
Панель Aspire
![Панель Aspire](assets/aspire-panel.png)
Панель Swagger
![Панель Swagger](assets/swagger-panel.png)
Пример работы одной ручки
![Пример работы одной ручки](assets/handler-response-example.png)
Прохождение тестов
![Прохождение тестов](assets/test-passing.png)