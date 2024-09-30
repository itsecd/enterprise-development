using DispatchTransportControl.Api.Repository.Context;
using DispatchTransportControl.Api.Repository.impl;
using DotNet.Testcontainers.Builders;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace DispatchTransportControl.Domain.Test;

public class TestFixture : IAsyncLifetime
{
    private const string DatabaseName = "testdb";

    private const string DatabaseUsername = "testuser";

    private const string DatabasePassword = "testpassword";

    private const int DatabasePort = 5432;

    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
        .WithExposedPort(DatabasePort)
        .WithPortBinding(DatabasePort, true)
        .WithDatabase(DatabaseName)
        .WithUsername(DatabaseUsername)
        .WithPassword(DatabasePassword)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(DatabasePort))
        .Build();

    private TransportDbContext DbContext { get; set; } = null!;

    public DriverRepository DriverRepository { get; private set; } = null!;

    public VehicleRepository VehicleRepository { get; private set; } = null!;

    public TestDataProvider TestDataProvider { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _postgresContainer.StartAsync();

        var options = new DbContextOptionsBuilder<TransportDbContext>()
            .UseNpgsql(_postgresContainer.GetConnectionString())
            .Options;

        DbContext = new TransportDbContext(options);
        await DbContext.Database.EnsureCreatedAsync();

        DriverRepository = new DriverRepository(DbContext);
        VehicleRepository = new VehicleRepository(DbContext);

        TestDataProvider = new TestDataProvider();
        TestDataProvider.RouteAssignments
            .ForEach(d => d.StartTime = DateTime.SpecifyKind(d.StartTime, DateTimeKind.Utc));
        TestDataProvider.RouteAssignments
            .ForEach(d => d.EndTime = DateTime.SpecifyKind(d.EndTime, DateTimeKind.Utc));
        DbContext.Drivers.AddRange(TestDataProvider.Drivers);
        DbContext.VehicleModels.AddRange(TestDataProvider.VehicleModels);
        DbContext.Vehicles.AddRange(TestDataProvider.Vehicles);
        DbContext.RouteAssignments.AddRange(TestDataProvider.RouteAssignments);
        await DbContext.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.DisposeAsync();
        await _postgresContainer.DisposeAsync();
    }
}