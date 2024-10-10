namespace TaxiDetailsClass.Domain;
/// <summary>
/// class User about info of users
/// </summary>
public class User(int id, string phone, string fullName)
{/// <value>Identivicator</value>
    public int Id { get; set; } = id;
    /// <value>Phone</value>
    public string Phone { get; set; } = phone;
    /// <value>Fullname</value>
    public string FullName { get; set; } = fullName;
}