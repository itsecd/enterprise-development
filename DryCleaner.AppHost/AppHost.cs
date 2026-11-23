var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DryCleaner_Api>("drycleaner-api");

builder.Build().Run();