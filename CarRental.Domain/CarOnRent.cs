namespace CarRental.Domain;

/// <summary>
/// Реализация выдачи автомобиля в прокат
/// </summary>
/// <param name="vehicle">Арендуемый автомобиль</param>
/// <param name="client">Клиент, который арендует автомобиль</param>
/// <param name="rentalPointGetFrom">Пункт аренды, откуда берется автомобиль.</param>
/// <param name="rentalTimeGet">Время, когда был взят автомобиль</param>
/// <param name="rentalDuration">Время, на которое был взят автомобиль</param>
/// <param name="rentalTimeReturn">Время, когда был возвращен автомобиль</param>
/// <param name="rentalPointReturnTo">Пункт, в который был возвращен автомобиль</param>
public class CarOnRent(Vehicle vehicle, RentalClient client, RentalPoint rentalPointGetFrom,
    DateTime rentalTimeGet, TimeSpan rentalDuration,
    DateTime? rentalTimeReturn = null, RentalPoint? rentalPointReturnTo = null)
{
    public Vehicle? Vehicle { get; set; } = vehicle;
    public RentalClient? Client { get; set; } = client;
    public RentalPoint? RentalPointGetFrom { get; set; } = rentalPointGetFrom;
    public DateTime? RentalTime { get; set; } = rentalTimeGet;
    public DateTime? ReturnTime { get; set; } = rentalTimeReturn;
    public TimeSpan? RentalDuration { get; set; } = rentalDuration;
    public RentalPoint? RentalPointReturnTo { get; set; } = rentalPointReturnTo;
}