using System.ComponentModel.DataAnnotations;

namespace MainService.Models.Request
{
    public class RefreshTokenRequest
    {
        [Required] 
        public string ExpiredToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        
        
    }
}
