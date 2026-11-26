using System.ComponentModel.DataAnnotations;
using VetClinic.Contracts.Dto.Owners;
using VetClinic.Contracts.Dto.Vets;

namespace VetClinic.Tests;

/// <summary>
/// Проверки DTO: длинные имена и год рождения не из будущего
/// </summary>
public class DtoValidationTests
{
    /// <summary>
    /// ФИО длиннее 200 символов проходит валидацию
    /// </summary>
    [Fact]
    public void OwnerFullName_AcceptsMoreThan200Characters()
    {
        var owner = new OwnerEditDto
        {
            FullName = new string('А', 250),
            Address = "ул. Полевая, 1",
            Phone = "+79000000000"
        };

        Assert.Empty(Validate(owner));
    }

    /// <summary>
    /// Год рождения раньше 1900 не проходит валидацию
    /// </summary>
    [Theory]
    [InlineData(1899, false)]
    [InlineData(1900, true)]
    [InlineData(2000, true)]
    public void VetBirthYear_Before1900_IsRejected(int birthYear, bool expectedValid)
    {
        var errors = Validate(NewVet(birthYear));

        if (expectedValid)
        {
            Assert.Empty(errors);
        }
        else
        {
            Assert.Contains(nameof(VetEditDto.BirthYear), Assert.Single(errors).MemberNames);
        }
    }

    /// <summary>
    /// Текущий год допустим, следующий за ним уже нет
    /// </summary>
    [Fact]
    public void VetBirthYear_NextYear_IsRejected()
    {
        Assert.Empty(Validate(NewVet(DateTime.Now.Year)));
        Assert.Contains(nameof(VetEditDto.BirthYear), Assert.Single(Validate(NewVet(DateTime.Now.Year + 1))).MemberNames);
    }

    /// <summary>
    /// Врач с указанным годом рождения
    /// </summary>
    /// <param name="birthYear">Год рождения</param>
    private static VetEditDto NewVet(int birthYear) => new()
    {
        PassportNumber = "0000 000001",
        FullName = "Иванов Иван Иванович",
        BirthYear = birthYear,
        WorkExperienceYears = 0
    };

    /// <summary>
    /// Прогнать валидацию DTO
    /// </summary>
    /// <param name="dto">Проверяемый объект</param>
    private static List<ValidationResult> Validate(object dto)
    {
        List<ValidationResult> errors = [];
        Validator.TryValidateObject(dto, new ValidationContext(dto), errors, validateAllProperties: true);
        return errors;
    }
}
