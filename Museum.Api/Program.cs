using System.Text.Json.Serialization;
using Museum.Api.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MuseumStore>();
builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();
