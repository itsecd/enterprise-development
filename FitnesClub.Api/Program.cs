using FitnesClub.Api.Services;
using FitnesClub.Data.Context;
using FitnesClub.Data.Repositories;
using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<FitnesClubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FitnesClubDb")));

builder.Services.AddScoped<IRepository<Trainer, Guid>, EfRepository<Trainer>>();
builder.Services.AddScoped<IRepository<Member, Guid>, EfRepository<Member>>();
builder.Services.AddScoped<IRepository<Booking, Guid>, EfRepository<Booking>>();
builder.Services.AddScoped<IRepository<Specialization, Guid>, EfRepository<Specialization>>();
builder.Services.AddScoped<IBookingValidationService, BookingValidationService>();

var app = builder.Build();

if (!string.IsNullOrEmpty(builder.Configuration.GetConnectionString("FitnesClubDb")))
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<FitnesClubDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    app.MapGet("/", () => Results.Redirect("/scalar/v1"));
    app.MapGet("/swagger", () => Results.Redirect("/scalar/v1"));
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseAuthorization();
app.MapControllers();
app.MapDefaultEndpoints();

app.Run();
