var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var hotelDb = postgres.AddDatabase("hoteldb");

builder.AddDotnetProject("hotel-api", "../Hotel.API/Hotel.API.csproj")
    .WithReference(hotelDb)
    .WaitFor(hotelDb)
    .WithUrlForEndpoint("https", url =>
    {
        url.DisplayText = "Scalar API";
        url.Url = "/scalar/v1";
    });

builder.Build().Run();
