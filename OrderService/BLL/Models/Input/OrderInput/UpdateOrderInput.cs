using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Input.OrderInput
{
    public class UpdateOrderInput
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public int BasketId { get; set; }

        public decimal Price { get; set; }
    }
}
