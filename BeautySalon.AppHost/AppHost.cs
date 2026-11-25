var builder = DistributedApplication.CreateBuilder(args);

var database = builder.AddSqlServer("sql")
    .WithDataVolume()
    .AddDatabase("salondb");

builder.AddProject<Projects.BeautySalon_Api>("api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
