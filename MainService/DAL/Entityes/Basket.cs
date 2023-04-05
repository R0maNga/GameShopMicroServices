using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entityes
{
    public class Basket
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public List<BasketToGame> BasketToGame { get; set; }
        public int UserId { get; set; }
        
        public User User { get; set; }
    }
}
