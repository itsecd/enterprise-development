var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql")
    .WithDataVolume();
var database = mysql.AddDatabase("fooddelivery");

builder.AddProject<Projects.FoodDelivery_Api>("food-delivery-api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
