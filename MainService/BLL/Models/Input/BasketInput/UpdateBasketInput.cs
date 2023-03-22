using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Input.BasketInput
{
    public class UpdateBasketInput
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public int UserId { get; set; }

    }
}
