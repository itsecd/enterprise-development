using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineCompany.Domain;
internal class Passenegr
{
    /// <summary>
    /// Класс описывает информацию о пассажире
    /// </summary>
    public required uint IdPassenger { get; set; }
    public required string FIO { get; set; }
    public required string Passport { get; set; }

    public required bool registration = false;
    public required string ticketnumber = null!; // ~ TicketNumber
    public required string seatNumber = null!;
    public required float baggageWeight = 0;
    public required ushort codeFlight = 0; // поле для связи пассажира и рейса

    public bool Registration
    {
        get { return registration; }
        set { registration = value; }
    }

    public string TicketNumber
    {
        get { return ticketnumber; }
        set { ticketnumber = value; }
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

    public ushort сodeFlight
    {
        get { return codeFlight; }
        set { codeFlight = value; }
    }
}

