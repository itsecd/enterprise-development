namespace AdmissionCommittee.Domain.Models;
/// <summary>
/// An applicant entering the university
/// </summary>
public class Applicant
{
    /// <summary>
    /// Unique Id
    /// </summary>
    /// <example>1</example>
    public required int Id { get; set; }
    /// <summary>
    /// Applicant's full name
    /// </summary>
    /// <example>Ivanov Ivan</example>
    public required string FullName { get; set; }
    /// <summary>
    /// Birthday date of an applicant
    /// </summary>
    /// <example>1990-01-01</example>
    public required DateTime BirthdayDate { get; set; }
    /// <summary>
    /// Applicant's home country
    /// </summary>
    /// <example>Russia</example>
    public required string Country { get; set; }
    /// <summary>
    /// Applicant's home city
    /// </summary>
    /// <example>Moscow</example>
    public required string City { get; set; }
}
