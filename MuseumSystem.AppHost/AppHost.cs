var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var museumDb = postgres.AddDatabase("museumdb");

var api = builder.AddProject<Projects.MuseumSystem_Api>("museum-api")
    .WithReference(museumDb)
    .WaitFor(museumDb);

builder.Build().Run();