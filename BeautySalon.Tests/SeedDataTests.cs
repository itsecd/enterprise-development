using BeautySalon.Infrastructure.InMemory;

namespace BeautySalon.Tests;

/// <summary>
/// Проверки начальных данных салона
/// </summary>
public class SeedDataTests
{
    /// <summary>
    /// Каждая коллекция содержит минимум десять объектов с уникальными идентификаторами
    /// </summary>
    /// <param name="collection">Название коллекции</param>
    [Theory]
    [InlineData("customers")]
    [InlineData("specialists")]
    [InlineData("services")]
    [InlineData("appointments")]
    public void CollectionsHaveEnoughObjectsWithUniqueIds(string collection)
    {
        var ids = collection switch
        {
            "customers" => BeautySalonData.Customers.Select(item => item.Id).ToList(),
            "specialists" => BeautySalonData.Specialists.Select(item => item.Id).ToList(),
            "services" => BeautySalonData.Services.Select(item => item.Id).ToList(),
            "appointments" => BeautySalonData.Appointments.Select(item => item.Id).ToList(),
            _ => throw new ArgumentException("Неизвестная коллекция")
        };

        Assert.True(ids.Count >= 10);
        Assert.All(ids, id => Assert.True(id > 0));
        Assert.Equal(ids.Count, ids.Distinct().Count());
    }

    /// <summary>
    /// Записи связаны с объектами из начальных справочников
    /// </summary>
    [Fact]
    public void AppointmentsReferenceExistingEntities()
    {
        Assert.All(BeautySalonData.Appointments, appointment =>
        {
            Assert.Same(
                BeautySalonData.Customers.Single(
                    customer => customer.Id == appointment.Customer.Id),
                appointment.Customer);

            Assert.Same(
                BeautySalonData.Specialists.Single(
                    specialist => specialist.Id == appointment.Specialist.Id),
                appointment.Specialist);

            Assert.Same(
                BeautySalonData.Services.Single(
                    service => service.Id == appointment.Service.Id),
                appointment.Service);
        });
    }

    /// <summary>
    /// Начальные записи не пересекаются у специалиста или посетителя
    /// </summary>
    /// <param name="checkSpecialist">Проверять расписание специалиста</param>
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void InitialAppointmentsDoNotOverlap(bool checkSpecialist)
    {
        var groups = BeautySalonData.Appointments.GroupBy(appointment =>
            checkSpecialist ? appointment.Specialist.Id : appointment.Customer.Id);

        foreach (var group in groups)
        {
            var schedule = group
                .OrderBy(appointment => appointment.AppointmentDateTime)
                .ToList();

            for (var index = 1; index < schedule.Count; index++)
            {
                var previous = schedule[index - 1];
                var current = schedule[index];
                var previousEnd = previous.AppointmentDateTime.AddMinutes(
                    previous.Service.DurationMinutes);

                Assert.True(
                    previousEnd <= current.AppointmentDateTime,
                    $"Пересекаются записи {previous.Id} и {current.Id}");
            }
        }
    }

    /// <summary>
    /// Услуги имеют допустимую стоимость и положительную длительность
    /// </summary>
    [Fact]
    public void ServicesHaveValidPriceAndDuration()
    {
        Assert.All(BeautySalonData.Services, service =>
        {
            Assert.False(string.IsNullOrWhiteSpace(service.Name));
            Assert.False(string.IsNullOrWhiteSpace(service.Category));
            Assert.True(service.Price >= 0);
            Assert.True(service.DurationMinutes > 0);
        });
    }
}