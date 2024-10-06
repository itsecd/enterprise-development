using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseEnterpriseApp
{/// <summary>
 /// Товар
 /// </summary>
    public class Product
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public required int Id { get; set; }
        /// <summary>
        /// Код товара
        /// </summary>
        public required int Code { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public required string Name { get; set; }
    }
}
