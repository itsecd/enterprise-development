var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql")
    .WithImageTag("8.0.36")
    .WithDataVolume()
    .WithPhpMyAdmin();

var vetDb = mysql.AddDatabase("vetdb");

builder.AddProject<Projects.Vet_Api>("vet-api")
    .WithReference(vetDb)
    .WaitFor(vetDb);

builder.Build().Run();