var builder = DistributedApplication.CreateBuilder(args);

var mysql = builder.AddMySql("mysql")
    .WithLifetime(ContainerLifetime.Persistent);

var fitnessGymDb = mysql.AddDatabase("fitnessgymdb");

builder.AddProject<Projects.FitnessGym_Api>("fitnessgym-api")
    .WithReference(fitnessGymDb)
    .WaitFor(fitnessGymDb);

builder.Build().Run();