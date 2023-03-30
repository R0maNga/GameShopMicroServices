using AutoMapper;
using BLL.Models.Input.BasketToGameInput;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Contracts.Finders;
using MainService.Models.Request.BasketToGameRequest;
using MainService.Models.Response.BasketToGameResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace MainService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketToGameController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBasketToGameService _service;
        private readonly IMessageProducer _messageProducer;

        public BasketToGameController(IMapper mapper, IBasketToGameService service, IMessageProducer message)
        {
            _mapper = mapper;
            _service = service;
            _messageProducer = message;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasketToGame(CreateBasketToGameRequest basketToGame, CancellationToken token)
        {
            try
            {
                var mappedData = _mapper.Map<CreateBasketToGameInput>(basketToGame);
                await _service.CreateBasketToGame(mappedData, token);

                return Ok("BasketToGame Created");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBasketToGame(UpdateBasketToGameRequest basketToGame, CancellationToken token)
        {
            try
            {
                var foundData = await _service.GetBasketToGameById(basketToGame.Id, token);
                if (foundData is null)

                    return BadRequest();

                var mappedData = _mapper.Map<UpdateBasketToGameInput>(foundData);
                await _service.UpdateBasketToGame(mappedData, token);

                return Ok("BasketToGame Updated");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasketToGame(DeleteBasketToGameRequest basketToGame, CancellationToken token)
        {
            try
            {
                var foundData = await _service.GetBasketToGameById(basketToGame.Id, token);
                if (foundData is null)

                    return BadRequest();

                var mappedData = _mapper.Map<DeleteBasketToGameInput>(foundData);
                await _service.DeleteBasketToGame(mappedData, token);

                return Ok("BasketToGame Deleted");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBasketToGameByBasketId(int id, CancellationToken token, bool includeGame = true)
        {
            try
            {
                var basketToGame = await _service.GetAllBasketToGameByBasketId(id, token, includeGame);
                var total = _service.CalculateTotalPrice(basketToGame);
                
                Order order = new Order()
                {
                    Price = total,
                    BasketId = basketToGame[0].BasketId,
                    OrderStatus = "waiting"

                };
                
                _messageProducer.SendMessage(order);
                return Ok(basketToGame);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
