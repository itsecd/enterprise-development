var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddMySql("mysql");
var hotelDb = postgres.AddDatabase("hoteldb");

var migrationService = builder.AddProject<Projects.Hotel_MigrationService>("migrationservice")
    .WithReference(hotelDb)
    .WaitFor(hotelDb);

var apiService = builder.AddProject<Projects.Hotel_WebAPI>("apiservice")
    .WithReference(hotelDb)
    .WaitForCompletion(migrationService);

builder.Build().Run();