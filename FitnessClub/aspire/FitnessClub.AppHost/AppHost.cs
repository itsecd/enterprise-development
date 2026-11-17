var builder = DistributedApplication.CreateBuilder(args);

var mysqlPassword = builder.AddParameter("mysql-password", secret: true);
var mysql = builder.AddMySql("mysql", mysqlPassword, port: 3306)
    .WithDataVolume("fitnessclub-mysql-data");
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
