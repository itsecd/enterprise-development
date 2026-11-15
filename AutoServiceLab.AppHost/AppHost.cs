var builder = DistributedApplication.CreateBuilder(args);

const string databasename = "auto-service-lab";

var password = builder.AddParameter("PostgressPassword", true);

var autoserviceDb = builder.AddPostgres("postgres", password: password)
    .AddDatabase(databasename);

builder.AddProject<Projects.AutoServiceLab_Api>("auto-service-lab-api")
    .WithReference(autoserviceDb, "Postgres")
    .WaitFor(autoserviceDb);

builder.Build().Run();
