var builder = DistributedApplication.CreateBuilder(args);

// PostgreSQL
var postgres = builder.AddPostgres("postgres")
    .WithDataVolume();

// База данных службы доставки еды
var database = postgres.AddDatabase("fooddeliverydb");

// Запуск API с подключением к базе данных
builder.AddProject<Projects.EnterpriseDevelopment_Api>("api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
