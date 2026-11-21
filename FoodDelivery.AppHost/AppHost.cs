var builder = DistributedApplication.CreateBuilder(args);

var postgresPassword = builder.AddParameter("postgres-password", "joepeach228", secret: true);
var postgres = builder.AddPostgres("postgres").WithPassword(postgresPassword);

var foodDeliveryDb = postgres.AddDatabase("fooddeliverydb");

builder.AddProject<Projects.FoodDelivery_Api>("fooddelivery-api")
    .WithReference(foodDeliveryDb)
    .WaitFor(foodDeliveryDb);

builder.Build().Run();