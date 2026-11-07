var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FitnessClub_Api>("fitnessclub-api")
    .WithUrl("/scalar/v1", "Scalar");

builder.Build().Run();
