namespace VetClinic.Domain.Entities;
/// <summary>
/// Ветеринар
/// </summary>
public class Vet
{
    /// <summary>
    /// Идентификатор врача
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Имя врача
    /// </summary>
    public required string FullName { get; set; }
    /// <summary>
    /// Номер паспорта 
    /// </summary>
    public required string PassportNumber { get; set; }
    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly BirthDate { get; set; }
    /// <summary>
    /// Стаж работы
    /// </summary>
    public required int ExpirienceYears { get; set; }
    /// <summary>
    /// Идентификатор специализации врача
    /// </summary>
    public int SpecializationId { get; set; }
    /// <summary>
    /// Ссылка на специализацию врача
    /// </summary>
    public required Specialization Specialization { get; set; }
}