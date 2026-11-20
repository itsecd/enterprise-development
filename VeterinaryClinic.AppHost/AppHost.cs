var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume();
var database = postgres.AddDatabase("clinicdb");

builder.AddProject<Projects.VeterinaryClinic_Api>("veterinaryclinic-api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
