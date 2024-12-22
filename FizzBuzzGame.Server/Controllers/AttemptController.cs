using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Interfaces.IServices;
using FizzBuzzGame.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzGame.Server.Controllers
{
    [Route("/attempts")]
    [ApiController]
    public class AttemptController : ControllerBase
    {
        private readonly IAttemptService _attemptService;
        private readonly ILogger<AttemptController> _logger;

        public AttemptController(IAttemptService attemptService, ILogger<AttemptController> logger)
        {
            _attemptService = attemptService;
            _logger = logger;
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAndStartAttemptController(InitialAttemptDTO attemptDTO)
        {
            try
            {
                var result = await _attemptService.CreateAndStartAttemptAsync(attemptDTO);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Controller: Database internal error");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Controller: No more suitable random number to generate");
                return StatusCode(409, "Conflict: " + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError("Controller: Invalid input");
                return BadRequest("Invalid input: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError("Controller: Invalid input");
                return BadRequest("Invalid input: " + ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("{id}/answer")]
        public IActionResult HandleAttemptAnswerController(int id, AttemptAnswerDTO attemptAnswerDTO)
        {
            attemptAnswerDTO.Id = id;
            try
            {
                var result = _attemptService.HandleAttemptAsnwer(attemptAnswerDTO);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Controller: No more suitable random number to generate");
                return StatusCode(409, "Conflict: " + ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError("Controller: Invalid input");
                return BadRequest("Invalid input: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: Invalid game");
                return BadRequest("Invalid game: " + ex.Message);
            }
        }

        //[Authorize]
        [HttpPut("{id}/finalize")]
        public async Task<IActionResult> FinalizeAttemptController(int id)
        {
            try
            {
                var result = await _attemptService.FinalizeAttemptAsync(id);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Controller: Database internal error");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: ALready finalize attempt");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttemptResultController(int id)
        {
            try
            {
                var result = await _attemptService.GetAttemptResultAsync(id);
                if (result == null)
                {
                    return NotFound($"Not found attempt with Id {id}");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller: Database internal error");
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
