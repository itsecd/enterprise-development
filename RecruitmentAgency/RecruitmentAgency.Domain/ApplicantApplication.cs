namespace RecruitmentAgency.Domain;
/// <summary>
/// Заявка соискателя
/// </summary>
public class ApplicantApplication
{
    /// <summary>
    /// Идентификатор заявки соискателя
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Дата подачи
    /// </summary>
    public required DateTime SubmissionDate { get; set; }
    /// <summary>
    /// Соискатель 
    /// </summary>
    public required Applicant Applicant { get; set; }
    /// <summary>
    /// Должность 
    /// </summary>
    public required Position Position { get; set; }

}
