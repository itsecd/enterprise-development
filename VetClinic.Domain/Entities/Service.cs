namespace VetClinic.Domain.Entities;
/// <summary>
/// Оказываемая животному услуга 
/// </summary>
public class Service
{
    /// <summary>
    /// Идентификатор услуги
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название услуги 
    /// </summary>
    public required string Title { get; set; }
    /// <summary>
    /// Цена услуги 
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public int SpecializationId { get; set; }
    /// <summary>
    /// Ссылка на специализацию услуги
    /// </summary>
    public required Specialization Specialization { get; set; }
}