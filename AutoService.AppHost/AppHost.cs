var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("autoservice-postgres");
var database = postgres.AddDatabase("autoservice-db");

builder.AddProject<Projects.AutoService_Api>("autoservice-api")
    .WithReference(database)
    .WaitFor(database)
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Swagger UI";
        url.Url += "/swagger";
    });

builder.Build().Run();
