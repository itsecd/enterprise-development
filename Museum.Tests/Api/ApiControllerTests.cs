using Microsoft.AspNetCore.Mvc;
using Museum.Api.Contracts;
using Museum.Api.Controllers;
using Museum.Api.Services;
using Museum.Domain.Data;
using Museum.Domain.Enums;

namespace Museum.Tests.Api;

/// <summary>
/// Проверяет CRUD-операции и ответы контроллеров.
/// </summary>
public class ApiControllerTests
{
    [Fact]
    public void ExhibitionCanBeCreatedUpdatedAndDeleted()
    {
        var context = DataSeeder.Seed();
        var controller = new ExhibitionsController(context);

        var request = new ExhibitionRequest
        {
            Name = "Новая выставка",
            Theme = ExhibitionTheme.History,
            HallNumber = 6,
            StartDate = new DateOnly(2026, 2, 1),
            EndDate = new DateOnly(2026, 2, 28)
        };

        var created = Assert.IsType<CreatedAtActionResult>(
            controller.Create(request).Result);

        var exhibition = Assert.IsType<ExhibitionResponse>(created.Value);

        Assert.Equal(11, exhibition.Id);
        Assert.Equal(11, context.Exhibitions.Count);

        request.Name = "Обновлённая выставка";

        Assert.IsType<NoContentResult>(
            controller.Update(exhibition.Id, request));

        var response = Assert.IsType<OkObjectResult>(
            controller.GetById(exhibition.Id).Result);

        var updated = Assert.IsType<ExhibitionResponse>(response.Value);

        Assert.Equal("Обновлённая выставка", updated.Name);

        Assert.IsType<NoContentResult>(controller.Delete(exhibition.Id));

        Assert.Equal(10, context.Exhibitions.Count);
        Assert.IsType<NotFoundResult>(
            controller.GetById(exhibition.Id).Result);
    }

    [Fact]
    public void VisitorCanBeCreatedUpdatedAndDeleted()
    {
        var context = DataSeeder.Seed();
        var controller = new VisitorsController(context);

        var request = new VisitorRequest
        {
            FullName = "Тестовый посетитель",
            Phone = "+79990000111",
            BirthDate = new DateOnly(2000, 1, 1)
        };

        var created = Assert.IsType<CreatedAtActionResult>(
            controller.Create(request).Result);

        var visitor = Assert.IsType<VisitorResponse>(created.Value);

        Assert.Equal(11, visitor.Id);

        request.FullName = "Обновлённый посетитель";

        Assert.IsType<NoContentResult>(
            controller.Update(visitor.Id, request));

        var response = Assert.IsType<OkObjectResult>(
            controller.GetById(visitor.Id).Result);

        var updated = Assert.IsType<VisitorResponse>(response.Value);

        Assert.Equal("Обновлённый посетитель", updated.FullName);

        Assert.IsType<NoContentResult>(controller.Delete(visitor.Id));

        Assert.Equal(10, context.Visitors.Count);
    }

    [Fact]
    public void TourCanBeCreatedUpdatedAndDeleted()
    {
        var context = DataSeeder.Seed();
        var controller = new ToursController(context);

        var request = new TourRequest
        {
            StartDateTime = new DateTime(2026, 2, 10, 10, 0, 0),
            Duration = TimeSpan.FromMinutes(60)
        };

        var created = Assert.IsType<CreatedAtActionResult>(
            controller.Create(request).Result);

        var tour = Assert.IsType<TourResponse>(created.Value);

        Assert.Equal(11, tour.Id);

        request.Duration = TimeSpan.FromMinutes(90);

        Assert.IsType<NoContentResult>(
            controller.Update(tour.Id, request));

        var response = Assert.IsType<OkObjectResult>(
            controller.GetById(tour.Id).Result);

        var updated = Assert.IsType<TourResponse>(response.Value);

        Assert.Equal(TimeSpan.FromMinutes(90), updated.Duration);

        Assert.IsType<NoContentResult>(controller.Delete(tour.Id));

        Assert.Equal(10, context.Tours.Count);
    }

    [Fact]
    public void TicketCanBeCreatedUpdatedAndDeleted()
    {
        var context = DataSeeder.Seed();
        var controller = new TicketsController(context);

        var request = new TicketRequest(
            1, 1, TicketType.Adult, 100m);

        var created = Assert.IsType<CreatedAtActionResult>(
            controller.Create(request).Result);

        var ticket = Assert.IsType<TicketResponse>(created.Value);

        Assert.Equal(13, ticket.Id);
        Assert.Equal(13, context.Tickets.Count);

        var updatedRequest = new TicketRequest(
            2, 2, TicketType.Concessionary, 50m);

        Assert.IsType<NoContentResult>(
            controller.Update(ticket.Id, updatedRequest));

        var response = Assert.IsType<OkObjectResult>(
            controller.GetById(ticket.Id).Result);

        var updated = Assert.IsType<TicketResponse>(response.Value);

        Assert.Equal(2, updated.TourId);
        Assert.Equal(2, updated.VisitorId);
        Assert.Equal(50m, updated.Price);

        Assert.DoesNotContain(
            context.Tours.Single(x => x.Id == 1).Tickets,
            x => x.Id == ticket.Id);

        Assert.Contains(
            context.Tours.Single(x => x.Id == 2).Tickets,
            x => x.Id == ticket.Id);

        Assert.IsType<NoContentResult>(controller.Delete(ticket.Id));

        Assert.Equal(12, context.Tickets.Count);
    }

    [Fact]
    public void TourExhibitionCanBeCreatedUpdatedAndDeleted()
    {
        var context = DataSeeder.Seed();
        var controller = new TourExhibitionsController(context);

        var created = Assert.IsType<CreatedAtActionResult>(
            controller.Create(new TourExhibitionRequest(10, 1)).Result);

        var relation = Assert.IsType<TourExhibitionResponse>(
            created.Value);

        Assert.Equal(10, relation.TourId);
        Assert.Equal(1, relation.ExhibitionId);

        Assert.IsType<NoContentResult>(
            controller.Update(
                10, 1,
                new TourExhibitionRequest(10, 2)));

        Assert.Contains(
            context.TourExhibitions,
            x => x.TourId == 10 && x.ExhibitionId == 2);

        Assert.DoesNotContain(
            context.TourExhibitions,
            x => x.TourId == 10 && x.ExhibitionId == 1);

        Assert.IsType<NoContentResult>(controller.Delete(10, 2));

        Assert.Equal(12, context.TourExhibitions.Count);
    }

    [Fact]
    public void DeletingExhibitionRemovesItsRelations()
    {
        var context = DataSeeder.Seed();
        var controller = new ExhibitionsController(context);

        Assert.IsType<NoContentResult>(controller.Delete(1));

        Assert.DoesNotContain(
            context.TourExhibitions,
            x => x.ExhibitionId == 1);

        Assert.DoesNotContain(
            context.Tours.SelectMany(x => x.TourExhibitions),
            x => x.ExhibitionId == 1);
    }

    [Fact]
    public void DeletingVisitorRemovesTheirTickets()
    {
        var context = DataSeeder.Seed();
        var controller = new VisitorsController(context);

        Assert.IsType<NoContentResult>(controller.Delete(1));

        Assert.DoesNotContain(
            context.Tickets,
            x => x.VisitorId == 1);

        Assert.Equal(10, context.Tickets.Count);
    }

    [Fact]
    public void InvalidRequestsReturnBadRequest()
    {
        var context = DataSeeder.Seed();

        var exhibitions = new ExhibitionsController(context);
        var tickets = new TicketsController(context);

        var invalidExhibition = new ExhibitionRequest
        {
            Name = " ",
            Theme = ExhibitionTheme.Art,
            HallNumber = 0,
            StartDate = new DateOnly(2026, 2, 28),
            EndDate = new DateOnly(2026, 2, 1)
        };

        Assert.IsType<BadRequestObjectResult>(
            exhibitions.Create(invalidExhibition).Result);

        Assert.IsType<BadRequestObjectResult>(
            tickets.Create(
                new TicketRequest(999, 1, TicketType.Adult, 100m)).Result);
    }

    [Fact]
    public void MissingEntitiesReturnNotFound()
    {
        var context = DataSeeder.Seed();

        var exhibitions = new ExhibitionsController(context);
        var analytics = new AnalyticsController(
            new MuseumAnalyticsService(context));

        Assert.IsType<NotFoundResult>(
            exhibitions.GetById(999).Result);

        Assert.IsType<NotFoundResult>(
            analytics.GetTourVisitors(999).Result);
    }

    [Fact]
    public void DuplicateRelationReturnsConflict()
    {
        var context = DataSeeder.Seed();
        var controller = new TourExhibitionsController(context);

        Assert.IsType<ConflictObjectResult>(
            controller.Create(
                new TourExhibitionRequest(1, 1)).Result);
    }

    [Fact]
    public void InvalidAnalyticsPeriodReturnsBadRequest()
    {
        var controller = new AnalyticsController(
            new MuseumAnalyticsService(DataSeeder.Seed()));

        Assert.IsType<BadRequestObjectResult>(
            controller.GetAttendanceByTheme(
                new DateOnly(2026, 1, 12),
                new DateOnly(2026, 1, 10)).Result);
    }
}
