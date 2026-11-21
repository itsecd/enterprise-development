namespace DryCleaner.Domain.Entities;

/// <summary>
/// Клиент химчистки
/// </summary>
public class Client
{
    /// <summary>
    /// Идинтификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Номер клиента
    /// </summary>
    /// 
    public string? Phone { get; set; }
}