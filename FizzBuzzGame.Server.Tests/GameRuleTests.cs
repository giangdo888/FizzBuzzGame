using System;
using System.Threading.Tasks;
using FizzBuzzGame.Server.Controllers;
using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Interfaces.IServices;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace FizzBuzzGame.Server.Tests
{
    public class GameRuleControllerTests
    {
        private readonly GameRuleController _controller;
        private readonly Mock<IGameRuleService> _mockGameRuleService;
        private readonly Mock<ILogger<GameRuleController>> _mockLogger;

        public GameRuleControllerTests()
        {
            // Mocking IGameRuleService and ILogger
            _mockGameRuleService = new Mock<IGameRuleService>();
            _mockLogger = new Mock<ILogger<GameRuleController>>();

            // Initializing the controller with the mocked services
            _controller = new GameRuleController(_mockGameRuleService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateGameRuleController_ReturnsOkResult_WhenGameRuleIsCreated()
        {
            // Arrange
            var gameRuleDTO = new StandAloneGameRuleDTO { GameId = 1, Divisor = 3, Word = "Fizz" };
            _mockGameRuleService.Setup(service => service.CreateGameRuleAsync(gameRuleDTO)).ReturnsAsync(gameRuleDTO);

            // Act
            var result = await _controller.CreateGameRuleController(gameRuleDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<StandAloneGameRuleDTO>(okResult.Value);
            Assert.Equal(gameRuleDTO.GameId, returnValue.GameId);
            Assert.Equal(gameRuleDTO.Divisor, returnValue.Divisor);
            Assert.Equal(gameRuleDTO.Word, returnValue.Word);
        }

        [Fact]
        public async Task CreateGameRuleController_ReturnsBadRequest_WhenArgumentNullExceptionOccurs()
        {
            // Arrange
            var gameRuleDTO = new StandAloneGameRuleDTO(); // Invalid input, missing values
            _mockGameRuleService.Setup(service => service.CreateGameRuleAsync(gameRuleDTO))
                .ThrowsAsync(new ArgumentNullException("Invalid input"));

            // Act
            var result = await _controller.CreateGameRuleController(gameRuleDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Invalid input", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task UpdateGameRuleController_ReturnsOkResult_WhenGameRuleIsUpdated()
        {
            // Arrange
            var gameRuleDTO = new StandAloneGameRuleDTO { GameId = 1, Divisor = 3, Word = "Fizz" };
            _mockGameRuleService.Setup(service => service.UpdateGameRuleAsync(gameRuleDTO)).ReturnsAsync(gameRuleDTO);

            // Act
            var result = await _controller.UpdateGameRuleController(gameRuleDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<StandAloneGameRuleDTO>(okResult.Value);
            Assert.Equal(gameRuleDTO.GameId, returnValue.GameId);
            Assert.Equal(gameRuleDTO.Divisor, returnValue.Divisor);
            Assert.Equal(gameRuleDTO.Word, returnValue.Word);
        }

        [Fact]
        public async Task UpdateGameRuleController_ReturnsBadRequest_WhenArgumentExceptionOccurs()
        {
            // Arrange
            var gameRuleDTO = new StandAloneGameRuleDTO(); // Invalid input, missing values
            _mockGameRuleService.Setup(service => service.UpdateGameRuleAsync(gameRuleDTO))
                .ThrowsAsync(new ArgumentException("Invalid input"));

            // Act
            var result = await _controller.UpdateGameRuleController(gameRuleDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Invalid input", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteGameRuleController_ReturnsOkResult_WhenGameRuleIsDeleted()
        {
            // Arrange
            var gameRuleIdentifierDTO = new GameRuleIdentifierDTO { GameId = 1, Divisor = 3 };
            _mockGameRuleService.Setup(service => service.DeleteGameRuleAsync(gameRuleIdentifierDTO.GameId.Value, gameRuleIdentifierDTO.Divisor.Value))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteGameRuleController(gameRuleIdentifierDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Successfully delete rule with gameId {gameRuleIdentifierDTO.GameId} and divisor {gameRuleIdentifierDTO.Divisor}", okResult.Value);
        }

        [Fact]
        public async Task DeleteGameRuleController_ReturnsNotFound_WhenGameRuleDoesNotExist()
        {
            // Arrange
            var gameRuleIdentifierDTO = new GameRuleIdentifierDTO { GameId = 1, Divisor = 3 };
            _mockGameRuleService.Setup(service => service.DeleteGameRuleAsync(gameRuleIdentifierDTO.GameId.Value, gameRuleIdentifierDTO.Divisor.Value))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteGameRuleController(gameRuleIdentifierDTO);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Not found rule with gameId {gameRuleIdentifierDTO.GameId} and divisor {gameRuleIdentifierDTO.Divisor}", notFoundResult.Value);
        }
    }
}
