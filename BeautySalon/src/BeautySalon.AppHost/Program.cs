var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.BeautySalon_Api>("beautysalon-api")
    .WithUrl("/scalar", "Scalar");

builder.Build().Run();