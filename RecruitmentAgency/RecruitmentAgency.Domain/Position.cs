namespace RecruitmentAgency.Domain;
/// <summary>
/// Должность
/// </summary>
public class Position
{
    /// <summary>
    /// Идентификатор должности
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Раздел 
    /// </summary>
    public required string Section { get; set; }
    /// <summary>
    /// Должность
    /// </summary>
    public required string PositionName { get; set; }
}
