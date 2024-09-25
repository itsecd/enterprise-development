using System;

namespace RecruitmentAgency.Domain;
///<summary>
///Работодатель
///</summary>
public class Employer
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
    /// ФИО контактного лица
    /// </summary>
    public required string ContactPersonName { get; set; }
    /// <summary>
    /// Телефон работодателя
    /// </summary>
    public required string CompanyNumber { get; set; }
}
