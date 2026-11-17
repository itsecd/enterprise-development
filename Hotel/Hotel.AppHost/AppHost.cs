var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql")
    .WithDataVolume("hotel-mysql-data");

var hotelDb = mysql.AddDatabase("HotelDb");

builder.AddProject<Projects.Hotel_Api_Host>("hotel-api")
    .WithReference(hotelDb)
    .WaitFor(hotelDb);

builder.Build().Run();
