using AutoService.Domain.Entities;
using AutoService.Domain.Enums;
using Bogus;

namespace AutoService.Domain.Data;

public static class DataSeeder
{
    public static AutoServiceContext Seed()
    {
        var context = new AutoServiceContext();

        CreateClients(context);

        CreateCars(context);

        CreateMechanics(context);

        CreateWorkTypes(context);

        CreateOrders(context);

        return context;
    }


    private static void CreateClients(AutoServiceContext context)
    {
        var clientFaker = new Faker<Client>()
            .RuleFor(
                x => x.Id,
                f => f.IndexFaker + 1)
            .RuleFor(
                x => x.FullName,
                f => f.Person.FullName)
            .RuleFor(
                x => x.Phone,
                f => f.Phone.PhoneNumber());

        context.Clients.AddRange(
            clientFaker.Generate(10));
    }


    private static void CreateMechanics(AutoServiceContext context)
    {
        var mechanicFaker = new Faker<Mechanic>()
            .RuleFor(
                x => x.Id,
                f => f.IndexFaker + 1)
            .RuleFor(
                x => x.PassportNumber,
                f => f.Random.Replace("##########"))
            .RuleFor(
                x => x.FullName,
                f => f.Person.FullName)
            .RuleFor(
                x => x.Experience,
                f => f.Random.Int(1, 30))
            .RuleFor(
                x => x.Specialization,
                f => f.PickRandom<MechanicSpecialization>());

        context.Mechanics.AddRange(
            mechanicFaker.Generate(10));
    }


    private static void CreateWorkTypes(AutoServiceContext context)
    {
        var workFaker = new Faker<WorkType>()
            .RuleFor(
                x => x.Id,
                f => f.IndexFaker + 1)
            .RuleFor(
                x => x.Name,
                f => f.Commerce.ProductName())
            .RuleFor(
                x => x.Category,
                f => f.PickRandom<WorkCategory>())
            .RuleFor(
                x => x.Cost,
                f => f.Random.Decimal(500, 50000))
            .RuleFor(
                x => x.Duration,
                f => TimeSpan.FromHours(
                    f.Random.Int(1, 8)))
            .RuleFor(
                x => x.Description,
                f => f.Lorem.Sentence());

        context.WorkTypes.AddRange(
            workFaker.Generate(10));
    }


    private static void CreateCars(AutoServiceContext context)
    {
        var carFaker = new Faker<Car>()
            .RuleFor(
                x => x.Id,
                f => f.IndexFaker + 1)
            .RuleFor(
                x => x.LicensePlate,
                f => f.Vehicle.Vin())
            .RuleFor(
                x => x.Brand,
                f => f.Vehicle.Manufacturer())
            .RuleFor(
                x => x.Model,
                f => f.Vehicle.Model())
            .RuleFor(
                x => x.Year,
                f => f.Random.Int(2000, 2026))
            .RuleFor(
                x => x.ClientId,
                f => f.PickRandom(context.Clients).Id)
            .RuleFor(
                x => x.Client,
                (f, x) =>
                    context.Clients
                        .First(c => c.Id == x.ClientId));

        context.Cars.AddRange(
            carFaker.Generate(10));
    }


    private static void CreateOrders(AutoServiceContext context)
    {
    }
}