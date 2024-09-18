namespace CarRental.Domain;


/// <param name="vehicle">Арендуемый автомобиль</param>
/// <param name="client">Клиент, который арендует автомобиль</param>
/// <param name="rentalPointGetFrom">Пункт аренды, откуда берется автомобиль</param>
/// <param name="rentalTimeGet">Время, когда был взят автомобиль</param>
/// <param name="rentalTimeReturn">Время, когда был возвращен автомобиль</param>
/// <param name="rentalDuration">Время, на которое был взят автомобиль</param>
/// <param name="rentalPointReturnTo">Пункт, в который был возвращен автомобиль</param>
public class CarOnRent(Vehicle vehicle, RentalClient client, RentalPoint rentalPointGetFrom,
    DateTime rentalTimeGet, TimeSpan rentalDuration,
    DateTime? rentalTimeReturn = null, RentalPoint? rentalPointReturnTo = null)
{
    /// <summary>
    /// Арендуемый автомобиль
    /// </summary>
    public Vehicle? Vehicle { get; set; } = vehicle;
    /// <summary>
    /// Клиент, который арендует автомобиль
    /// </summary>
    public RentalClient? Client { get; set; } = client;
    /// <summary>
    /// Пункт аренды, откуда берется автомобиль
    /// </summary>
    public RentalPoint? RentalPointGetFrom { get; set; } = rentalPointGetFrom;
    /// <summary>
    /// Время, когда был взят автомобиль
    /// </summary>
    public DateTime? RentalTime { get; set; } = rentalTimeGet;
    /// <summary>
    /// Время, когда был возвращен автомобиль
    /// </summary>
    public DateTime? ReturnTime { get; set; } = rentalTimeReturn;
    /// <summary>
    /// Время, на которое был взят автомобиль
    /// </summary>
    public TimeSpan? RentalDuration { get; set; } = rentalDuration;
    /// <summary>
    /// Пункт, в который был возвращен автомобиль
    /// </summary>
    public RentalPoint? RentalPointReturnTo { get; set; } = rentalPointReturnTo;
}