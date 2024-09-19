namespace CityPharmacyChain.Domain;

public class Pharmacy(int number, string name, long phoneNumber, string address, string directorFullName)
{
    public int Number { get; set; } = number;
    public string Name { get; set; } = name;
    public long PhoneNumber { get; set; } = phoneNumber;
    public string Address { get; set; } = address;
    public string DirectorFullName { get; set; } = directorFullName;
    public List<Product> Products { get; set; } = [];
}
