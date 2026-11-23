using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("postgres-password", secret: true);
var postgres = builder.AddPostgres("postgres", password: password)
    .WithImageTag("18.3");

// В тестах данные живут только в контейнере и не используют рабочий том.
if (builder.Configuration.GetValue("UseVolumes", true))
{
    postgres.WithDataVolume("fitnessgym-postgres-data");
}

var database = postgres.AddDatabase("fitnessgym");

var migrations = builder.AddProject<Projects.FitnessGym_MigrationService>("migrations")
    .WithReference(database)
    .WaitFor(database);

builder.AddProject<Projects.FitnessGym_Api>("api")
    .WithReference(database)
    .WaitForCompletion(migrations)
    .WithHttpHealthCheck("/health");

builder.Build().Run();
