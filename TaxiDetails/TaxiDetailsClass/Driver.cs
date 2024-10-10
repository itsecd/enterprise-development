namespace TaxiDetailsClass.Domain;
/// <summary>
/// class Driver about info of drivers
/// </summary>
public class Driver(int id, string name, string surname, string patronymic, string passport, string address, string phone)
{
    /// <value>Identivicator</value>
    public int Id { get; set; } = id;
    /// <value>Name of driver</value>
    public string Name { get; set; } = name;
    /// <value>Surname of driver</value>
    public string Surname { get; set; } = surname;
    /// <value>Patronymic of driver</value>
    public string Patronymic { get; set; } = patronymic;
    /// <value>Passport of driver</value>
    public string Passport { get; set; } = passport;
    /// <value>Address of driver</value>
    public string Address { get; set; } = address;
    /// <value>Phone of driver</value>
    public string Phone { get; set; } = phone;
}