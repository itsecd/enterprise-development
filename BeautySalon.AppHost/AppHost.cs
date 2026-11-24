var builder = DistributedApplication.CreateBuilder(args);

// Добавляем MS SQL Server
var sqlServer = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume();

// Создаем базу данных внутри сервера
var database = sqlServer.AddDatabase("beauty-salon-db");

// Добавляем наш сервер приложений и связываем его с БД
var server = builder.AddProject<Projects.BeautySalon_Server>("beauty-salon-server")
    .WithReference(database);
    //.WaitFor(database);

builder.Build().Run();