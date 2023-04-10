using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IGamesHostedService
    {
        void CheckGameStorageEvent(string message, CancellationToken token);
        void AddGameEvent(string message, CancellationToken token);
    }
}
