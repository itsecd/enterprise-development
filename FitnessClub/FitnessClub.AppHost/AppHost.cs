var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("postgres")
    .WithImageTag("17-alpine")
    .WithDataVolume()
    .AddDatabase("fitnessclub");

builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
