using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineCompany.Domain;
public class Plane
{
    private string _model;
    private int _loadCapacity;
    private int _efficincy;
    private int _passengerMax;

    public Plane(string model, int loadCapacity, int efficinty, int passengerMax) 
    {
        _model = model;
        _loadCapacity = loadCapacity;
        _efficincy = efficinty;
        _passengerMax = passengerMax;
    }

    public string Model => _model;
    public int LoadCapacity => _loadCapacity;
    public int Efficincy => _efficincy;
    public int PassengerMax => _passengerMax;
}

