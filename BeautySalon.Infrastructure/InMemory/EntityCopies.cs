using BeautySalon.Domain.Entities;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Копирование сущностей без передачи изменяемого состояния хранилища
/// </summary>
internal static class EntityCopies
{
    internal static Customer Copy(Customer source)
    {
        return new Customer
        {
            Id = source.Id,
            LastName = source.LastName,
            FirstName = source.FirstName,
            Patronymic = source.Patronymic,
            Gender = source.Gender,
            DateOfBirth = source.DateOfBirth,
            PhoneNumber = source.PhoneNumber,
        };
    }

    internal static BeautySpecialist Copy(BeautySpecialist source)
    {
        return new BeautySpecialist
        {
            Id = source.Id,
            LastName = source.LastName,
            FirstName = source.FirstName,
            Patronymic = source.Patronymic,
            Gender = source.Gender,
            DateOfBirth = source.DateOfBirth,
            PhoneNumber = source.PhoneNumber,
            PassportNumber = source.PassportNumber,
            Specialization = source.Specialization,
            WorkExperience = source.WorkExperience,
        };
    }

    internal static Service Copy(Service source)
    {
        return new Service
        {
            Id = source.Id,
            Name = source.Name,
            Category = source.Category,
            Price = source.Price,
            DurationMinutes = source.DurationMinutes,
        };
    }

    internal static Appointment Copy(Appointment source)
    {
        return new Appointment
        {
            Id = source.Id,
            Customer = Copy(source.Customer),
            Specialist = Copy(source.Specialist),
            Service = Copy(source.Service),
            AppointmentDateTime = source.AppointmentDateTime,
            IsRegularCustomer = source.IsRegularCustomer,
        };
    }
}