

namespace EnterpriseDevelopment.Tests
{
    public class EnterpriseDevelopmentTests: BaseTest
    {
       
        [Fact]
        public void ReturnAllCars()
        {   
             var allVehicles = vehicles.ToList();
            Assert.Equal(7, allVehicles.Count);
        }
        [Fact]
        public void ClientsWhoRentedCarOfTheSpecialModel()
        {
            string modelToSearch = "Model 1";
            var clientsWhoOrderedCurrentModel = vehiclesOnRent
                .Where(r => r.Vehicle.Model == modelToSearch)
                .Select(r => r.Client)
                .Distinct()
                .OrderBy(c => c.FullName)
                .ToList();
            Assert.Equal("Full Name 2", clientsWhoOrderedCurrentModel[0].FullName);
        }
        [Fact]
        public void CarsOnRentRightNow()
        {
            var carsOnRentRightNow = vehiclesOnRent
                .Where(r => r.ReturnTime == null)
                .ToList();
            Assert.Equal(vehicles[6], carsOnRentRightNow[0].Vehicle);
            Assert.Equal(vehicles[5], carsOnRentRightNow[1].Vehicle);
            Assert.Equal(vehicles[4], carsOnRentRightNow[2].Vehicle);
            Assert.Equal(vehicles[3], carsOnRentRightNow[3].Vehicle);
            Assert.Equal(vehicles[2], carsOnRentRightNow[4].Vehicle);
            Assert.Equal(vehicles[1], carsOnRentRightNow[5].Vehicle);
            Assert.Equal(vehicles[0], carsOnRentRightNow[6].Vehicle);
        }
        [Fact]
        public void Top5CarsOnRent()
        {
            var top5RentedCars = vehiclesOnRent
            .GroupBy(r => r.Vehicle)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();
            Assert.Equal(vehicles[6], top5RentedCars[0]);
            Assert.Equal(vehicles[5], top5RentedCars[1]);
            Assert.Equal(vehicles[4], top5RentedCars[2]);
            Assert.Equal(vehicles[3], top5RentedCars[3]);
            Assert.Equal(vehicles[2], top5RentedCars[4]);
        }
        [Fact]
        public void NumberOfRentForEachVehicle()
        {
            var vehicleRentalCounts = vehiclesOnRent
            .GroupBy(r => r.Vehicle)
            .Select(g => new
            {
                 Car = g.Key,
                Count = g.Count()
            })
            .ToList();
            Assert.Equal(2, vehicleRentalCounts[0].Count);
            Assert.Equal(3, vehicleRentalCounts[1].Count);
            Assert.Equal(4, vehicleRentalCounts[2].Count);
            Assert.Equal(5, vehicleRentalCounts[3].Count);
            Assert.Equal(6, vehicleRentalCounts[4].Count);
            Assert.Equal(1, vehicleRentalCounts[5].Count);
            Assert.Equal(1, vehicleRentalCounts[6].Count);
        }
        [Fact]
        public void RentalPointsWithMostRents()
        {
            var mostFrequentRentalPoints = vehiclesOnRent
    .GroupBy(r => r.RentalPointGetFrom)
    .OrderByDescending(g => g.Count())
    .ThenBy(g => g.Key.Name)
    .Select(g => g.Key)
    .Take(1)
    .ToList();
            Assert.Equal(rentalPoints[2], mostFrequentRentalPoints[0]);
        }

    }
}