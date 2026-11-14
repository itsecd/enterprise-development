var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql")
    .WithDataVolume()
    .WithPhpMyAdmin();

var vetDb = mysql.AddDatabase("vetdb");

builder.AddProject<Projects.Vet_Api>("vet-api")
    .WithReference(vetDb)
    .WaitFor(vetDb);

builder.Build().Run();