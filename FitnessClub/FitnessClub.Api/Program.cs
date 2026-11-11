using System.Text.Json.Serialization;
using FitnessClub.Application;
using FitnessClub.Contracts.Dtos;
using FitnessClub.Infrastructure.InMemory;
using Microsoft.AspNetCore.HttpLogging;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInMemoryInfrastructure();
builder.Services.AddApplication();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<GenderDto>()));
builder.Services.AddProblemDetails();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestMethod
        | HttpLoggingFields.RequestPath
        | HttpLoggingFields.ResponseStatusCode;
    options.CombineLogs = true;
});
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseHttpLogging();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => options.DisableAgent());
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
