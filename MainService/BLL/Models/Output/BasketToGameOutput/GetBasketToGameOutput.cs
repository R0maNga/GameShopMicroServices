using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Output.BasketToGameOutput
{
    public class GetBasketToGameOutput
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int GameId { get; set; }
        public int GameAmount { get; set; }
        public  GameForBusketToGameOutput Game { get; set; }
    }
}
