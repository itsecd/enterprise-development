namespace ShopSalesManagement.Domain
{
    /// <summary>
    /// Представляет покупку в рамках продажи.
    /// </summary>
    public class Purchase
    {
        /// <summary>
        /// Уникальный идентификатор покупки.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Идентификатор продажи.
        /// </summary>
        public int SaleId { get; set; }
        /// <summary>
        /// Идентификатор товара.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Количество товара.
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Цена за единицу товара.
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// Общая сумма за товар.
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}