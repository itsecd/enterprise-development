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
    /// Applicant's name
    /// </summary>
    /// <example>Андрей</example>
    public required string Name { get; set; }
    /// <summary>
    /// Birthday date of an applicant
    /// </summary>
    /// <example>1990-01-01</example>
    public required DateTime BirthdayDate { get; set; }
    /// <summary>
    /// Applicant's home country
    /// </summary>
    /// <example>Россия</example>
    public required string Country { get; set; }
    /// <summary>
    /// Applicant's home city
    /// </summary>
    /// <example>Москва</example>
    public required string City { get; set; }
}
