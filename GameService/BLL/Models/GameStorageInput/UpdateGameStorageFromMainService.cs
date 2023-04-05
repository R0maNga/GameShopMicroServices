using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.GameStorageInput
{
    public class UpdateGameStorageFromMainService
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int SoldGames { get; set; }
        public int BasketId { get; set; }
    }
}
