using DistrictEnterpriseStatisticalData.Domain.Repository;
using DotNet.Testcontainers.Builders;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace DistrictEnterpriseStatisticalData.Domain.Test;

public class TestDB : IAsyncLifetime
{
    private DistrictDbContext Context { get; set; } = null!;
    
    public EnterpriseRepository EnterpriseRepository { get; set; } = null!;
    
    public EnterpriseTypeRepository EnterpriseTypeRepository { get; set; } = null!;
    
    public FormOfOwnershipRepository FormOfOwnershipRepository { get; set; } = null!;
    
    public SupplierRepository SupplierRepository { get; set; } = null!;
    
    public SupplyRepository SupplyRepository { get; set; } = null!;
    
    private const string Name = "TestDB";
    
    private const string Username = "user";
    
    private const string Password = "password";
    
    private const int Port = 5432;
    
    public DataProvider DataProvider { get; set; } = null!;
    
    private PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithExposedPort(Port)
        .WithPortBinding(Port, true)
        .WithDatabase(Name)
        .WithUsername(Username)
        .WithPassword(Password)
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(Port))
        .Build();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        Context = new DistrictDbContext(new DbContextOptionsBuilder<DistrictDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options);
        await Context.Database.EnsureCreatedAsync();
        
        EnterpriseRepository = new EnterpriseRepository(Context);
        EnterpriseTypeRepository = new EnterpriseTypeRepository(Context);
        FormOfOwnershipRepository = new FormOfOwnershipRepository(Context);
        SupplierRepository = new SupplierRepository(Context);
        SupplyRepository = new SupplyRepository(Context);
        DataProvider = new DataProvider();
        
        Context.EnterpriseType.AddRange(DataProvider.EnterpriseTypes);
        Context.Enterprise.AddRange(DataProvider.Enterprises);
        Context.FormOfOwnership.AddRange(DataProvider.FormsOfOwnership);
        Context.Supplier.AddRange(DataProvider.Suppliers);
        Context.Supply.AddRange(DataProvider.Supplies);
        await Context.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await Context.Database.EnsureDeletedAsync();
        await Context.DisposeAsync();
        await _container.DisposeAsync();
    }
}