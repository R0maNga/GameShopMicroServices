using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Output.GameOutput
{
    public class GameOutputForGameService
    {
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int SoldGames { get; set; }
        public int GamesInStorage { get; set; }
    }
}
