using Scalar.AspNetCore;
using BeautySalon.Domain;
using BeautySalon.Domain.Entities;
using BeautySalon.Infrastructure.InMemory;
using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Clients;
using BeautySalon.Application.Contracts.Masters;
using BeautySalon.Application.Contracts.Services;
using BeautySalon.Application.Contracts.Appointments;
using BeautySalon.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IRepository<Client, int>, ClientInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Master, int>, MasterInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Service, int>, ServiceInMemoryRepository>();
builder.Services.AddSingleton<IRepository<Appointment, int>, AppointmentInMemoryRepository>();

builder.Services.AddScoped<IApplicationService<ClientDto, ClientCreateUpdateDto, int>, ClientService>();
builder.Services.AddScoped<IApplicationService<MasterDto, MasterCreateUpdateDto, int>, MasterService>();
builder.Services.AddScoped<IApplicationService<ServiceDto, ServiceCreateUpdateDto, int>, ServiceService>();
builder.Services.AddScoped<IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int>, AppointmentService>();

builder.Services.AddScoped<IAnalyticService, AnalyticService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
