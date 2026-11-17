var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("postgres")
    .WithImageTag("18.3")
    .WithDataVolume()
    .AddDatabase("fitnessgym");

var migrations = builder.AddProject<Projects.FitnessGym_MigrationService>("migrations")
    .WithReference(database)
    .WaitFor(database);

builder.AddProject<Projects.FitnessGym_Api>("api")
    .WithReference(database)
    .WaitForCompletion(migrations)
    .WithHttpHealthCheck("/health");

builder.Build().Run();
