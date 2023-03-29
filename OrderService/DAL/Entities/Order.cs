using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int BasketId { get; set; }
        public string OrderStatus { get; set; }
    }
}
