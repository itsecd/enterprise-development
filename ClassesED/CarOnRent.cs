using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CarOnRent
{
    public Vehicle Vehicle { get; set; }
    public Client Client { get; set; }
    public RentalPoint RentalPointGetFrom { get; set; }
    public DateTime RentalTime { get; set; }
    public DateTime? ReturnTime { get; set; }
    public TimeSpan RentalDuration { get; set; }
    public RentalPoint? RentalPointReturnTo { get; set; }
    public CarOnRent(Vehicle vehicle, Client client, RentalPoint rentalPointGetFrom, 
        DateTime rentalTimeGet, TimeSpan rentalDuration, DateTime? rentalTimeReturn = null, RentalPoint? rentalPointReturnTo = null)
    {
        Vehicle = vehicle;
        Client = client;
        RentalPointGetFrom = rentalPointGetFrom;
        RentalTime = rentalTimeGet;
        RentalDuration = rentalDuration;
        RentalPointReturnTo = rentalPointReturnTo;
        ReturnTime = rentalTimeReturn; // Изначально время возврата не установлено
    }
}
