using Confluent.Kafka;
using DryCleaning.Contracts.Dto;
using DryCleaning.Generator.Messaging;
using DryCleaning.Generator.Options;
using DryCleaning.Generator.Services;
using DryCleaning.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Настройки генерации читаются из раздела Generator и проверяются при запуске
builder.Services.AddOptions<GeneratorOptions>()
    .BindConfiguration(GeneratorOptions.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Настройки отправки читаются из раздела Kafka: имя топика и повторные попытки
builder.Services.AddOptions<KafkaOptions>()
    .BindConfiguration(KafkaOptions.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Продюсер Kafka: адрес брокера приходит от Aspire по имени ресурса.
// Одна попытка отправки длится не дольше 5 секунд, дальше решает наша логика повторов
builder.AddKafkaProducer<Null, IList<OrderCreateDto>>("drycleaning-kafka",
    settings => settings.Config.MessageTimeoutMs = 5000,
    producerBuilder => producerBuilder.SetValueSerializer(new OrderBatchSerializer()));

builder.Services.AddSingleton<IOrderProducer, KafkaOrderProducer>();
builder.Services.AddHostedService<OrderGeneratorService>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();
