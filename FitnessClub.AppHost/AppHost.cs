var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume();

var db = postgres.AddDatabase("fitnessclub");

builder.AddProject<Projects.FitnessClub_Api>("api")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
