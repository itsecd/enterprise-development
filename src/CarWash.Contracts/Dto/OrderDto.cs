using System.ComponentModel.DataAnnotations;

namespace CarWash.Contracts.Dto;

public class OrderDto
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string ClientFullName { get; set; } = string.Empty;
    public int CarId { get; set; }
    public string CarLicensePlate { get; set; } = string.Empty;
    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int BoxNumber { get; set; }
}

public class OrderEditDto
{
    [Required(ErrorMessage = "ID клиента обязателен")]
    public int ClientId { get; set; }

    [Required(ErrorMessage = "ID автомобиля обязателен")]
    public int CarId { get; set; }

    [Required(ErrorMessage = "ID услуги обязателен")]
    public int ServiceId { get; set; }

    [Required(ErrorMessage = "Время начала обязательно")]
    public DateTime StartTime { get; set; }

    [Range(1, 100, ErrorMessage = "Номер бокса должен быть от 1 до 100")]
    public int BoxNumber { get; set; }
}