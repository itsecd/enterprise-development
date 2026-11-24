var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithImageTag("18.3")
    .WithImagePullPolicy(ImagePullPolicy.Never)
    .WithLifetime(ContainerLifetime.Persistent);

var db = postgres.AddDatabase("drycleanerdb");

builder.AddProject<Projects.DryCleaner_Api>("drycleaner-api")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();