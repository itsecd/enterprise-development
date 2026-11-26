var builder = DistributedApplication.CreateBuilder(args);

// 1. Поднимаем контейнер MySQL с томом для сохранения данных
var mysql = builder.AddMySql("mysql")
    .WithDataVolume();

// 2. Объявляем базу данных внутри сервера
var vetDb = mysql.AddDatabase("vetclinicdb");

// 3. Передаем ссылку на БД в ваш API-проект (оставьте то имя проекта в <...>, которое уже сгенерировала студия)
builder.AddDotnetProject("vetclinic-api", "../VetClinic.Api.Host/VetClinic.Api.Host.csproj")
    .WithReference(vetDb)
    .WaitFor(vetDb);

builder.Build().Run();
