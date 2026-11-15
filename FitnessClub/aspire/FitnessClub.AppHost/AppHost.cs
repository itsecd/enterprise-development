var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql");
var db = mysql.AddDatabase("fitnessclub-db");

var migrations = builder
    .AddProject<Projects.FitnessClub_MigrationService>("fitnessclub-migrations")
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api")
    .WithHttpEndpoint(name: "http")
    .WithUrl("/scalar/v1", "Scalar")
    .WithHttpHealthCheck("/health")
    .WithReference(db)
    .WaitFor(db)
    .WaitForCompletion(migrations);

builder.Build().Run();
