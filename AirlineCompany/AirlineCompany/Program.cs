using AirlineCompany.Domain;
using System.Data;
using System.Globalization;

//var airReader = File.ReadAllLines("airflyights.csv");
//var airFlights = new List<AirFlight>();
//ushort flyId = 0;

//foreach(var airLine in airReader)
//{
//    if (!airLine.Contains('"')) continue;

//    var tokens = airLine.Split(';');

//    for (var i = 0; i < tokens.Length; i++)
//    {
//        tokens[i] = tokens[i].Trim('"');
//    }

//    var flight = new AirFlight
//    {
//        Idflight = flyId++,
//        CodeNumber = tokens[0],
//        DeparturePoint = tokens[1],
//        ArrivalPoint = tokens[2],
//        Departure = DateTime.ParseExact(tokens[3], "yyyy-MM-dd HH:m:s", CultureInfo.InvariantCulture),
//        Arrive = DateTime.Parse(tokens[4]),
//        FlyingTime = TimeOnly.Parse(tokens[5]),
//        PlaneType = tokens[6]
//    };
    
//    airFlights.Add(flight);
//}

//foreach (var airFlight in airFlights)
//{
//    Console.WriteLine(airFlight.Idflight);
//    Console.WriteLine(airFlight.CodeNumber);
//    Console.WriteLine(airFlight.DeparturePoint);
//    Console.WriteLine(airFlight.Departure);
//    Console.WriteLine(airFlight.ArrivalPoint);
//    Console.WriteLine(airFlight.Arrive);
//    Console.WriteLine(airFlight.FlyingTime);
//    Console.WriteLine(airFlight.PlaneType);
//    Console.WriteLine("--------------------------");
//}

//var passengerReader = File.ReadAllLines("passengers.csv");
//var passengers = new List<Passeneger>();
//ushort passengerId = 0;

//foreach (var passengerline in passengerReader)
//{
//    if (!passengerline.Contains('"')) continue;

//    var tokens = passengerline.Split(';');

//    for (var i = 0; i < tokens.Length; i++)
//    {
//        tokens[i] = tokens[i].Trim('"');
//    }

//    var passenger = new Passeneger
//    {
//        IdPassenger = passengerId++,
//        FIO = tokens[0],
//        Passport = tokens[1],
//        registration = bool.Parse(tokens[2]),
//        ticketNumber = tokens[3],
//        seatNumber = tokens[4],
//        baggageWeight = float.Parse(tokens[5], CultureInfo.InvariantCulture.NumberFormat),
//        codeFlight = tokens[6]
//    };

//    passengers.Add(passenger);
//}

//foreach (var passenger in passengers)
//{
//    Console.WriteLine(passenger.IdPassenger);
//    Console.WriteLine(passenger.FIO);
//    Console.WriteLine(passenger.Passport);
//    Console.WriteLine(passenger.registration);
//    Console.WriteLine(passenger.ticketNumber);
//    Console.WriteLine(passenger.seatNumber);
//    Console.WriteLine(passenger.baggageWeight);
//    Console.WriteLine(passenger.codeFlight);
//    Console.WriteLine("--------------------------");
//}


var planeReader = File.ReadAllLines("planes.csv");
var planes = new List<Plane>();
ushort planeId = 0;

foreach (var planeline in planeReader)
{
    if (!planeline.Contains('"')) continue;

    var tokens = planeline.Split(';');

    for (var i = 0; i < tokens.Length; i++)
    {
        tokens[i] = tokens[i].Trim('"');
    }

    var plane = new Plane
    {
        IdPlane = planeId++,
        Model = tokens[0],
        LoadCapacity = float.Parse(tokens[1], CultureInfo.InvariantCulture.NumberFormat),
        Efficiency = float.Parse(tokens[2], CultureInfo.InvariantCulture.NumberFormat),
        PassengerMax = ushort.Parse(tokens[3], CultureInfo.InvariantCulture.NumberFormat),
    };

    planes.Add(plane);
}

foreach (var plane in planes)
{
    Console.WriteLine(plane.IdPlane);
    Console.WriteLine(plane.Model);
    Console.WriteLine(plane.LoadCapacity);
    Console.WriteLine(plane.Efficiency);
    Console.WriteLine(plane.PassengerMax);
    Console.WriteLine("--------------------------");
}
