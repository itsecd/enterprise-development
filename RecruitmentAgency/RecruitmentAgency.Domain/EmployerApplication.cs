namespace RecruitmentAgency;
/// <summary>
/// Заявка работадателя
/// </summary>
internal class EmployerApplication
{
    /// <summary>
    /// Идентификатор заявки работадателя
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Дата подачи
    /// </summary>
    public required DateTime SubmissionDate { get; set; }
    /// <summary>
    /// Работадатель
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
