
using FitnessClub.Api.Services;
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.InMemory.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Тестовые данные фитнес-клуба.
builder.Services.AddSingleton(_ =>
    new FitnessClubDataSeeder(DateTime.Now));

// Репозитории.
builder.Services.AddSingleton<IRepository<Client>, ClientRepository>();
builder.Services.AddSingleton<IRepository<Trainer>, TrainerRepository>();
builder.Services.AddSingleton<IRepository<Specialization>, SpecializationRepository>();
builder.Services.AddSingleton<IRepository<Gym>, GymRepository>();
builder.Services.AddSingleton<IRepository<TrainingSession>, TrainingSessionRepository>();

// Сервисы.
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<TrainerService>();
builder.Services.AddScoped<SpecializationService>();
builder.Services.AddScoped<GymService>();
builder.Services.AddScoped<TrainingSessionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
