namespace TransportClass
{
    /// <summary>
    /// Водитель
    /// </summary>
    public class Driver
    {
        /// <summary>
        /// Индетификатор водителя
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Паспорт
        /// </summary>
        public string Passport { get; set; }

        /// <summary>
        /// Лицензия водителя
        /// </summary>
        public string DriverLicense { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}