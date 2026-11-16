var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var carWashDb = sqlServer.AddDatabase("CarWashDb");

builder.AddProject<Projects.CarWash_Api_Host>("carwash-api")
    .WithReference(carWashDb)
    .WaitFor(carWashDb);

builder.Build().Run();