using System.Text.Json.Serialization;
using FitnessClub.Application;
using FitnessClub.Contracts.Dtos;
using FitnessClub.Infrastructure.EfCore;
using FitnessClub.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddNpgsqlDbContext<FitnessClubDbContext>("fitnessclub");
builder.Services.AddPostgresRepositories();
builder.Services.AddApplication();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<GenderDto>()));
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<DatabaseExceptionHandler>();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestMethod
        | HttpLoggingFields.RequestPath
        | HttpLoggingFields.ResponseStatusCode;
    options.CombineLogs = true;
});
builder.Services.AddOpenApi();

var app = builder.Build();
await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FitnessClubDbContext>();
    await context.Database.MigrateAsync();
}
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
