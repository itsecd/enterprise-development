using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineCompany.Domain;
public class Passeneger
{
    /// <summary>
    /// Класс описывает информацию о пассажире
    /// </summary>
    public required uint IdPassenger { get; set; }
    public required string FIO { get; set; }
    public required string Passport { get; set; }

    public required bool registration = false;
    public required string ticketNumber = null!; // ~ TicketNumber
    public required string seatNumber = null!;
    public required float baggageWeight = 0;
    public required string codeFlight = ""; // поле для связи пассажира и рейса

    public bool Registration
    {
        get { return registration; }
        set { registration = value; }
    }

    public string TicketNumber
    {
        get { return ticketNumber; }
        set { ticketNumber = value; }
    }
    public string SeatNumber
    {
        get { return seatNumber; }
        set { seatNumber = value; }
    }

    public float BaggageWeight
    {
        get { return baggageWeight; }
        set { baggageWeight = value; }
    }

    public string сodeFlight
    {
        get { return codeFlight; }
        set { codeFlight = value; }
    }
}

