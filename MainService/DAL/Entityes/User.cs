using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.Entityes
{
    public class User
    {
        
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<UserRefreshToken> UserRefreshTokens { get; set; }
        public Basket Basket { get; set; }

    }
}
