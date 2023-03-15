using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entityes
{
    [Table("UserRefreshToken")]
    public class UserRefreshToken
    {
        [Key]
        public int UserRefreshTokenId { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        [NotMapped]
        public bool IsActive
        {
            get
            {
                return ExpirationDate > DateTime.UtcNow;
            }
        }

        public string IpAdress { get; set; }
        public bool IsInvalidated { get; set; }
        public int Id { get; set; }
        [ForeignKey("Id")]
        public virtual User User { get; set; }
        
        

    }
}
