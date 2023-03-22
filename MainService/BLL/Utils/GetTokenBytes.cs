using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utils
{
    public  class GetTokenBytes:IGetTokenBytes
    {
        private readonly IConfiguration _configuration;

        public GetTokenBytes(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public byte[] GetTokeBytes()
        {
            var jwtKey = _configuration.GetValue<string>("JwtSettings:Key");
            var keyBytes = Encoding.ASCII.GetBytes(jwtKey);
            return keyBytes;
        }
    }
}
