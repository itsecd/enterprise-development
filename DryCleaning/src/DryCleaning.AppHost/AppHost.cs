var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume();

var db = sql.AddDatabase("drycleaning");

var kafka = builder.AddKafka("drycleaning-kafka")
    .WithKafkaUI();

builder.AddProject<Projects.DryCleaning_Api>("api")
    .WithReference(db)
    .WithReference(kafka)
    .WaitFor(db)
    .WaitFor(kafka);

builder.Build().Run();