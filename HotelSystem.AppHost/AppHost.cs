var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume();

var database = postgres.AddDatabase("hoteldb");

builder.AddDotnetProject(
        "hotelsystem-server",
        "../HotelSystem.Server/HotelSystem.Server.csproj")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();