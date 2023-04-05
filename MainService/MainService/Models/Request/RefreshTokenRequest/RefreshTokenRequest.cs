using System.ComponentModel.DataAnnotations;

namespace MainService.Models.Request.RefreshTokenRequest
{
    public class RefreshTokenRequest
    {
        [Required]
        public string ExpiredToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }


    }
}
