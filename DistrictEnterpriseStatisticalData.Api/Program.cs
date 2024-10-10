using DistrictEnterpriseStatisticalData.Api;
using DistrictEnterpriseStatisticalData.Api.Service;
using DistrictEnterpriseStatisticalData.Domain;
using DistrictEnterpriseStatisticalData.Domain.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Mapper));

builder.Services.AddDbContext<DistrictDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddTransient<EnterpriseService>();
builder.Services.AddTransient<EnterpriseTypeService>();
builder.Services.AddTransient<FormOfOwnershipService>();
builder.Services.AddTransient<SupplierService>();
builder.Services.AddTransient<SupplyService>();

builder.Services.AddTransient<EnterpriseRepository>();
builder.Services.AddTransient<EnterpriseTypeRepository>();
builder.Services.AddTransient<FormOfOwnershipRepository>();
builder.Services.AddTransient<SupplierRepository>();
builder.Services.AddTransient<SupplyRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();