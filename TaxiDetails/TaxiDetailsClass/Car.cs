namespace TaxiDetailsClass.Domain;
/// <summary>
/// class about auto
/// </summary>
public class Car(int id, string plate, string model, string color, Driver assignedDriver)
{
    /// <value>Identivicator</value>
    public int Id { get; set; } = id;
    public string Plate { get; set; } = plate;
    /// <value>Model</value>
    public string Model { get; set; } = model;
    /// <value>color</value>
    public string Color { get; set; } = color;
    /// <value>relationship with driver</value>
    public Driver AssignedDriver { get; set; } = assignedDriver;
}