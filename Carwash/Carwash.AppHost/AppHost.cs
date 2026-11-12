var builder = DistributedApplication.CreateBuilder(args);

const string dbName = "carwash";

var password = builder.AddParameter("SqlServerPassword", true);

var carwashDb = builder.AddSqlServer("sqlserver", password: password)
    .AddDatabase(dbName);

builder.AddProject<Projects.Carwash_Api>("carwash-api")
    .WithReference(carwashDb, "SqlServer")
    .WaitFor(carwashDb);

builder.Build().Run();
