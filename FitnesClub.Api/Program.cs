using FitnesClub.Api.Storage;
using FitnesClub.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRepository<Trainer, Guid>, InMemoryTrainerRepo>();
builder.Services.AddSingleton<IRepository<Member, Guid>, InMemoryMemberRepo>();
builder.Services.AddSingleton<IRepository<Booking, Guid>, InMemoryBookingRepo>();
builder.Services.AddSingleton<IRepository<Specialization, Guid>, InMemorySpecializationRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
