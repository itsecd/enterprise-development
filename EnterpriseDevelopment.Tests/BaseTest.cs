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
            new ( "Name 1", "Address 1" ),
            new ( "Name 2", "Address 2" ),
            new ( "Name 3", "Address 3" ),
            new ( "Name 4", "Address 4" ),
            new ( "Name 5", "Address 5" ),
            new ( "Name 6", "Address 6" ),
            new ( "Name 7", "Address 7" ),
            new ( "Name 8", "Address 8" ),
            new ( "Name 9", "Address 9" ),
            new ( "Name 10", "Address 10" ),
            new ( "Name 11", "Address 11" ),
            new ( "Name 12", "Address 12" ),

        };

        public static List<Client> clients = new List<Client>
        {
            new ( "Passport 1", "Full Name 1", new DateTime(1991, 1, 1) ),
            new ( "Passport 2", "Full Name 2", new DateTime(1992, 2, 2) ),
            new ( "Passport 3", "Full Name 3", new DateTime(1993, 3, 3) ),
            new ( "Passport 4", "Full Name 4", new DateTime(1994, 4, 1) ),
            new ( "Passport 5", "Full Name 5", new DateTime(1995, 5, 5) ),
            new ( "Passport 6", "Full Name 6", new DateTime(1996, 6, 6) ),
            new ( "Passport 7", "Full Name 7", new DateTime(1997, 7, 7) ),
            new ( "Passport 8", "Full Name 8", new DateTime(1998, 8, 8) ),
            new ( "Passport 9", "Full Name 9", new DateTime(1999, 9, 9) ),
            new ( "Passport 10", "Full Name 10", new DateTime(2000, 10, 10) ),
            new ( "Passport 11", "Full Name 11", new DateTime(2001, 11, 11) ),
            new ( "Passport 12", "Full Name 12", new DateTime(2002, 12, 12) ),
        };

        public static List<CarOnRent> vehiclesOnRent = new List<CarOnRent>
        {
            new (
                vehicles[2],
                clients[3],
                rentalPoints[0],
                DateTime.Now.AddDays(-5),
                TimeSpan.FromDays(2),
                DateTime.Now.AddDays(-3),
                rentalPoints[4]
            ),
            new (
                vehicles[3],
                clients[5],
                rentalPoints[2],
                DateTime.Now.AddDays(-3),
                TimeSpan.FromDays(1),
                DateTime.Now.AddDays(-2),
                rentalPoints[3]

            ),
            new (
                vehicles[3],
                clients[6],
                rentalPoints[2],
                DateTime.Now.AddDays(-3),
                TimeSpan.FromDays(2),
                DateTime.Now.AddDays(-1),
                rentalPoints[5]
            ),
            new (
                vehicles[4],
                clients[5],
                rentalPoints[0],
                DateTime.Now.AddDays(-2),
                TimeSpan.FromDays(2),
                DateTime.Now,
                rentalPoints[4]
            ),
            new (vehicles[4],
                clients[7],
                rentalPoints[6],
                DateTime.Now.AddDays(-3),
                TimeSpan.FromDays(2),
                DateTime.Now.AddDays(-1),
                rentalPoints[7]
            ),
            new (
                vehicles[4],
                clients[8],
                rentalPoints[7],
                DateTime.Now.AddDays(-5),
                TimeSpan.FromDays(3),
                DateTime.Now.AddDays(-2),
                rentalPoints[3]
            ),
            new (
                vehicles[5],
                clients[2],
                rentalPoints[1],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(4),
                DateTime.Now,
                rentalPoints[2]
            ),
            new(
                vehicles[5],
                clients[0],
                rentalPoints[2],
                DateTime.Now.AddDays(-2),
                TimeSpan.FromDays(2),
                DateTime.Now,
                rentalPoints[2]
            ),
            new (
                vehicles[5],
                clients[9],
                rentalPoints[2],
                DateTime.Now.AddDays(-3),
                TimeSpan.FromDays(3),
                DateTime.Now,
                rentalPoints[3]
            ),

            new (
                vehicles[5],
                clients[10],
                rentalPoints[3],
                DateTime.Now.AddDays(-2),
                TimeSpan.FromDays(2),
                DateTime.Now,
                rentalPoints[2]
                )
            ,
            
            new (
                vehicles[6],
                clients[3],
                rentalPoints[1],
                DateTime.Now.AddDays(-2),
                TimeSpan.FromDays(2),
                DateTime.Now,
                rentalPoints[2]
            ),
            
            new(
                vehicles[6],
                clients[5],
                rentalPoints[2],
                DateTime.Now.AddDays(-10),
                TimeSpan.FromDays(5),
                DateTime.Now.AddDays(-5),
                rentalPoints[3]
            ),
            
            new (
                vehicles[6],
                clients[8],
                rentalPoints[4],
                DateTime.Now.AddDays(-2),
                TimeSpan.FromDays(2),
                DateTime.Now,
                rentalPoints[5]
            ),
            
            new (
                vehicles[6],
                clients[11],
                rentalPoints[5],
                DateTime.Now.AddDays(-6),
                TimeSpan.FromDays(4),
                DateTime.Now.AddDays(-2),
                rentalPoints[5]
            ),
            
            new (
                vehicles[6],
                clients[11],
                rentalPoints[5],
                DateTime.Now.AddDays(-8),
                TimeSpan.FromDays(4),
                DateTime.Now.AddDays(-4),
                rentalPoints[6]
            ),
            new (
                vehicles[6],
                clients[11],
                rentalPoints[6],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            
            new (
                vehicles[5],
                clients[9],
                rentalPoints[5],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            
            new (
                vehicles[4],
                clients[7],
                rentalPoints[3],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            
            new (
                vehicles[3],
               clients[4],
                rentalPoints[2],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            
            new ( 
                vehicles[2],
                clients[6],
                rentalPoints[4],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            
            new (
                vehicles[1],
                clients[2],
                rentalPoints[2],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            new (
                vehicles[0],
                clients[1],
                rentalPoints[2],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),

        };

    }
}
