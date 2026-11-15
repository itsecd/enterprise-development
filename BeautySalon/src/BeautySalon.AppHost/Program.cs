var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume();

var salonDb = postgres.AddDatabase("salon");

builder.AddProject<Projects.BeautySalon_Api>("beautysalon-api")
    .WithUrl("/scalar", "Scalar")
    .WithReference(salonDb)
    .WaitFor(salonDb)
    .WithHttpHealthCheck("/health");

builder.Build().Run();