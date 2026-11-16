var builder = DistributedApplication.CreateBuilder(args);

/// <summary>
/// Создание базы данных
/// </summary>
var mysql = builder.AddMySql("mysql");
var db = mysql.AddDatabase("cleaningdb");

/// <summary>
/// Добавляем API и связываем с БД
/// </summary>
builder.AddProject<Projects.DryCleaning_Api>("drycleaning-api")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
