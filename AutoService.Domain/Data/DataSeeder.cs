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
        var orderFaker = new Faker<RepairOrder>()
            .RuleFor(
                x => x.Id,
                f => f.IndexFaker + 1)
            .RuleFor(
                x => x.ClientId,
                f => f.PickRandom(context.Clients).Id)
            .RuleFor(
                x => x.CarId,
                (f, x) =>
                    context.Cars
                        .First(car => car.ClientId == x.ClientId)
                        .Id)
            .RuleFor(
                x => x.AdmissionDate,
                f => f.Date.Past(1))
            .RuleFor(
                x => x.ReleaseDate,
                f => f.Date.Recent());


        var orders = orderFaker.Generate(20);

        foreach (var order in orders)
        {
            order.Client =
                context.Clients
                    .First(x => x.Id == order.ClientId);

            order.Car =
                context.Cars
                    .First(x => x.Id == order.CarId);

            AddMechanics(context, order);

            AddWorks(context, order);
        }

        context.RepairOrders.AddRange(orders);
    }


    private static void AddMechanics(
    AutoServiceContext context,
    RepairOrder order)
    {
        var mechanics =
            context.Mechanics
                .OrderBy(_ => Guid.NewGuid())
                .Take(2)
                .ToList();


        foreach (var mechanic in mechanics)
        {
            var orderMechanic = new OrderMechanic
            {
                Id = context.RepairOrders.Count + 1,

                RepairOrder = order,

                RepairOrderId = order.Id,

                Mechanic = mechanic,

                MechanicId = mechanic.Id
            };

            order.Mechanics.Add(orderMechanic);

            mechanic.Orders.Add(orderMechanic);
        }
    }


    private static void AddWorks(
    AutoServiceContext context,
    RepairOrder order)
    {
        var works =
            context.WorkTypes
                .OrderBy(_ => Guid.NewGuid())
                .Take(3)
                .ToList();


        foreach (var work in works)
        {
            var orderWork = new OrderWork
            {
                Id = context.RepairOrders.Count + 1,

                RepairOrder = order,

                RepairOrderId = order.Id,

                WorkType = work,

                WorkTypeId = work.Id
            };


            order.Works.Add(orderWork);

            work.Orders.Add(orderWork);
        }
    }
}