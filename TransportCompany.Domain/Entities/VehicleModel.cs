using TransportCompany.Domain.Enums;

namespace TransportCompany.Domain.Entities;

public class VehicleModel
{ 
    public int Id { get; set; }
    public BodyType BodyType { get; set; }
    public double BodyVolume { get; set; } // м^3
}

/*
    Шорткат:
    public string Name { get; set; } эквивалентен:

    private string name;
    public string Name {
        get {
            return this.name;
        }
        set {
            this.name = value;
        }
    }
    
*/