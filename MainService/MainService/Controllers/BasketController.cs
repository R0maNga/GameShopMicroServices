using AutoMapper;
using BLL.Models.Input.BasketInput;
using BLL.Models.Input.GameInput;
using BLL.Services;
using BLL.Services.Interfaces;
using MainService.Models.Request.BasketRequest;
using MainService.Models.Request.GemeRequest;
using Microsoft.AspNetCore.Mvc;

namespace MainService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController:ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IBasketService _basketService;

        public BasketController(IMapper mapper, IBasketService basketService)
        {
            _mapper = mapper;
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket(CreateBasketRequest basket, CancellationToken token)
        {
            try
            {
                var mappedBasket = _mapper.Map<CreateBasketInput>(basket);
                await _basketService.CreateBasket(mappedBasket, token);

                return Ok("Basket Created");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBasket(UpdateBasketRequest basket, CancellationToken token)
        {
            try
            {
                var foundBasket = await _basketService.GetBasketById(basket.Id, token);
                if (foundBasket is null)

                    return BadRequest();

                var mappedBasket = _mapper.Map<UpdateBasketInput>(basket);
                await _basketService.UpdateBasket(mappedBasket, token);

                return Ok("Basket Updated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
