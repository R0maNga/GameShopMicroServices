using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IGamesHostedService
    {
        void ProcessEvent(string message, CancellationToken token);
        void GameEvent(string message, CancellationToken token);
    }
}
