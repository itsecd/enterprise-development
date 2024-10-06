using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseEnterpriceApp
{
    /// <summary>
    /// Ячейка склада
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Идентификатор ячейки
        /// </summary>
        public required int Id { get; set; }
        /// <summary>
        /// Товар
        /// </summary>
        public Product? Product { get; set; }
        /// <summary>
        /// Количество товара в ячейке
        /// </summary>
        public required int Quantity { get; set; }
    }
}
