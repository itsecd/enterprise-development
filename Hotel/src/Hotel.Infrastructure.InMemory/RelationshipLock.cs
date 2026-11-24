using Hotel.Domain.Interfaces;

namespace Hotel.Infrastructure.InMemory;

/// <summary>Синхронизирует изменения связанных коллекций в памяти.</summary>
public sealed class RelationshipLock : IRelationshipLock, IDisposable
{
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task<IDisposable> AcquireAsync()
    {
        await _semaphore.WaitAsync();
        return new Releaser(_semaphore);
    }

    public void Dispose() => _semaphore.Dispose();

    private sealed class Releaser(SemaphoreSlim semaphore) : IDisposable
    {
        private SemaphoreSlim? _semaphore = semaphore;

        public void Dispose()
        {
            Interlocked.Exchange(ref _semaphore, null)?.Release();
        }
    }
}
