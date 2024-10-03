namespace AdmissionCommittee.Domain.Models;

/// <summary>
/// Represents a speciality offered by a university.
/// </summary>
public class Speciality
{
    /// <summary>
    /// Unique identifier of the speciality.
    /// </summary>
    /// <example>1</example>
    public required int Id { get; set; }

    /// <summary>
    /// Speciality code used to identify the speciality.
    /// </summary>
    /// <example>100503D</example>
    public required string Number { get; set; }

    /// <summary>
    /// Name of the speciality.
    /// </summary>
    /// <example>ИБАС</example>
    public required string Name { get; set; }

    /// <summary>
    /// The faculty or department that offers this speciality.
    /// </summary>
    /// <example>ИИК</example>
    public required string Facility { get; set; }
}
