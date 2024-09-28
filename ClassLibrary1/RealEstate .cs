namespace RealEstateAgency.Domain
{
    /// <summary>
    /// объект недвижимости
    /// </summary>
    public class RealEstate // Сделать класс публичным
    {
        public enum PropertyType { Residential, Uninhabitable }

        /// <summary>
        /// тип объекта недвижимости (жилое/нежилое)
        /// </summary>
        public PropertyType Type { get; set; }

        /// <summary>
        /// адресс
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// площадь
        /// </summary>
        public double Square { get; set; }

        /// <summary>
        /// количество комнат
        /// </summary>
        public int NumberOfRooms { get; set; }

        /// <summary>
        /// Конструктор для создания объекта недвижимости
        /// </summary>
        /// <param name="type">Тип объекта (жилое или не жилое)</param>
        /// <param name="address">адрес объекта</param>
        /// <param name="square">площадь объекта</param>
        /// <param name="numberOfRooms">количество комнат</param>
        public RealEstate(PropertyType type, string address, double square, int numberOfRooms)
        {
            Type = type;
            Address = address;
            Square = square;
            NumberOfRooms = numberOfRooms;
        }
    }
}
