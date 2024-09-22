namespace TransportClass
{
    /// <summary>
    /// Транспортное средство
    /// </summary>
    public class Transport
    {
        /// <summary>
        /// Список видов транспорта
        /// </summary>
        public enum VehicleType
        {
            Bus,
            Tram,
            Trolleybus
        }

        /// <summary>
        /// Индентификатор транспорта
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Номер автомобиля
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Вид транспорта
        /// </summary>
        public VehicleType Type { get; set; }

        /// <summary>
        /// Название модели транспорта
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// Тип транспорта (низкопольный / ненизкопльный) 
        /// </summary>
        public bool? IsLowFloor { get; set; }

        /// <summary>
        /// Максимальная вместительность
        /// </summary>
        public int? MaxCapacity { get; set; }

        /// <summary>
        /// Год выпуска транспорта
        /// </summary>
        public int? YearOfManufacture { get; set; }
    }
}