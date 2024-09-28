using AirlineCompany.Domain;
using System.Data;
using System.Globalization;

var airReader = File.ReadAllLines("airflyights.csv");
var airFlights = new List<AirFlight>();
ushort flyId = 0;

foreach (var airLine in airReader)
{
    if (!airLine.Contains('"')) continue;

    var tokens = airLine.Split(';');

    for (var i = 0; i < tokens.Length; i++)
    {
        tokens[i] = tokens[i].Trim('"');
    }

    var flight = new AirFlight
    {
        Idflight = flyId++,
        CodeNumber = tokens[0],
        DeparturePoint = tokens[1],
        ArrivalPoint = tokens[2],
        Departure = DateTime.ParseExact(tokens[3], "yyyy-MM-dd HH:m:s", CultureInfo.InvariantCulture),
        Arrive = DateTime.Parse(tokens[4]),
        FlyingTime = TimeOnly.Parse(tokens[5]),
        PlaneType = tokens[6]
    };

    airFlights.Add(flight);
}

/*foreach (var airFlight in airFlights)
{
    Console.WriteLine(airFlight.Idflight);
    Console.WriteLine(airFlight.CodeNumber);
    Console.WriteLine(airFlight.DeparturePoint);
    Console.WriteLine(airFlight.Departure);
    Console.WriteLine(airFlight.ArrivalPoint);
    Console.WriteLine(airFlight.Arrive);
    Console.WriteLine(airFlight.FlyingTime);
    Console.WriteLine(airFlight.PlaneType);
    Console.WriteLine("--------------------------");
}*/

var passengerReader = File.ReadAllLines("passengers.csv");
var passengers = new List<Passeneger>();
ushort passengerId = 0;

foreach (var passengerline in passengerReader)
{
    if (!passengerline.Contains('"')) continue;

    var tokens = passengerline.Split(';');

    for (var i = 0; i < tokens.Length; i++)
    {
        tokens[i] = tokens[i].Trim('"');
    }

    var passenger = new Passeneger
    {
        IdPassenger = passengerId++,
        FIO = tokens[0],
        Passport = tokens[1],
        registration = bool.Parse(tokens[2]),
        ticketNumber = tokens[3],
        seatNumber = tokens[4],
        baggageWeight = float.Parse(tokens[5], CultureInfo.InvariantCulture.NumberFormat),
        codeFlight = tokens[6]
    };

    passengers.Add(passenger);
}

/*foreach (var passenger in passengers)
{
    Console.WriteLine(passenger.IdPassenger);
    Console.WriteLine(passenger.FIO);
    Console.WriteLine(passenger.Passport);
    Console.WriteLine(passenger.registration);
    Console.WriteLine(passenger.ticketNumber);
    Console.WriteLine(passenger.seatNumber);
    Console.WriteLine(passenger.baggageWeight);
    Console.WriteLine(passenger.codeFlight);
    Console.WriteLine("--------------------------");
}*/


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

/*foreach (var plane in planes)
{
    Console.WriteLine(plane.IdPlane);
    Console.WriteLine(plane.Model);
    Console.WriteLine(plane.LoadCapacity);
    Console.WriteLine(plane.Efficiency);
    Console.WriteLine(plane.PassengerMax);
    Console.WriteLine("--------------------------");
}*/

// 1) Вывести сведения о всех авиарейсах, вылетевших из указанного пункта отправления
// в указанный пункт прибытия.


var flyightDA =
   from fly in airFlights
   where fly.DeparturePoint == "Tokio" && fly.ArrivalPoint == "Dublin"
   select fly;

//foreach (var fly in flyightDA)
//{
//    Console.WriteLine(fly.Idflight);
//    Console.WriteLine(fly.CodeNumber);
//    Console.WriteLine(fly.DeparturePoint);
//    Console.WriteLine(fly.Departure);
//    Console.WriteLine(fly.ArrivalPoint);
//    Console.WriteLine(fly.Arrive);
//    Console.WriteLine(fly.FlyingTime);
//    Console.WriteLine(fly.PlaneType);
//    Console.WriteLine("1--------------------------1");
//}

// 2) Вывести сведения обо всех пассажирах, летящих данным рейсом,
// вес багажа которых равен нулю, упорядочить по ФИО.

var passWF =
    from passenger in passengers
    orderby passenger.FIO descending
    where passenger.сodeFlight == "5000" && passenger.baggageWeight == 0
    select new
    {
        passenger.IdPassenger,
        passenger.FIO,
        passenger.Passport,
        passenger.codeFlight,
        passenger.baggageWeight,
    };

//foreach (var pass in passWF)
//{
//    Console.WriteLine(pass.IdPassenger);
//    Console.WriteLine(pass.FIO);
//    Console.WriteLine(pass.Passport);
//    Console.WriteLine(pass.codeFlight);
//    Console.WriteLine(pass.baggageWeight);
//    Console.WriteLine("2--------------------------2");
//}

//3) Вывести сводную информацию обо всех полетах самолетов данного типа
//в указанный период времени.

var flyightPT =
    from fly in airFlights
    where fly.PlaneType == "Panda 202208" &&
    fly.Departure >= DateTime.Parse("2024-09-01") &&
    fly.Departure <= DateTime.Parse("2024-11-30")
    select fly;

//foreach (var fly in flyightPT)
//{
//    Console.WriteLine(fly.Idflight);
//    Console.WriteLine(fly.CodeNumber);
//    Console.WriteLine(fly.DeparturePoint);
//    Console.WriteLine(fly.Departure);
//    Console.WriteLine(fly.ArrivalPoint);
//    Console.WriteLine(fly.Arrive);
//    Console.WriteLine(fly.FlyingTime);
//    Console.WriteLine(fly.PlaneType);
//    Console.WriteLine("3--------------------------3");
//}

//4) Вывести топ 5 авиарейсов по количеству перевезённых пассажиров.


var flyightTop = 
    (from fly in airFlights
    let c = passengers.Count(pass => pass.codeFlight == fly.CodeNumber)
    orderby c descending
    select new { fly.Idflight, fly.CodeNumber, c }).ToList();

//for (var i = 0; i < 5; i++)
//{
//    Console.WriteLine(flyightTop[i].Idflight);
//    Console.WriteLine(flyightTop[i].CodeNumber);
//    Console.WriteLine(flyightTop[i].c);
//    Console.WriteLine("4--------------------------4");
//}

//5) Вывести список рейсов с минимальным временем в пути.

var flyightTime =
    from fly in airFlights
    let minTime = airFlights.Min(pass => pass.FlyingTime)
    where fly.FlyingTime == minTime
    select fly;

//foreach (var fly in flyightTime)
//{
//    Console.WriteLine(fly.Idflight);
//    Console.WriteLine(fly.CodeNumber);
//    Console.WriteLine(fly.DeparturePoint);
//    Console.WriteLine(fly.Departure);
//    Console.WriteLine(fly.ArrivalPoint);
//    Console.WriteLine(fly.Arrive);
//    Console.WriteLine(fly.FlyingTime);
//    Console.WriteLine("5--------------------------5");
//}

//6) Вывести информацию о средней и максимальной загрузке авиарейсов
// из заданного пункта отправления.


var flyightWeight =
    from fly in airFlights
    join pass in passengers on fly.CodeNumber equals pass.codeFlight
    where fly.DeparturePoint == "Rome"
    select new
    {
        id = fly.Idflight,
        code = fly.CodeNumber,
        depar = fly.DeparturePoint,
        arrive = fly.ArrivalPoint,
        bag = pass.baggageWeight,
    };

var flyightWMA =
    (from fly in flyightWeight
    let maxW = (from f in flyightWeight
                select f.bag).Max()
    let avgW = (from f in flyightWeight
                select f.bag).Average()
    select new 
    { 
        maxW, avgW
    }).Distinct();


foreach (var fly in flyightWMA)
{
    Console.WriteLine(fly.maxW);
    Console.WriteLine(fly.avgW);

    Console.WriteLine("6ma--------------------------6ma");
}