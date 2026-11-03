var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase("drycleaning");

builder.AddProject<Projects.DryCleaning_Api>("api")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();