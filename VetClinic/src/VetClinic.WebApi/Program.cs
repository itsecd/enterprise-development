using VetClinic.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInMemoryRepositories();
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseAuthorization();
app.MapControllers();

app.Run();