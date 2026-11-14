using CarWash.Api.Errors;
using CarWash.Application.Services;
using CarWash.Contracts.Services;
using CarWash.Domain.Repositories;
using CarWash.Infrastructure.Ef;
using CarWash.Infrastructure.Ef.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => options.AllowInputFormatterExceptionMessages = false)
    .ConfigureApiBehaviorOptions(options =>
    {
        var defaultFactory = options.InvalidModelStateResponseFactory;
        options.InvalidModelStateResponseFactory = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogWarning("Ошибка проверки запроса {Method} {Path}; TraceId: {TraceId}",
                context.HttpContext.Request.Method, context.HttpContext.Request.Path,
                context.HttpContext.TraceIdentifier);
            return defaultFactory(context);
        };
    });
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("carwash")
    ?? throw new InvalidOperationException("Запустите приложение через CarWash.AppHost или задайте ConnectionStrings__carwash.");
builder.Services.AddDbContext<CarWashDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<ICarRepository, EfCarRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();
builder.Services.AddScoped<ICustomerRepository, EfCustomerRepository>();
builder.Services.AddScoped<IWashServiceRepository, EfWashServiceRepository>();

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IWashServiceService, WashServiceService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CarWashDbContext>();
    await context.Database.MigrateAsync();
}

app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();
}

app.MapControllers();
await app.RunAsync();
