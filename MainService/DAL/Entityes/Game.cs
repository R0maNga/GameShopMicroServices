using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entityes
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public decimal Prie { get; set; }
        public int Quantity { get; set; }
        public List<BasketToGame> BasketToGame { get; set; }
    }
}
