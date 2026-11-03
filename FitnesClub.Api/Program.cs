using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Interfaces;
using FitnesClub.Data.Repositories;
using FitnesClub.Api.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSingleton<IRepository<Trainer, Guid>, InMemoryTrainerRepo>();
builder.Services.AddSingleton<IRepository<Member, Guid>, InMemoryMemberRepo>();
builder.Services.AddSingleton<IRepository<Booking, Guid>, InMemoryBookingRepo>();
builder.Services.AddSingleton<IRepository<Specialization, Guid>, InMemorySpecializationRepo>();
builder.Services.AddScoped<IBookingValidationService, BookingValidationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    app.MapGet("/", () => Results.Redirect("/scalar/v1"));
    app.MapGet("/swagger", () => Results.Redirect("/scalar/v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
