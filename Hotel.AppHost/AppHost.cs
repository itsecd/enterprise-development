var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql");
var hotelDb = mysql.AddDatabase("hoteldb");

builder.AddDotnetProject("hotel-api", "../Hotel.API/Hotel.API.csproj")
    .WithReference(hotelDb)
    .WaitFor(hotelDb)
    .WithUrlForEndpoint("https", url =>
    {
        url.DisplayText = "Scalar API";
        url.Url = "/scalar/v1";
    });

builder.Build().Run();
