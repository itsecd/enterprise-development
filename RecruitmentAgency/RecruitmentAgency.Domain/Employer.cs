namespace RecruitmentAgency.Domain;
///<summary>
///Работадатель
///</summary>
internal class Employer
{
    /// <summary>
    /// Индентификатор работадателя
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Название компании
    /// </summary>
    public required string CompanyName { get; set; }
    /// <summary>
    /// Телефон работадателя
    /// </summary>
    public required string CompanyNumber { get; set; }
}
