namespace AdmissionCommittee.Domain.Models;

/// <summary>
/// Represents an application submitted by an applicant for a specific speciality.
/// </summary>
public class Application
{
    /// <summary>
    /// Unique identifier of the application.
    /// </summary>
    /// <example>1</example>
    public required int Id { get; set; }

    /// <summary>
    /// Identifier of the speciality that the applicant is applying for.
    /// </summary>
    /// <example>1</example>
    public required int SpecialityId { get; set; }

    /// <summary>
    /// Identifier of the applicant who submitted the application.
    /// </summary>
    /// <example>1</example>
    public required int ApplicantId { get; set; }

    /// <summary>
    /// Priority of the application. Lower values indicate higher priority.
    /// </summary>
    /// <example>1</example>
    public required int Priority { get; set; }
}
