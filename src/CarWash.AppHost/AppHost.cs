var builder = DistributedApplication.CreateBuilder(args);

// MySQL в Docker-контейнере (версия 8.4 совпадает с версией в CarWashDbContext.MySqlVersion).
// phpMyAdmin — веб-интерфейс для просмотра таблиц.
var mysql = builder.AddMySql("mysql")
    .WithImageTag("8.4")
    .WithPhpMyAdmin();

// База данных внутри сервера MySQL. Имя "carwashdb" — то же, что ищет Api в строках подключения
var carWashDb = mysql.AddDatabase("carwashdb");

// Сервер получает строку подключения к БД и стартует только после того, как БД готова
builder.AddProject<Projects.CarWash_Api>("carwash-api")
    .WithReference(carWashDb)
    .WaitFor(carWashDb);

builder.Build().Run();