var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .WithDataVolume(); 

var db = sqlServer.AddDatabase("FitnesClubDb");

builder.AddProject<Projects.FitnesClub_Api>("api")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();