var builder = DistributedApplication.CreateBuilder(args);

// MySQL в контейнере и база данных museum
var mySql = builder.AddMySql("mysql");
var database = mySql.AddDatabase("museum");

// Сервер запускается после того, как база данных готова
builder.AddProject<Projects.Museum_Api>("museum-api")
    .WithReference(database)
    .WaitFor(database)
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Scalar (OpenAPI UI)";
        url.Url = "/scalar";
    });

builder.Build().Run();
