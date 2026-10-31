using System.ComponentModel.DataAnnotations;

namespace CarWash.Contracts.Dto;

public class ClientDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Phone { get; set; }
}

public class ClientEditDto
{
    [Required(ErrorMessage = "ФИО обязательно")]
    public string FullName { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Некорректный формат телефона")]
    public string? Phone { get; set; }
}