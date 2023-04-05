using DAL.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Output.BasketOutput
{
    public class GetBasketOutput
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public int UserId { get; set; }
    }
}
