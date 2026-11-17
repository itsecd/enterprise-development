var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithLifetime(ContainerLifetime.Persistent);

var db = postgres.AddDatabase("drycleanerdb");

builder.AddProject<Projects.DryCleaner_Api>("drycleaner-api")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();