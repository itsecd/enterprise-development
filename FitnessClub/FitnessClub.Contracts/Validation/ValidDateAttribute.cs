using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Validation;

/// <summary>Проверяет наличие даты и допустимость будущей даты.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ValidDateAttribute : ValidationAttribute
{
    public bool AllowFuture { get; set; } = true;

    public override bool IsValid(object? value) => value switch
    {
        DateTime date => date != default && (AllowFuture || date.Date <= DateTime.UtcNow.Date),
        DateTimeOffset date => date != default && (AllowFuture || date.UtcDateTime.Date <= DateTime.UtcNow.Date),
        _ => false
    };

    public override string FormatErrorMessage(string name) =>
        AllowFuture ? $"Укажите дату для поля {name}." : $"Поле {name} должно содержать дату не позднее сегодняшней.";
}
