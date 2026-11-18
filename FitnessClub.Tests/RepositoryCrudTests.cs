using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.PostgreSql.Data;
using FitnessClub.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Tests;

/// <summary>
/// Интеграционные тесты CRUD-операций
/// </summary>
[Collection("PostgreSql")]
public class RepositoryCrudTests(PostgresFixture postgres)
{
    /// <summary>
    /// Проверка клиента
    /// </summary>
    [Fact]
    public async Task ClientRepository_CreateReadUpdateDelete_Roundtrip()
    {
        using var scope = await postgres.CreateScopeAsync();
        var repo = scope.ServiceProvider.GetRequiredService<IClientRepository>();

        var created = await repo.Create(new Client
        {
            PassportNumber = "4501 999901",
            FullName = "Тестов Тест Тестович",
            Gender = Gender.Male,
            BirthDate = new DateTime(1990, 6, 15),
            Phone = "+7 911 999-99-99",
            SubscriptionStart = SeedData.SeedDate.Date,
            SubscriptionEnd = SeedData.SeedDate.Date.AddMonths(3),
        });

        Assert.True(created.Id > 0);

        var loaded = await repo.Read(created.Id);
        Assert.NotNull(loaded);
        Assert.Equal("Тестов Тест Тестович", loaded!.FullName);
        Assert.Equal("+7 911 999-99-99", loaded.Phone);

        loaded.Phone = "+7 911 999-00-00";
        Assert.True(await repo.Update(loaded));

        var reloaded = await repo.Read(created.Id);
        Assert.NotNull(reloaded);
        Assert.Equal("+7 911 999-00-00", reloaded!.Phone);

        Assert.True(await repo.Delete(created.Id));
        Assert.Null(await repo.Read(created.Id));
    }

    /// <summary>
    /// Проверка занятия
    /// </summary>
    [Fact]
    public async Task LessonRepository_CreateReadDelete_WithLoadedReferences()
    {
        using var scope = await postgres.CreateScopeAsync();
        var lessons = scope.ServiceProvider.GetRequiredService<ILessonRepository>();
        var clients = scope.ServiceProvider.GetRequiredService<IClientRepository>();
        var trainers = scope.ServiceProvider.GetRequiredService<ITrainerRepository>();

        var client = (await clients.ReadAll()).First(c => c.FullName == "Орлова Марина Ивановна");
        var trainer = (await trainers.ReadAll()).First(t => t.FullName == "Смирнов Алексей Петрович");

        var created = await lessons.Create(new Lesson
        {
            Client = client,
            Trainer = trainer,
            DateStart = new DateTime(SeedData.SeedDate.Year, SeedData.SeedDate.Month, 25, 20, 0, 0),
            Hall = "Основной зал",
            IsTrial = true,
        });

        Assert.True(created.Id > 0);

        var stored = (await lessons.ReadAll()).Single(l => l.Id == created.Id);
        Assert.Equal(client.Id, stored.Client.Id);
        Assert.Equal("Смирнов Алексей Петрович", stored.Trainer.FullName);
        Assert.Equal("Плавание", stored.Trainer.Specialization.Name);

        Assert.True(await lessons.Delete(created.Id));
        Assert.Null(await lessons.Read(created.Id));
    }
}
