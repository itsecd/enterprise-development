using EnterpriseWarehouse.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<OrganizationService>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<SupplyService>();
builder.Services.AddSingleton<CellService>();
builder.Services.AddSingleton<QueryService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();