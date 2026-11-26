namespace VetClinic.Domain.Entities;
/// <summary>
/// Владелец животного
/// </summary>
public class Owner
{
    /// <summary>
    /// Идентификатор владельца животного
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// ФИО владельца
    /// </summary>
    public required string FullName { get; set; }
    /// <summary>
    /// Номер телефона 
    /// </summary>
    public required string Phone { get; set; }
    /// <summary>
    /// Адрес владельца 
    /// </summary>
    public required string Adress { get; set; }
}