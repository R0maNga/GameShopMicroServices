using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace BLL.Models.Output.UserOutput
{
    public class GetUser
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        
        
    }
}
