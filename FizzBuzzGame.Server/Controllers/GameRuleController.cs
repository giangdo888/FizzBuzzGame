using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Interfaces.IServices;
using FizzBuzzGame.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzGame.Server.Controllers
{
    [Route("/rules")]
    [ApiController]
    public class GameRuleController : ControllerBase
    {
        private readonly IGameRuleService _gameRuleService;
        private readonly ILogger<GameRuleController> _logger;

        public GameRuleController(IGameRuleService gameRuleService, ILogger<GameRuleController> logger)
        {
            _gameRuleService = gameRuleService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameRuleController(StandAloneGameRuleDTO gameRuleDTO)
        {
            try
            {
                var result = await _gameRuleService.CreateGameRuleAsync(gameRuleDTO);
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

        [HttpPut]
        public async Task<IActionResult> UpdateGameRuleController(StandAloneGameRuleDTO gameRuleDTO)
        {
            try
            {
                var result = await _gameRuleService.UpdateGameRuleAsync(gameRuleDTO);
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
        public async Task<IActionResult> DeleteGameRuleController(GameRuleIdentifierDTO gameRuleIdentifierDTO)
        {
            try
            {
                var result = await _gameRuleService.DeleteGameRuleAsync(gameRuleIdentifierDTO.GameId.Value, gameRuleIdentifierDTO.Divisor.Value);
                if (result == false)
                {
                    return NotFound("Not found rule with gameId " + gameRuleIdentifierDTO.GameId.Value + " and divisor " + gameRuleIdentifierDTO.Divisor.Value);
                }
                return Ok("Successfully delete rule with gameId " + gameRuleIdentifierDTO.GameId.Value + " and divisor " + gameRuleIdentifierDTO.Divisor.Value);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Controller: Database internal error");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
