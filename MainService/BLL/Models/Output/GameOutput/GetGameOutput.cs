using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Output.GameOutput
{
    public class GetGameOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public decimal Prie { get; set; }
        public int Quantity { get; set; }
    }
}
