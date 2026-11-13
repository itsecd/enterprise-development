using System.ComponentModel.DataAnnotations;
using CarWash.Shared.Enums;

namespace CarWash.Contracts.Dto;

public class ServiceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public CarCategory Category { get; set; }
    public decimal Price { get; set; }
    public int DurationMinutes { get; set; }
}

public class ServiceEditDto
{
    [Required(ErrorMessage = "Название услуги обязательно")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Категория обязательна")]
    public CarCategory Category { get; set; }

    [Range(0.01, 100000, ErrorMessage = "Цена должна быть больше нуля")]
    public decimal Price { get; set; }

    [Range(1, 1440, ErrorMessage = "Длительность должна быть от 1 до 1440 минут")]
    public int DurationMinutes { get; set; }
}