using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CarOnRent
{
    public required Vehicle Vehicle { get; set; }
    public required Client Client { get; set; }
    public required RentalPoint RentalPointGetFrom { get; set; }
    public DateTime RentalTime { get; set; }
    public DateTime? ReturnTime { get; set; }
    public TimeSpan RentalDuration { get; set; }
    public RentalPoint? RentalPointReturnTo { get; set; }
}
