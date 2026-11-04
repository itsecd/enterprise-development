var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Vet_Api>("vet-api");

builder.Build().Run();
