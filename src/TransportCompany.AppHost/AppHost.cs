var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .WithDataVolume();

var db = sqlServer.AddDatabase("TransportCompanyDb");

builder.AddProject<Projects.TransportCompany_Api>("api")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();