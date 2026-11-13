using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using VeterinaryClinic.Application.Services;
using VeterinaryClinic.Api;
using VeterinaryClinic.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi();
builder.Services.AddClinicRepositories();
builder.Services.AddScoped<IBreedService, BreedService>();
builder.Services.AddScoped<ISpecializationService, SpecializationService>();
builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<IVeterinarianService, VeterinarianService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();

var app = builder.Build();
ClinicSeedFactory.SeedClinicTestData(app.Services);
app.UseExceptionHandler();
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
        .CreateLogger("VeterinaryClinic.Api.Requests");
    logger.LogInformation("Запрос {Method} {Path}", context.Request.Method, context.Request.Path);
    await next(context);
    logger.LogInformation("Ответ {Method} {Path}: {StatusCode}",
        context.Request.Method, context.Request.Path, context.Response.StatusCode);
});
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapControllers();
app.Run();
