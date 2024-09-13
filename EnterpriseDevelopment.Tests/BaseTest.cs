using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseDevelopment.Tests
{
    public class BaseTest
    {
        public static List<Vehicle> vehicles = new List<Vehicle>
        {
          
          new("Number 1", "Model 1", "Color 1"),
          new("Number 2", "Model 2", "Color 2"),
          new("Number 3", "Model 3", "Color 3"),
          new("Number 4", "Model 4", "Color 4"),
          new("Number 5", "Model 5", "Color 5"),
          new("Number 6", "Model 6", "Color 6"),
          new("Number 7", "Model 7", "Color 7"),

        };

        public static List<RentalPoint> rentalPoints = new List<RentalPoint>
        {
            new RentalPoint { Name = "Name 1", Address = "Address 1" },
            new RentalPoint { Name = "Name 2", Address = "Address 2" },
            new RentalPoint { Name = "Name 3", Address = "Address 3" },
            new RentalPoint { Name = "Name 4", Address = "Address 4" },
            new RentalPoint { Name = "Name 5", Address = "Address 5" },
            new RentalPoint { Name = "Name 6", Address = "Address 6" },
            new RentalPoint { Name = "Name 7", Address = "Address 7" },
            new RentalPoint { Name = "Name 8", Address = "Address 8" },
            new RentalPoint { Name = "Name 9", Address = "Address 9" },
            new RentalPoint { Name = "Name 10", Address = "Address 10" },
            new RentalPoint { Name = "Name 11", Address = "Address 11" },
            new RentalPoint { Name = "Name 12", Address = "Address 12" },

        };

        public static List<Client> clients = new List<Client>
        {
            new Client { Passport = "Passport 1", FullName = "Full Name 1", BirthDate = new DateTime(1991, 1, 1) },
            new Client { Passport = "Passport 2", FullName = "Full Name 2", BirthDate = new DateTime(1992, 2, 2) },
            new Client { Passport = "Passport 3", FullName = "Full Name 3", BirthDate = new DateTime(1993, 3, 3) },
            new Client { Passport = "Passport 4", FullName = "Full Name 4", BirthDate = new DateTime(1994, 4, 1) },
            new Client { Passport = "Passport 5", FullName = "Full Name 5", BirthDate = new DateTime(1995, 5, 5) },
            new Client { Passport = "Passport 6", FullName = "Full Name 6", BirthDate = new DateTime(1996, 6, 6) },
            new Client { Passport = "Passport 7", FullName = "Full Name 7", BirthDate = new DateTime(1997, 7, 7) },
            new Client { Passport = "Passport 8", FullName = "Full Name 8", BirthDate = new DateTime(1998, 8, 8) },
            new Client { Passport = "Passport 9", FullName = "Full Name 9", BirthDate = new DateTime(1999, 9, 9) },
            new Client { Passport = "Passport 10", FullName = "Full Name 10", BirthDate = new DateTime(2000, 10, 10) },
            new Client { Passport = "Passport 11", FullName = "Full Name 11", BirthDate = new DateTime(2001, 11, 11) },
            new Client { Passport = "Passport 12", FullName = "Full Name 12", BirthDate = new DateTime(2002, 12, 12) },
        };

        public static List<CarOnRent> vehiclesOnRent = new List<CarOnRent>
        {
            new CarOnRent
            {
                Vehicle = vehicles[2],
                Client = clients[3],
                RentalPointGetFrom = rentalPoints[0],
                RentalTime = DateTime.Now.AddDays(-5),
                ReturnTime = DateTime.Now.AddDays(-3),
                RentalPointReturnTo = rentalPoints[4],
                RentalDuration = TimeSpan.FromDays(2)
            },
            new CarOnRent
            {
                Vehicle = vehicles[3],
                Client = clients[5],
                RentalPointGetFrom = rentalPoints[2],
                RentalTime = DateTime.Now.AddDays(-3),
                RentalDuration = TimeSpan.FromDays(1),
                RentalPointReturnTo = rentalPoints[3],
                ReturnTime = DateTime.Now.AddDays(-2)
            },
            new CarOnRent
            {
                Vehicle = vehicles[3],
                Client = clients[6],
                RentalPointGetFrom = rentalPoints[2],
                RentalTime = DateTime.Now.AddDays(-3),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[5],
                ReturnTime = DateTime.Now.AddDays(-1)
            },
            new CarOnRent
            {
                Vehicle = vehicles[4],
                Client = clients[5],
                RentalPointGetFrom = rentalPoints[0],
                RentalTime = DateTime.Now.AddDays(-2),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[4],
                ReturnTime = DateTime.Now
            },
            new CarOnRent
            {
                Vehicle = vehicles[4],
                Client = clients[7],
                RentalPointGetFrom = rentalPoints[6],
                RentalTime = DateTime.Now.AddDays(-2),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[7],
                ReturnTime = DateTime.Now
            },
            new CarOnRent
            {
                Vehicle = vehicles[4],
                Client = clients[8],
                RentalPointGetFrom = rentalPoints[7],
                RentalTime = DateTime.Now.AddDays(-2),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[3],
                ReturnTime = DateTime.Now
            },
            new CarOnRent
            {
                Vehicle = vehicles[5],
                Client = clients[2],
                RentalPointGetFrom = rentalPoints[1],
                RentalTime = DateTime.Now.AddDays(-2),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[2],
                ReturnTime = DateTime.Now
            },
            new CarOnRent
            {
                Vehicle = vehicles[5],
                Client = clients[0],
                RentalPointGetFrom = rentalPoints[2],
                RentalTime = DateTime.Now.AddDays(-2),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[2],
                ReturnTime = DateTime.Now
            },
            new CarOnRent
            {
                Vehicle = vehicles[5],
                Client = clients[9],
                RentalPointGetFrom = rentalPoints[2],
                RentalTime = DateTime.Now.AddDays(-2),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[3],
                ReturnTime = DateTime.Now
            },

            new CarOnRent { Vehicle = vehicles[5], Client = clients[10], RentalPointGetFrom = rentalPoints[3], RentalTime = DateTime.Now.AddDays(-2),
                     RentalDuration = TimeSpan.FromDays(2), RentalPointReturnTo = rentalPoints[2], ReturnTime = DateTime.Now },
            
            new CarOnRent
            {
                Vehicle = vehicles[6],
                Client = clients[3],
                RentalPointGetFrom = rentalPoints[1],
                RentalTime = DateTime.Now.AddDays(-2),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[2],
                ReturnTime = DateTime.Now
            },
            
            new CarOnRent
            {
                Vehicle = vehicles[6],
                Client = clients[5],
                RentalPointGetFrom = rentalPoints[2],
                RentalTime = DateTime.Now.AddDays(-2),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[3],
                ReturnTime = DateTime.Now
            },
            
            new CarOnRent
            {
                Vehicle = vehicles[6],
                Client = clients[8],
                RentalPointGetFrom = rentalPoints[4],
                RentalTime = DateTime.Now.AddDays(-2),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[5],
                ReturnTime = DateTime.Now
            },
            
            new CarOnRent
            {
                Vehicle = vehicles[6],
                Client = clients[11],
                RentalPointGetFrom = rentalPoints[5],
                RentalTime = DateTime.Now.AddDays(-2),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[5],
                ReturnTime = DateTime.Now
            },
            
            new CarOnRent
            {
                Vehicle = vehicles[6],
                Client = clients[11],
                RentalPointGetFrom = rentalPoints[5],
                RentalTime = DateTime.Now.AddDays(-2),
                RentalDuration = TimeSpan.FromDays(2),
                RentalPointReturnTo = rentalPoints[6],
                ReturnTime = DateTime.Now
            },
            new CarOnRent
            {
                Vehicle = vehicles[6],
                Client = clients[11],
                RentalPointGetFrom = rentalPoints[6],
                RentalTime = DateTime.Now.AddDays(-4),
                RentalDuration = TimeSpan.FromDays(7)
            },
            
            new CarOnRent { Vehicle = vehicles[5], Client = clients[9], RentalPointGetFrom = rentalPoints[5], RentalTime = DateTime.Now.AddDays(-4),
                     RentalDuration = TimeSpan.FromDays(7)},
            
            new CarOnRent { Vehicle = vehicles[4], Client = clients[7], RentalPointGetFrom = rentalPoints[3], RentalTime = DateTime.Now.AddDays(-4),
                     RentalDuration = TimeSpan.FromDays(7)},
            
            new CarOnRent { Vehicle = vehicles[3], Client = clients[4], RentalPointGetFrom = rentalPoints[2], RentalTime = DateTime.Now.AddDays(-4),
                     RentalDuration = TimeSpan.FromDays(7)},
            
            new CarOnRent { Vehicle = vehicles[2], Client = clients[6], RentalPointGetFrom = rentalPoints[4], RentalTime = DateTime.Now.AddDays(-4),
                     RentalDuration = TimeSpan.FromDays(7)},
            
            new CarOnRent { Vehicle = vehicles[1], Client = clients[2], RentalPointGetFrom = rentalPoints[2], RentalTime = DateTime.Now.AddDays(-4),
                     RentalDuration = TimeSpan.FromDays(7)},
            
            new CarOnRent { Vehicle = vehicles[0], Client = clients[1], RentalPointGetFrom = rentalPoints[2], RentalTime = DateTime.Now.AddDays(-4),
                     RentalDuration = TimeSpan.FromDays(7)},

        };

    }
}
