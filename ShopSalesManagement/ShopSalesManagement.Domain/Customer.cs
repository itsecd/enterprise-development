namespace ShopSalesManagement.Domain
{
    /// <summary>
    /// Представляет покупателя.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Уникальный идентификатор покупателя.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Номер карты покупателя.
        /// </summary>
        public string CardNumber { get; set; }
        /// <summary>
        /// Полное имя покупателя.
        /// </summary>
        public string FullName { get; set; }
    }
}