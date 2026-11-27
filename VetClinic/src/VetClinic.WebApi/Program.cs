using Microsoft.EntityFrameworkCore;
using VetClinic.Infrastructure.PostgreSql;
using VetClinic.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPostgreSqlRepositories(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VetClinicDbContext>();
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
