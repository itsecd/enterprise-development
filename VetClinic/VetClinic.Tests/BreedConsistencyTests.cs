using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using VetClinic.Application.Exceptions;
using VetClinic.Application.Services;
using VetClinic.Contracts.Dto.Breeds;
using VetClinic.Contracts.Dto.Pets;
using VetClinic.Infrastructure.SqlServer;
using VetClinic.Infrastructure.SqlServer.Repositories;
using VetClinic.Shared;

namespace VetClinic.Tests;

public class BreedConsistencyTests
{
    [Fact]
    public async Task UpdateBreed_WhilePetIsBeingCreated_RejectsSpeciesChange()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var options = new DbContextOptionsBuilder<VetClinicDbContext>()
            .UseSqlite(new SqliteConnectionStringBuilder
            {
                DataSource = $"clinic-{Guid.NewGuid()}",
                Mode = SqliteOpenMode.Memory,
                Cache = SqliteCacheMode.Shared,
                DefaultTimeout = 5,
                Pooling = false
            }.ToString())
            .Options;
        await using var setup = new VetClinicDbContext(options);
        await setup.Database.OpenConnectionAsync(cancellationToken);
        await setup.Database.EnsureCreatedAsync(cancellationToken);
        var breed = await new BreedRepository(setup).CreateAsync(new()
        {
            Name = "Новая порода",
            Species = AnimalSpecies.Dog
        }, cancellationToken);

        var petSave = new PauseBeforeSave();
        var breedTransaction = new ObserveTransactionStart();
        await using var petContext = new VetClinicDbContext(
            new DbContextOptionsBuilder<VetClinicDbContext>(options).AddInterceptors(petSave).Options);
        await using var breedContext = new VetClinicDbContext(
            new DbContextOptionsBuilder<VetClinicDbContext>(options).AddInterceptors(breedTransaction).Options);
        var petService = new PetService(new PetRepository(petContext), new OwnerRepository(petContext),
            new BreedRepository(petContext), new ClinicTransaction(petContext));
        var breedService = new BreedService(new BreedRepository(breedContext), new PetRepository(breedContext),
            new ClinicTransaction(breedContext));

        var create = petService.CreateAsync(new PetEditDto
        {
            Nickname = "Рекс",
            Species = AnimalSpecies.Dog,
            BreedId = breed.Id,
            OwnerId = 1,
            BirthDate = new DateTime(2020, 1, 1),
            Weight = 10
        }, cancellationToken);

        Task change;
        try
        {
            await petSave.Reached.Task.WaitAsync(TimeSpan.FromSeconds(5), cancellationToken);
            change = Task.Run(() => breedService.UpdateAsync(breed.Id,
                new BreedEditDto { Name = breed.Name, Species = AnimalSpecies.Cat }, cancellationToken),
                cancellationToken);
            await breedTransaction.Started.Task.WaitAsync(TimeSpan.FromSeconds(5), cancellationToken);
        }
        finally
        {
            petSave.Resume.TrySetResult();
        }

        var created = await create;
        await Assert.ThrowsAsync<ConflictException>(() => change);
        await using var verification = new VetClinicDbContext(options);
        var pet = await verification.Pets.SingleAsync(p => p.Id == created.Id, cancellationToken);
        var storedBreed = await verification.Breeds.SingleAsync(b => b.Id == pet.BreedId, cancellationToken);
        Assert.Equal(storedBreed.Species, pet.Species);
    }

    private sealed class PauseBeforeSave : SaveChangesInterceptor
    {
        public TaskCompletionSource Reached { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public TaskCompletionSource Resume { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            Reached.TrySetResult();
            await Resume.Task.WaitAsync(cancellationToken);
            return result;
        }
    }

    private sealed class ObserveTransactionStart : DbTransactionInterceptor
    {
        public TaskCompletionSource Started { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public override ValueTask<InterceptionResult<DbTransaction>> TransactionStartingAsync(
            DbConnection connection, TransactionStartingEventData eventData, InterceptionResult<DbTransaction> result,
            CancellationToken cancellationToken = default)
        {
            Started.TrySetResult();
            return ValueTask.FromResult(result);
        }
    }
}
