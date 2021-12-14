using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Stock
    {
        public int StockId { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }
        public string productName { get; set; }
        public DateTime updateOn { get; set; }
    }
}
