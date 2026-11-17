using DryCleaner.Api;
using DryCleaner.Application.Contracts.ServiceInterfaces;
using DryCleaner.Application.Services;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Infrastructure.EfCore.Repositories;
using DryCleaner.Infrastructure.EFCore;
using DryCleaner.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<DryCleanerDbContext>("Postgres");

builder.Services.AddTransient<IRepository<ProductCategory, int>, ProductCategoryEfCoreRepository>();
builder.Services.AddTransient<IRepository<Product, int>, ProductEfCoreRepository>();
builder.Services.AddTransient<IRepository<Client, int>, ClientEfCoreRepository>();
builder.Services.AddTransient<IRepository<Order, int>, OrderEfCoreRepository>();

builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAnalyticService, AnalyticService>();

builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecurityTransformer>();
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<DryCleanerDbContext>();
    context.Database.Migrate();
}

app.MapDefaultEndpoints();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
