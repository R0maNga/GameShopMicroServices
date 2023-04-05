using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entityes
{
    public class BasketToGame
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int GameId { get; set; }
        public int GameAmount { get; set; }
        public Basket Basket { get; set; }
        public Game Game { get; set; }
    }
}
