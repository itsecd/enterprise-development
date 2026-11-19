var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.AutoService_Api>("autoservice-api");

builder.Build().Run();
