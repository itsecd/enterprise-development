public class Vehicle
{
    public  string CarNumber { get; set; }
    public string Model { get; set; }
    public string Color { get; set; }
    public Vehicle(string carNumber, string model, string color)
    {
        CarNumber = carNumber;
        Model = model;
        Color = color;
    }

}

