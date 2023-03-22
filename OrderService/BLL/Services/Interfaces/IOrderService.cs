using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Input.OrderInput;
using BLL.Models.Output;

namespace BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task Create(CreateOrderInput order, CancellationToken token);
        Task Update(UpdateOrderInput order, CancellationToken token);
        Task<GetOrderOutput> GetOrderById(int id, CancellationToken token);
        Task ConfirmOrder(int id, CancellationToken token);
        Task CancelOrder(int id, CancellationToken token);
    }
}
