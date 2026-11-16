var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddPostgres("postgres")
    .WithImageTag("17")
    .WithDataVolume()
    .AddDatabase("carwash");

builder.AddProject<Projects.CarWash_Api>("carwash-api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
