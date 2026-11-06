using System.Text.Json.Serialization;
using FitnessClub.Api.ExceptionHandlers;
using FitnessClub.Application;
using FitnessClub.Infrastructure.InMemory;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi();

builder.Services.AddInMemoryStorage();
builder.Services.AddApplication();

builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();
await app.Services.SeedInMemoryStorageAsync();

app.UseExceptionHandler();
app.UseSerilogRequestLogging();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapControllers();
app.Run();