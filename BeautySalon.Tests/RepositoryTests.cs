using BeautySalon.Infrastructure.InMemory;
using BeautySalon.Infrastructure.InMemory.Repositories;

namespace BeautySalon.Tests;

/// <summary>
/// Проверки хранения и связей между сущностями
/// </summary>
public class RepositoryTests
{
    /// <summary>
    /// Изменение полученного объекта не меняет сохранённые данные
    /// </summary>
    [Fact]
    public void ReadingCustomerReturnsIndependentCopy()
    {
        var repository = new CustomerRepository(new SalonStorage());
        var customer = repository.GetById(1)!;
        var originalName = customer.LastName;

        customer.LastName = "Другая фамилия";

        Assert.Equal(originalName, repository.GetById(1)!.LastName);
    }

    /// <summary>
    /// Обновлённые сведения посетителя видны в его записях
    /// </summary>
    [Fact]
    public void UpdatingCustomerUpdatesAppointmentDetails()
    {
        var storage = new SalonStorage();
        var customers = new CustomerRepository(storage);
        var appointments = new AppointmentRepository(storage);
        var customer = customers.GetById(1)!;
        customer.LastName = "Новая фамилия";

        Assert.True(customers.Update(customer));

        var appointment = appointments.GetAll().First(item => item.Customer.Id == 1);
        Assert.Equal("Новая фамилия", appointment.Customer.LastName);
    }

    /// <summary>
    /// Разные хранилища не разделяют изменяемые данные
    /// </summary>
    [Fact]
    public void StorageInstancesAreIndependent()
    {
        var first = new CustomerRepository(new SalonStorage());
        var second = new CustomerRepository(new SalonStorage());
        var originalName = second.GetById(1)!.LastName;
        var customer = first.GetById(1)!;
        customer.LastName = "Изменённая фамилия";

        first.Update(customer);

        Assert.Equal(originalName, second.GetById(1)!.LastName);
    }

    /// <summary>
    /// Идентификатор удалённого объекта не выдаётся повторно
    /// </summary>
    [Fact]
    public void DeletedIdentifierIsNotReused()
    {
        var repository = new CustomerRepository(new SalonStorage());
        var template = repository.GetById(1)!;

        var first = repository.Add(template);
        Assert.True(repository.Delete(first.Id));

        var second = repository.Add(template);

        Assert.True(second.Id > first.Id);
    }

    /// <summary>
    /// Нельзя удалить посетителя с существующими записями
    /// </summary>
    [Fact]
    public void CustomerWithAppointmentsCannotBeDeleted()
    {
        var repository = new CustomerRepository(new SalonStorage());

        Assert.Throws<InvalidOperationException>(() => repository.Delete(1));
        Assert.NotNull(repository.GetById(1));
    }
}