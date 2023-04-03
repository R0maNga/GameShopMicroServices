using AutoMapper;
using BLL.Models.Input.OrderInput;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models.Request.OrderRequest;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest order, CancellationToken token)
        {
            try
            {
                var mappedData = _mapper.Map<CreateOrderInput>(order);
                await _orderService.Create(mappedData, token);
                return Ok("Order Created");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrderRequest order, CancellationToken token)
        {
            try
            {
               var foundOrder = await _orderService.GetOrderById(order.Id, token);
               if (foundOrder is null)
                   return BadRequest();

               var mappedOrder = _mapper.Map<UpdateOrderInput>(foundOrder);
               await _orderService.Update(mappedOrder, token);
               return Ok("Order Update");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("confirm-order")]
        public async Task<IActionResult> ConfirmOrder(int id, CancellationToken token)
        {
            try
            {
                
                await _orderService.ConfirmOrder(id, token);
                return Ok("Order Confirmed");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("cancel-order")]
        public async Task<IActionResult> CancelOrder(int id, CancellationToken token)
        {
            try
            {

                await _orderService.CancelOrder(id, token);
                return Ok("Order Canceled");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
