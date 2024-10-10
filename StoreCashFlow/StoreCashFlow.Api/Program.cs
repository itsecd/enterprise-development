using StoreCashFlow.Api.Service;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CustomerService>();
builder.Services.AddSingleton<ProductService>();
builder.Services.AddSingleton<ProductAvailabilityService>();
builder.Services.AddSingleton<ProductTypeService>();
builder.Services.AddSingleton<SaleService>();
builder.Services.AddSingleton<StoreService>();
builder.Services.AddSingleton<RequestService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
