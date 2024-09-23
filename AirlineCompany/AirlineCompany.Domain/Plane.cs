using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineCompany.Domain;
public class Plane
{
    /// <summary>
    /// Класс содержит самолета
    /// </summary>
    public required ushort IdPlane { get; set; }
    public required string Model { set; get; }
    public required float LoadCapacity { set; get; }
    public required float Efficiency { set; get; }
    public required ushort PassengerMax { set; get; }
}

