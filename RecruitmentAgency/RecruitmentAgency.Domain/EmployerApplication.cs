namespace RecruitmentAgency.Domain;
/// <summary>
/// Заявка работодателя
/// </summary>
public class EmployerApplication
{
    /// <summary>
    /// Идентификатор заявки работодателя
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Дата подачи
    /// </summary>
    public required DateTime SubmissionDate { get; set; }
    /// <summary>
    /// Работодатель
    /// </summary>
    public required Employer Employer { get; set; }
    /// <summary>
    /// Должность
    /// </summary>
    public required Position Position { get; set; }
    /// <summary>
    /// Требование 
    /// </summary>
    public required string Requirements { get; set; }
    /// <summary>
    /// Предлагаемый уровень зарплаты
    /// </summary>
    public required int OfferedSalary { get; set; }
}
