using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Input.UserInput;
using BLL.Models.Output.RefreshTokenOutput;
using DAL.Entityes;


namespace BLL.Services.Interfaces
{
    public interface ITokenChecker
    {
        UserRefreshTokenOutput CheckToken (RefreshTokenRequest request);
        public  void UpdateToken(UserRefreshTokenOutput request);
        
    }
}
