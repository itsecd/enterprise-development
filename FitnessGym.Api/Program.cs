using FitnessGym.Application.Services;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Interfaces;
using FitnessGym.Infrastructure.InMemory.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IClientRepository, ClientRepository>();
builder.Services.AddSingleton<ITrainerRepository, TrainerRepository>();
builder.Services.AddSingleton<ISpecializationRepository, SpecializationRepository>();
builder.Services.AddSingleton<ITrainingSessionRepository, TrainingSessionRepository>();

builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<ITrainerService, TrainerService>();
builder.Services.AddSingleton<ISpecializationService, SpecializationService>();
builder.Services.AddSingleton<ITrainingSessionService, TrainingSessionService>();
builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("SecretKey не найден");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Введите JWT токен",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();