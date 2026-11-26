var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume();

var db = sql.AddDatabase("drycleaning");

var kafka = builder.AddKafka("drycleaning-kafka")
    .WithKafkaUI();

var kafkaTopic = builder.AddParameter("KafkaTopic");
var batchSize = builder.AddParameter("GeneratorBatchSize");
var payloadLimit = builder.AddParameter("GeneratorPayloadLimit");
var waitTime = builder.AddParameter("GeneratorWaitTime");

builder.AddProject<Projects.DryCleaning_Generator>("generator")
    .WithEnvironment("Kafka__TopicName", kafkaTopic)
    .WithEnvironment("Generator__BatchSize", batchSize)
    .WithEnvironment("Generator__PayloadLimit", payloadLimit)
    .WithEnvironment("Generator__WaitTime", waitTime)
    .WithReference(kafka)
    .WaitFor(kafka);

builder.AddProject<Projects.DryCleaning_Api>("api")
    .WithReference(db)
    .WithReference(kafka)
    .WaitFor(db)
    .WaitFor(kafka);

builder.Build().Run();