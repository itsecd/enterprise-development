using DryCleaning.Infrastructure.Repositories;
using DryCleaning.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, EfCoreCategoryRepository>();
builder.Services.AddScoped<IClientRepository, EfCoreClientRepository>();
builder.Services.AddScoped<IItemRepository, EfCoreItemRepository>();
builder.Services.AddScoped<IOrderRepository, EfCoreOrderRepository>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DryCleaningDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("cleaningdb"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("cleaningdb"))
    ));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DryCleaningDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
