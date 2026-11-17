var builder = DistributedApplication.CreateBuilder(args);

var connectionString = builder.AddConnectionString("drycleanerdb",
    "Host=localhost;Port=5432;Database=drycleanerdb;Username=postgres;Password=123456");

builder.AddProject<Projects.DryCleaner_Api>("drycleaner-api")
    .WithReference(connectionString);

builder.Build().Run();