using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message, string queueName);
    }
}
