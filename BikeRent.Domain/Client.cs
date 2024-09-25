namespace BikeRent.Domain;
/// <summary>
/// Class that represents service's client
/// </summary>
public class Client
{
    /// <summary>
    /// Client's id
    /// </summary>
    public required  int Id { get; set; }
    /// <summary>
    /// Client's name
    /// </summary>
    public required  string FirstName { get; set; }
    /// <summary>
    /// Client's second name
    /// </summary>
    public required string SecondName { get; set; }
    /// <summary>
    /// Client's Patronymic
    /// </summary>
    public required string Patronymic { get; set; }
    /// <summary>
    /// Client's birth date
    /// </summary>
    public required DateTime BirthDate { get; set; }
    /// <summary>
    /// Client's phone number
    /// </summary>
    public required string PhoneNumber { get; set; }
}