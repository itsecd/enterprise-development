var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql");
var database = sqlServer.AddDatabase("fitnessclub");

builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api")
    .WithReference(database)
    .WaitFor(database)
    .WithUrlForEndpoint("http", url =>
    {
        url.DisplayText = "Scalar (OpenAPI UI)";
        url.Url = "/scalar";
    });

builder.Build().Run();