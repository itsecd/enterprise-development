using System;

namespace TransportClass
{
    /// <summary>
    /// Маршрут
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Номер маршрута
        /// </summary>
        public string RouteNumber { get; set; }

        /// <summary>
        /// Назначенный транспорт
        /// </summary>
        public Transport AssignedTransport { get; set; }

        /// <summary>
        /// Назначенный водитель
        /// </summary>
        public Driver AssignedDriver { get; set; }

        /// <summary>
        /// Время начала маршрута 
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время окончания маршрута
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}