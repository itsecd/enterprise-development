using Microsoft.Extensions.Logging.Abstractions;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Data;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Shared.Enums;
using MuseumSystem.Infrastructure.InMemory.Repositories;
using MuseumSystem.Services.Services;

namespace MuseumSystem.Tests;

public class ServiceTests
{

    // тесты для EXHIBITION

    [Fact]
    public async Task ExhibitionService_GetAll_ReturnsSeededData()
    {
        // Arrange
        var repo = new InMemoryRepository<Exhibition>(MuseumDataSeeder.GetExhibitions());
        var service = new ExhibitionService(repo, NullLogger<ExhibitionService>.Instance);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() >= 10);
    }

    [Fact]
    public async Task ExhibitionService_CreateUpdateDelete_WorksCorrectly()
    {
        // Arrange
        var repo = new InMemoryRepository<Exhibition>(MuseumDataSeeder.GetExhibitions());
        var service = new ExhibitionService(repo, NullLogger<ExhibitionService>.Instance);

        var newExhibition = new ExhibitionCreateRequest
        {
            Name = "Тестовая выставка",
            Theme = ExhibitionTheme.Science,
            HallNumber = 5,
            StartDate = new DateOnly(2026, 5, 1),
            EndDate = new DateOnly(2026, 6, 1)
        };

        // Create
        var created = await service.CreateAsync(newExhibition);
        Assert.NotNull(created);
        Assert.True(created.Id > 0);

        // Update
        var updateRequest = new ExhibitionUpdateRequest
        {
            Name = "Обновленная выставка",
            Theme = ExhibitionTheme.Art,
            HallNumber = 6,
            StartDate = new DateOnly(2026, 5, 1),
            EndDate = new DateOnly(2026, 6, 1)
        };
        var isUpdated = await service.UpdateAsync(created.Id, updateRequest);
        Assert.True(isUpdated);

        var updated = await service.GetByIdAsync(created.Id);
        Assert.NotNull(updated);
        Assert.Equal("Обновленная выставка", updated.Name);

        // Delete
        var isDeleted = await service.DeleteAsync(created.Id);
        Assert.True(isDeleted);

        var deleted = await service.GetByIdAsync(created.Id);
        Assert.Null(deleted);
    }

    // тесты для EXCURSION SERVICE

    [Fact]
    public async Task ExcursionService_GetAll_ReturnsSeededData()
    {
        // Arrange
        var exhibitionRepo = new InMemoryRepository<Exhibition>(MuseumDataSeeder.GetExhibitions());
        var exhibitions = await exhibitionRepo.GetAllAsync();
        var excursions = MuseumDataSeeder.GetExcursions(exhibitions.ToList());
        var excursionRepo = new InMemoryRepository<Excursion>(excursions);

        var service = new ExcursionService(excursionRepo, exhibitionRepo, NullLogger<ExcursionService>.Instance);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() >= 10);
    }

    [Fact]
    public async Task ExcursionService_CreateUpdateDelete_WorksCorrectly()
    {
        // Arrange
        var exhibitionRepo = new InMemoryRepository<Exhibition>(MuseumDataSeeder.GetExhibitions());
        var exhibitions = await exhibitionRepo.GetAllAsync();
        var excursions = MuseumDataSeeder.GetExcursions(exhibitions.ToList());
        var excursionRepo = new InMemoryRepository<Excursion>(excursions);

        var service = new ExcursionService(excursionRepo, exhibitionRepo, NullLogger<ExcursionService>.Instance);

        var newExcursion = new ExcursionCreateRequest
        {
            StartsAt = new DateTime(2026, 10, 10, 12, 0, 0),
            Duration = TimeSpan.FromMinutes(90),
            ExhibitionIds = new List<int> { 1, 2 } // ID из сидера
        };

        // Create
        var created = await service.CreateAsync(newExcursion);
        Assert.NotNull(created);
        Assert.True(created.Id > 0);
        Assert.Equal(90, created.Duration.TotalMinutes);

        // Update
        var updateRequest = new ExcursionUpdateRequest
        {
            StartsAt = new DateTime(2026, 10, 11, 15, 0, 0),
            Duration = TimeSpan.FromMinutes(120),
            ExhibitionIds = new List<int> { 3 }
        };
        var isUpdated = await service.UpdateAsync(created.Id, updateRequest);
        Assert.True(isUpdated);

        var updated = await service.GetByIdAsync(created.Id);
        Assert.NotNull(updated);
        Assert.Equal(120, updated.Duration.TotalMinutes);
        Assert.Single(updated.ExhibitionIds);

        // Delete
        var isDeleted = await service.DeleteAsync(created.Id);
        Assert.True(isDeleted);
    }

    // тесты для VISITOR SERVICE

    [Fact]
    public async Task VisitorService_GetAll_ReturnsSeededData()
    {
        // Arrange
        var repo = new InMemoryRepository<Visitor>(MuseumDataSeeder.GetVisitors());
        var service = new VisitorService(repo, NullLogger<VisitorService>.Instance);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() >= 10);
    }

    [Fact]
    public async Task VisitorService_CreateUpdateDelete_WorksCorrectly()
    {
        // Arrange
        var repo = new InMemoryRepository<Visitor>(MuseumDataSeeder.GetVisitors());
        var service = new VisitorService(repo, NullLogger<VisitorService>.Instance);

        var newVisitor = new VisitorCreateRequest
        {
            FullName = "Иванов Иван Иванович",
            Phone = "+79001234567",
            BirthDate = new DateOnly(1990, 1, 1)
        };

        // Create
        var created = await service.CreateAsync(newVisitor);
        Assert.NotNull(created);
        Assert.True(created.Id > 0);

        // Update
        var updateRequest = new VisitorUpdateRequest
        {
            FullName = "Петров Петр Петрович",
            Phone = "+79007654321",
            BirthDate = new DateOnly(1995, 5, 5)
        };
        var isUpdated = await service.UpdateAsync(created.Id, updateRequest);
        Assert.True(isUpdated);

        var updated = await service.GetByIdAsync(created.Id);
        Assert.NotNull(updated);
        Assert.Equal("Петров Петр Петрович", updated.FullName);

        // Delete
        var isDeleted = await service.DeleteAsync(created.Id);
        Assert.True(isDeleted);
    }

    // тесты для TICKET SERVICE

    [Fact]
    public async Task TicketService_GetAll_ReturnsSeededData()
    {
        // Arrange
        var exhibitionRepo = new InMemoryRepository<Exhibition>(MuseumDataSeeder.GetExhibitions());
        var exhibitions = await exhibitionRepo.GetAllAsync();

        var excursionRepo = new InMemoryRepository<Excursion>(MuseumDataSeeder.GetExcursions(exhibitions.ToList()));
        var excursions = await excursionRepo.GetAllAsync();

        var visitorRepo = new InMemoryRepository<Visitor>(MuseumDataSeeder.GetVisitors());
        var visitors = await visitorRepo.GetAllAsync();

        var ticketRepo = new InMemoryRepository<Ticket>(MuseumDataSeeder.GetTickets(excursions.ToList(), visitors.ToList()));

        var service = new TicketService(ticketRepo, excursionRepo, visitorRepo, NullLogger<TicketService>.Instance);

        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count() >= 10);
    }

    [Fact]
    public async Task TicketService_CreateUpdateDelete_WorksCorrectly()
    {
        // Arrange
        var exhibitionRepo = new InMemoryRepository<Exhibition>(MuseumDataSeeder.GetExhibitions());
        var exhibitions = await exhibitionRepo.GetAllAsync();

        var excursionRepo = new InMemoryRepository<Excursion>(MuseumDataSeeder.GetExcursions(exhibitions.ToList()));
        var excursions = await excursionRepo.GetAllAsync();

        var visitorRepo = new InMemoryRepository<Visitor>(MuseumDataSeeder.GetVisitors());
        var visitors = await visitorRepo.GetAllAsync();

        var ticketRepo = new InMemoryRepository<Ticket>(MuseumDataSeeder.GetTickets(excursions.ToList(), visitors.ToList()));

        var service = new TicketService(ticketRepo, excursionRepo, visitorRepo, NullLogger<TicketService>.Instance);

        var newTicket = new TicketCreateRequest
        {
            ExcursionId = 1, // ID из сидера
            VisitorId = 1,   // ID из сидера
            Type = TicketType.Adult,
            Price = 1500m
        };

        // Create
        var created = await service.CreateAsync(newTicket);
        Assert.NotNull(created);
        Assert.True(created.Id > 0);
        Assert.Equal(1500m, created.Price);

        // Update
        var updateRequest = new TicketUpdateRequest
        {
            ExcursionId = 2,
            VisitorId = 2,
            Type = TicketType.Discounted,
            Price = 800m
        };
        var isUpdated = await service.UpdateAsync(created.Id, updateRequest);
        Assert.True(isUpdated);

        var updated = await service.GetByIdAsync(created.Id);
        Assert.NotNull(updated);
        Assert.Equal(800m, updated.Price);
        Assert.Equal(TicketType.Discounted, updated.Type);

        // Delete
        var isDeleted = await service.DeleteAsync(created.Id);
        Assert.True(isDeleted);
    }

    // тесты для ANALYTICS SERVICE

    [Fact]
    public async Task AnalyticsService_GetTop5Exhibitions_ReturnsFiveItems()
    {
        // Arrange
        var exhibitionRepo = new InMemoryRepository<Exhibition>(MuseumDataSeeder.GetExhibitions());
        var exhibitions = await exhibitionRepo.GetAllAsync();

        var excursionRepo = new InMemoryRepository<Excursion>(MuseumDataSeeder.GetExcursions(exhibitions.ToList()));
        var excursions = await excursionRepo.GetAllAsync();

        var visitorRepo = new InMemoryRepository<Visitor>(MuseumDataSeeder.GetVisitors());
        var visitors = await visitorRepo.GetAllAsync();

        var ticketRepo = new InMemoryRepository<Ticket>(MuseumDataSeeder.GetTickets(excursions.ToList(), visitors.ToList()));

        var service = new AnalyticsService(ticketRepo, excursionRepo, NullLogger<AnalyticsService>.Instance);

        // Act
        var result = await service.GetTop5ExhibitionsByVisitorsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count());
        Assert.True(result.First().VisitorCount >= result.Last().VisitorCount);
    }

    [Fact]
    public async Task AnalyticsService_GetThemeSummary_ReturnsDataForPeriod()
    {
        // Arrange
        var exhibitionRepo = new InMemoryRepository<Exhibition>(MuseumDataSeeder.GetExhibitions());
        var exhibitions = await exhibitionRepo.GetAllAsync();

        var excursionRepo = new InMemoryRepository<Excursion>(MuseumDataSeeder.GetExcursions(exhibitions.ToList()));
        var excursions = await excursionRepo.GetAllAsync();

        var visitorRepo = new InMemoryRepository<Visitor>(MuseumDataSeeder.GetVisitors());
        var visitors = await visitorRepo.GetAllAsync();

        var ticketRepo = new InMemoryRepository<Ticket>(MuseumDataSeeder.GetTickets(excursions.ToList(), visitors.ToList()));

        var service = new AnalyticsService(ticketRepo, excursionRepo, NullLogger<AnalyticsService>.Instance);

        var from = new DateTime(2026, 1, 1);
        var to = new DateTime(2026, 12, 31);

        // Act
        var result = await service.GetThemeSummaryForPeriodAsync(from, to);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(result.Count(), result.Select(x => x.Theme).Distinct().Count());
    }

    [Fact]
    public async Task AnalyticsService_GetVisitorsOfSelectedExcursion_ReturnsSortedByName()
    {
        // Arrange
        var exhibitionRepo = new InMemoryRepository<Exhibition>(MuseumDataSeeder.GetExhibitions());
        var exhibitions = await exhibitionRepo.GetAllAsync();

        var excursionRepo = new InMemoryRepository<Excursion>(MuseumDataSeeder.GetExcursions(exhibitions.ToList()));
        var excursions = await excursionRepo.GetAllAsync();

        var visitorRepo = new InMemoryRepository<Visitor>(MuseumDataSeeder.GetVisitors());
        var visitors = await visitorRepo.GetAllAsync();

        var ticketRepo = new InMemoryRepository<Ticket>(MuseumDataSeeder.GetTickets(excursions.ToList(), visitors.ToList()));

        var service = new AnalyticsService(ticketRepo, excursionRepo, NullLogger<AnalyticsService>.Instance);

        var excursionId = 3;

        // Act
        var result = await service.GetVisitorsOfSelectedExcursionAsync(excursionId);

        // Assert
        Assert.NotNull(result);
        if (result.Count() > 1)
        {
            var names = result.Select(x => x.FullName).ToList();
            var sortedNames = names.OrderBy(n => n).ToList();
            Assert.Equal(sortedNames, names);
        }
    }
}