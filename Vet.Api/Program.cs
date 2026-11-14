using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Vet.Api;
using Vet.Application;
using Vet.Infrastructure.EfCore;
using Vet.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    var apiXmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var apiXmlPath = Path.Combine(AppContext.BaseDirectory, apiXmlFilename);
    if (File.Exists(apiXmlPath))
    {
        options.IncludeXmlComments(apiXmlPath);
    }

    var contractsXmlFilename = "Vet.Contracts.xml";
    var contractsXmlPath = Path.Combine(AppContext.BaseDirectory, contractsXmlFilename);
    if (File.Exists(contractsXmlPath))
    {
        options.IncludeXmlComments(contractsXmlPath);
    }
});

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddApplication();

var connectionString = builder.Configuration.GetConnectionString("vetdb")
    ?? "server=localhost;database=vetdb;user=root;password=dummy";

var connectionStringBuilder = new MySqlConnector.MySqlConnectionStringBuilder(connectionString)
{
    SslMode = MySqlConnector.MySqlSslMode.None,
    AllowPublicKeyRetrieval = true
};

builder.Services.AddEfCoreInfrastructure(connectionStringBuilder.ConnectionString);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<VetDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vet API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapDefaultEndpoints();

await app.RunAsync();