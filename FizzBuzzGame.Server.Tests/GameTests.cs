using System;
using System.Linq;
using System.Threading.Tasks;
using FizzBuzzGame.Server.Controllers;
using FizzBuzzGame.Server.DTOs;
using FizzBuzzGame.Server.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FizzBuzzGame.Server.Tests
{
    public class GameControllerTests
    {
        private readonly GameController _controller;
        private readonly Mock<IGameService> _mockGameService;

        public GameControllerTests()
        {
            // Mocking IGameService
            _mockGameService = new Mock<IGameService>();

            // Initialize the controller with mocked service
            _controller = new GameController(_mockGameService.Object, new Mock<ILogger<GameController>>().Object);
        }

        [Fact]
        public async Task GetGameByIdController_ReturnsOkResult_WhenGameExists()
        {
            // Arrange
            var gameId = 1;
            var gameDTO = new GameDTO { Id = gameId, Name = "Game 1" };
            _mockGameService.Setup(service => service.GetGameByIdAsync(gameId)).ReturnsAsync(gameDTO);

            // Act
            var result = await _controller.GetGameByIdController(gameId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GameDTO>(okResult.Value);
            Assert.Equal(gameId, returnValue.Id);
        }

        [Fact]
        public async Task GetGameByIdController_ReturnsNotFound_WhenGameDoesNotExist()
        {
            // Arrange
            var gameId = 1;
            _mockGameService.Setup(service => service.GetGameByIdAsync(gameId)).ReturnsAsync((GameDTO)null);

            // Act
            var result = await _controller.GetGameByIdController(gameId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Not found game with Id {gameId}", notFoundResult.Value);
        }

        [Fact]
        public async Task GetAllGamesController_ReturnsOkResult_WhenGamesExist()
        {
            // Arrange
            var gameList = new[] { new GameDTO { Id = 1, Name = "Game 1" } };
            _mockGameService.Setup(service => service.GetAllGamesAsync()).ReturnsAsync(gameList);

            // Act
            var result = await _controller.GetAllGamesController();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GameDTO[]>(okResult.Value);
            Assert.Single(returnValue);
        }

        [Fact]
        public async Task GetAllGamesController_ReturnsNotFound_WhenNoGamesExist()
        {
            // Arrange
            _mockGameService.Setup(service => service.GetAllGamesAsync()).ReturnsAsync(Array.Empty<GameDTO>());

            // Act
            var result = await _controller.GetAllGamesController();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No game found", notFoundResult.Value);
        }

        [Fact]
        public async Task CreateGameController_ReturnsOkResult_WhenGameIsCreated()
        {
            // Arrange
            var gameDTO = new GameDTO { Name = "New Game" };
            _mockGameService.Setup(service => service.CreateGameAsync(gameDTO)).ReturnsAsync(gameDTO);

            // Act
            var result = await _controller.CreateGameController(gameDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GameDTO>(okResult.Value);
            Assert.Equal("New Game", returnValue.Name);
        }

        [Fact]
        public async Task UpdateGameController_ReturnsOkResult_WhenGameIsUpdated()
        {
            // Arrange
            var gameId = 1;
            var gameDTO = new GameDTO { Id = gameId, Name = "Updated Game" };
            _mockGameService.Setup(service => service.UpdateGameAsync(gameDTO)).ReturnsAsync(gameDTO);

            // Act
            var result = await _controller.UpdateGameController(gameId, gameDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GameDTO>(okResult.Value);
            Assert.Equal("Updated Game", returnValue.Name);
        }

        [Fact]
        public async Task DeleteGameController_ReturnsOkResult_WhenGameIsDeleted()
        {
            // Arrange
            var gameId = 1;
            _mockGameService.Setup(service => service.DeleteGameAsync(gameId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteGameController(gameId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Successfully delete game with Id {gameId}", okResult.Value);
        }

        [Fact]
        public async Task DeleteGameController_ReturnsNotFound_WhenGameDoesNotExist()
        {
            // Arrange
            var gameId = 1;
            _mockGameService.Setup(service => service.DeleteGameAsync(gameId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteGameController(gameId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Not found game with Id {gameId}", notFoundResult.Value);
        }
    }
}
