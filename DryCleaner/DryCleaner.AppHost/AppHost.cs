var builder = DistributedApplication.CreateBuilder(args);

const string dataBaseName = "DryCleaner";

var password = builder.AddParameter("PostgresPassword", true);

var dryCleanerDb = builder.AddPostgres("postgres", password: password)
    .AddDatabase(dataBaseName);

builder.AddProject<Projects.DryCleaner_Api>("DryCleanerApi")
    .WithReference(dryCleanerDb, "Postgres")
    .WaitFor(dryCleanerDb);

builder.Build().Run();
