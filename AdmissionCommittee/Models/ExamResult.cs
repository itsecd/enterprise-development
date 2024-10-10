namespace AdmissionCommittee.Domain.Models;

/// <summary>
/// Result of an exam
/// </summary>
public class ExamResult
{
    /// <summary>
    /// Unique Id
    /// </summary>
    /// <example>1</example>
    public required int Id { get; set; }
    /// <summary>
    /// Id of an Applicant
    /// </summary>
    /// <example>1</example>
    public required int ApplicantId { get; set; }
    /// <summary>
    /// Name of an exam
    /// </summary>
    /// <example>Math</example>
    public required string ExamName { get; set; }
    /// <summary>
    /// Applicant's result by an exam
    /// </summary>
    /// <example>87</example>
    public required int Result { get; set; }
}

