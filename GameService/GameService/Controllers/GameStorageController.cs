using AutoMapper;
using BLL.Models.GameStorageInput;
using BLL.Services.Interfaces;
using DAL.Contracts.Repositories;
using GameService.Models.GameStorageRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameStorageController : ControllerBase
    {
        private readonly IGameStorageService _gameStorageService;
        private readonly IMapper _mapper;


        public GameStorageController(IGameStorageService gameStorageService, IMapper mapper)
        {
            _gameStorageService = gameStorageService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameStorage(CreateGameStorageRequest gameStorage, CancellationToken token)
        {
            try
            {
                var mappedData = _mapper.Map<CreateGameStorageInput>(gameStorage);
                await _gameStorageService.Create(mappedData, token);

                return Ok("GameStorageCreated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGameStorage(UpdateGameStorageRequest gameStorage, CancellationToken token)
        {
            try
            {
                var foundedGameStorage = await _gameStorageService.GetGameStorageById(gameStorage.Id, token);
                if (foundedGameStorage is null)

                    return BadRequest("Bad Id");
                    
                
                var mappedData = _mapper.Map<UpdateGameStorageInput>(gameStorage);
                await _gameStorageService.Update(mappedData, token);

                return Ok("GameStorage Updated");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGameStorage(DeleteGameStorageRequest gameStorage, CancellationToken token)
        {
            try
            {
                var mappedData = _mapper.Map<DeleteGameStorageInput>(gameStorage);
                await _gameStorageService.Delete(mappedData, token);

                return Ok("GameStorage Deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetGameStorageById(int id, CancellationToken token)
        {
            try
            {
               var foundGameStorage = await _gameStorageService.GetGameStorageById(id, token);
               
               return Ok(foundGameStorage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
