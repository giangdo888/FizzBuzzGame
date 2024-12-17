using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzGame.Server.Controllers
{
    [Route("/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GameController> _logger;
        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameByIdController(int id)
        {
            try
            {
                var result = await _gameService.GetGameByIdAsync(id);
                if (result == null)
                {
                    return NotFound($"Not found game with Id {id}");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: Database internal error");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGamesController()
        {
            try
            {
                var result = await _gameService.GetAllGamesAsync();
                if (!result.Any())
                {
                    return NotFound("No game found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: Database internal error");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameController(GameDTO gameDTO)
        {
            try
            {
                var result = await _gameService.CreateGameAsync(gameDTO);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Controller: Database internal error");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError("Controller: Invalid input");
                return BadRequest("Invalid input " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Controller: Invalid input");
                return BadRequest("Invalid input " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameController(int id, GameDTO gameDTO)
        {
            gameDTO.Id = id;
            try
            {
                var result = await _gameService.UpdateGameAsync(gameDTO);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Controller: Database internal error");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError("Controller: Invalid input");
                return BadRequest("Invalid input " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Controller: Invalid input");
                return BadRequest("Invalid input " + ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGameController(int id)
        {
            try
            {
                var result = await _gameService.DeleteGameAsync(id);
                if (result == false)
                {
                    return NotFound("Not found game with Id " + id);
                }
                return Ok("Successfully delete game with Id " + id);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Controller: Database internal error");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
