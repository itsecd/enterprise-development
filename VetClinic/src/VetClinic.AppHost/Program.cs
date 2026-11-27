var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("postgres")
    .WithDataVolume()
    .AddDatabase("vetclinic");

builder.AddProject<Projects.VetClinic_WebApi>("webapi", launchProfileName: "http")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
