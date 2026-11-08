using System.Text.Json.Serialization;
using FitnessGym.Api.Extensions;
using FitnessGym.Api.Infrastructure;
using FitnessGym.Infrastructure.PostgreSql;
using FitnessGym.ServiceDefaults;
using Microsoft.AspNetCore.HttpLogging;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<FitnessGymDbContext>("fitnessgym");

builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi(options => options.AddSchemaTransformer<LocalDateTimeSchemaTransformer>());
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestMethod
                            | HttpLoggingFields.RequestPath
                            | HttpLoggingFields.ResponseStatusCode;
    options.CombineLogs = true;
});
builder.Services.AddPostgreSqlRepositories();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseHttpLogging();
app.UseExceptionHandler();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapControllers();
app.MapDefaultEndpoints();

app.Run();
