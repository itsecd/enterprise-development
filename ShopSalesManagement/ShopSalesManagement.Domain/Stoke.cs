namespace ShopSalesManagement.Domain
{
    /// <summary>
    /// Представляет наличие товара в магазине.
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// Уникальный идентификатор записи.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Идентификатор магазина.
        /// </summary>
        public int StoreId { get; set; }
        /// <summary>
        /// Количество товара в наличии.
        /// </summary>
        public int Quantity { get; set; }
    }
}