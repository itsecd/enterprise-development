using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseEnterpriceApp
{
    /// <summary>
    /// Поставка товара
    /// </summary>
    public class Supply
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public required int Id { get; set; }
        /// <summary>
        /// Товар
        /// </summary>
        public required Product Product { get; set; }
        /// <summary>
        /// Организация
        /// </summary>
        public required Organization Organization { get; set; }
        /// <summary>
        /// Дата поставки
        /// </summary>
        public required DateTime SupplyDate { get; set; }
        /// <summary>
        /// Количество поставленного товара
        /// </summary>
        public required int Quantity { get; set; }
    }
}
