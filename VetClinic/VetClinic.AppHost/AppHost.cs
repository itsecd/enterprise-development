var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");
if (!bool.TryParse(builder.Configuration["SqlDataVolume"], out var useDataVolume) || useDataVolume)
{
    sql.WithDataVolume();
}

var database = sql.AddDatabase("vetclinic");

builder.AddProject<Projects.VetClinic_Api>("api")
    .WithReference(database)
    .WaitFor(database)
    .WithHttpHealthCheck("/health")
    .WithUrlForEndpoint("https", url =>
    {
        url.Url = "/scalar/v1";
        url.DisplayText = "Scalar";
    });

builder.Build().Run();
