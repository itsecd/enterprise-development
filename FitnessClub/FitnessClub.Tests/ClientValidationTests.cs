using System.ComponentModel.DataAnnotations;
using FitnessClub.Contracts.Dtos;

namespace FitnessClub.Tests;

/// <summary>Проверки входных данных, отмеченных в ревью второй лабораторной.</summary>
public class ClientValidationTests
{
    [Theory]
    [InlineData("0001-01-01", "2026-12-01")]
    [InlineData("2026-11-01", "0001-01-01")]
    [InlineData("2026-12-01", "2026-11-01")]
    public void InvalidSubscriptionDatesAreRejected(string start, string end)
    {
        var dto = CreateClient();
        dto.SubscriptionStartDate = DateTime.Parse(start, System.Globalization.CultureInfo.InvariantCulture);
        dto.SubscriptionEndDate = DateTime.Parse(end, System.Globalization.CultureInfo.InvariantCulture);
        Assert.False(Validator.TryValidateObject(dto, new ValidationContext(dto), [], true));
    }

    [Fact]
    public void FutureBirthDateIsRejected()
    {
        var dto = CreateClient();
        dto.BirthDate = DateTime.Today.AddDays(1);
        Assert.False(Validator.TryValidateObject(dto, new ValidationContext(dto), [], true));
    }

    [Theory]
    [InlineData(null)]
    [InlineData((GenderDto)42)]
    public void InvalidGenderIsRejected(GenderDto? gender)
    {
        var dto = CreateClient();
        dto.Gender = gender;
        Assert.False(Validator.TryValidateObject(dto, new ValidationContext(dto), [], true));
    }

    [Fact]
    public void ValidClientIsAccepted()
    {
        var dto = CreateClient();
        Assert.True(Validator.TryValidateObject(dto, new ValidationContext(dto), [], true));
    }

    private static CreateClientDto CreateClient() => new()
    {
        PassportNumber = "1234 567890",
        FullName = "Иван Петров",
        Phone = "+79991234567",
        Gender = GenderDto.Male,
        BirthDate = new DateTime(1990, 1, 1),
        SubscriptionStartDate = new DateTime(2026, 11, 1),
        SubscriptionEndDate = new DateTime(2026, 12, 1)
    };
}
