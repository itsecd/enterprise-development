var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");

var foodDeliveryDb = postgres.AddDatabase("fooddeliverydb");

builder.AddProject<Projects.FoodDelivery_Api>("fooddelivery-api")
    .WithReference(foodDeliveryDb)
    .WaitFor(foodDeliveryDb);

builder.Build().Run();