using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineCompany.Domain;
internal class Passenegr
{
    public string FIO;
    private string _passport;
    private bool _registration = false;
    private string _flyingNumber = null!; // ~ TicketNumber
    private string _seatNumber = null!;
    private int _baggageWeight = 0;

    public Passenegr(string fio, string passport)
    {
        FIO = fio;
        _passport = passport;
    }

    public string Passport => _passport;

    public bool Registration
    {
        get { return _registration; }
        set { _registration = value; }
    }

    public string FlyingNumber
    {
        get { return _flyingNumber; }
        set { _flyingNumber = value; }
    }

    public string SeatNumber
    {
        get { return _seatNumber; }
        set { _seatNumber = value; }
    }

    public int BaggageWeight
    {
        get { return _baggageWeight; }
        set { _baggageWeight = value; }
    }
}

