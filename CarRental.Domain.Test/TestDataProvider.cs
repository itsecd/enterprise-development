namespace CarRental.Domain.Test;

public class TestDataProvider
{
    public List<Vehicle> vehicles = new List<Vehicle>
    {
        new("Number 1", "Model 1", "Color 1"),
        new("Number 2", "Model 2", "Color 2"),
        new("Number 3", "Model 3", "Color 3"),
        new("Number 4", "Model 4", "Color 4"),
        new("Number 5", "Model 5", "Color 5"),
        new("Number 6", "Model 6", "Color 6"),
        new("Number 7", "Model 7", "Color 7")
    };

    public List<RentalPoint> rentalPoints = new List<RentalPoint>
    {
        new("Name 1", "Address 1"),
        new("Name 2", "Address 2"),
        new("Name 3", "Address 3"),
        new("Name 4", "Address 4"),
        new("Name 5", "Address 5"),
        new("Name 6", "Address 6")
    };

    public List<RentalClient> clients = new List<RentalClient>
    {
        new ( "Passport 1", "Full Name 1", new DateTime(1991, 1, 1) ),
        new ( "Passport 2", "Full Name 2", new DateTime(1992, 2, 2) ),
        new ( "Passport 3", "Full Name 3", new DateTime(1993, 3, 3) ),
        new ( "Passport 4", "Full Name 4", new DateTime(1994, 4, 4) ),
        new ( "Passport 5", "Full Name 5", new DateTime(1995, 5, 5) )
    };

    public List<CarOnRent> vehiclesOnRent;

    public TestDataProvider()
    {
        vehiclesOnRent = new List<CarOnRent>
        {
            new(
                vehicles[2],
                clients[3],
                rentalPoints[0],
                DateTime.Now.AddDays(-5),
                TimeSpan.FromDays(2),
                DateTime.Now.AddDays(-3),
                rentalPoints[4]
            ),
            new(
                vehicles[3],
                clients[4],
                rentalPoints[2],
                DateTime.Now.AddDays(-3),
                TimeSpan.FromDays(1),
                DateTime.Now.AddDays(-2),
                rentalPoints[3]
            ),
            new(
                vehicles[3],
                clients[2],
                rentalPoints[2],
                DateTime.Now.AddDays(-3),
                TimeSpan.FromDays(2),
                DateTime.Now.AddDays(-1),
                rentalPoints[5]
            ),
            new(
                vehicles[4],
                clients[3],
                rentalPoints[0],
                DateTime.Now.AddDays(-2),
                TimeSpan.FromDays(2),
                DateTime.Now,
                rentalPoints[4]
            ),
            new(
                vehicles[4],
                clients[4],
                rentalPoints[5],
                DateTime.Now.AddDays(-3),
                TimeSpan.FromDays(2),
                DateTime.Now.AddDays(-1),
                rentalPoints[2]
            ),
            new(
                vehicles[4],
                clients[1],
                rentalPoints[2],
                DateTime.Now.AddDays(-5),
                TimeSpan.FromDays(3),
                DateTime.Now.AddDays(-2),
                rentalPoints[3]
            ),
            new(
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
            new(
                vehicles[5],
                clients[3],
                rentalPoints[2],
                DateTime.Now.AddDays(-3),
                TimeSpan.FromDays(3),
                DateTime.Now,
                rentalPoints[3]
            ),
            new(
                vehicles[5],
                clients[4],
                rentalPoints[3],
                DateTime.Now.AddDays(-2),
                TimeSpan.FromDays(2),
                DateTime.Now,
                rentalPoints[2]
            ),
            new(
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
                clients[2],
                rentalPoints[2],
                DateTime.Now.AddDays(-10),
                TimeSpan.FromDays(5),
                DateTime.Now.AddDays(-5),
                rentalPoints[3]
            ),
            new(
                vehicles[6],
                clients[4],
                rentalPoints[4],
                DateTime.Now.AddDays(-2),
                TimeSpan.FromDays(2),
                DateTime.Now,
                rentalPoints[5]
            ),
            new(
                vehicles[6],
                clients[1],
                rentalPoints[5],
                DateTime.Now.AddDays(-6),
                TimeSpan.FromDays(4),
                DateTime.Now.AddDays(-2),
                rentalPoints[5]
            ),
            new(
                vehicles[6],
                clients[0],
                rentalPoints[5],
                DateTime.Now.AddDays(-8),
                TimeSpan.FromDays(4),
                DateTime.Now.AddDays(-4),
                rentalPoints[2]
            ),
            new(
                vehicles[6],
                clients[2],
                rentalPoints[2],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            new(
                vehicles[5],
                clients[1],
                rentalPoints[5],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            new(
                vehicles[4],
                clients[3],
                rentalPoints[3],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            new(
                vehicles[3],
                clients[4],
                rentalPoints[2],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            new(
                vehicles[2],
                clients[1],
                rentalPoints[4],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            new(
                vehicles[1],
                clients[2],
                rentalPoints[2],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
            new(
                vehicles[0],
                clients[1],
                rentalPoints[2],
                DateTime.Now.AddDays(-4),
                TimeSpan.FromDays(7)
            ),
        };

    }
    
}


