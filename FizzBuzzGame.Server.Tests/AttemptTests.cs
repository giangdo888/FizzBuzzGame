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
    public class AttemptControllerTests
    {
        private readonly AttemptController _controller;
        private readonly Mock<IAttemptService> _mockAttemptService;
        private readonly Mock<ILogger<AttemptController>> _mockLogger;

        public AttemptControllerTests()
        {
            // Mocking IAttemptService and ILogger
            _mockAttemptService = new Mock<IAttemptService>();
            _mockLogger = new Mock<ILogger<AttemptController>>();

            // Initializing the controller with the mocked services
            _controller = new AttemptController(_mockAttemptService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task CreateAndStartAttemptController_ReturnsOkResult_WhenAttemptIsCreated()
        {
            // Arrange
            var attemptDTO = new InitialAttemptDTO { Id = 1, Duration = 30, UserId = 1, GameId = 1 };
            var expectedResult = new InittialAttemptQuestionDTO { Id = 1, Question = 15, TimeLimitEachQuestion = 10 };
            _mockAttemptService.Setup(service => service.CreateAndStartAttemptAsync(attemptDTO)).ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.CreateAndStartAttemptController(attemptDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<InittialAttemptQuestionDTO>(okResult.Value);
            Assert.Equal(expectedResult.Id, returnValue.Id);
            Assert.Equal(expectedResult.Question, returnValue.Question);
            Assert.Equal(expectedResult.TimeLimitEachQuestion, returnValue.TimeLimitEachQuestion);
        }

        [Fact]
        public async Task CreateAndStartAttemptController_ReturnsBadRequest_WhenArgumentNullExceptionOccurs()
        {
            // Arrange
            var attemptDTO = new InitialAttemptDTO(); // Invalid input, missing values
            _mockAttemptService.Setup(service => service.CreateAndStartAttemptAsync(attemptDTO))
                .ThrowsAsync(new ArgumentNullException("Invalid input"));

            // Act
            var result = await _controller.CreateAndStartAttemptController(attemptDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Invalid input", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task HandleAttemptAnswerController_ReturnsOkResult_WhenAnswerIsHandled()
        {
            // Arrange
            var attemptAnswerDTO = new AttemptAnswerDTO { Id = 1, Answer = "Fizz" };
            var expectedResult = new AttemptResultAndNewQuestionDTO
            {
                IsCorrect = true,
                IsTimeOut = false,
                Question = new AttemptQuestionDTO { Id = 1, Question = 5 }
            };
            _mockAttemptService.Setup(service => service.HandleAttemptAsnwer(attemptAnswerDTO)).Returns(expectedResult);

            // Act
            var result = _controller.HandleAttemptAnswerController(attemptAnswerDTO.Id, attemptAnswerDTO);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AttemptResultAndNewQuestionDTO>(okResult.Value);
            Assert.Equal(expectedResult.IsCorrect, returnValue.IsCorrect);
            Assert.Equal(expectedResult.IsTimeOut, returnValue.IsTimeOut);
            Assert.Equal(expectedResult.Question.Id, returnValue.Question.Id);
            Assert.Equal(expectedResult.Question.Question, returnValue.Question.Question);
        }

        [Fact]
        public void HandleAttemptAnswerController_ReturnsBadRequest_WhenInvalidOperationExceptionOccurs()
        {
            // Arrange
            var attemptId = 1;
            var attemptAnswerDTO = new AttemptAnswerDTO { Id = attemptId, Answer = "some answer" };

            var exceptionMessage = "No more suitable random number to generate";
            _mockAttemptService.Setup(service => service.HandleAttemptAsnwer(It.IsAny<AttemptAnswerDTO>()))
                .Throws(new InvalidOperationException(exceptionMessage));

            // Act
            var result = _controller.HandleAttemptAnswerController(attemptId, attemptAnswerDTO);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);  // Assert it's an ObjectResult
            Assert.Equal(409, objectResult.StatusCode);  // Assert the status code is 409
            Assert.Equal("Conflict: " + exceptionMessage, objectResult.Value);  // Assert the error message
        }


        [Fact]
        public async Task FinalizeAttemptController_ReturnsOkResult_WhenAttemptIsFinalized()
        {
            // Arrange
            var attemptId = 1;
            var expectedResult = new AttemptResultDTO
            {
                Id = attemptId,
                Duration = 10,
                UserId = 1,
                GameId = 1,
                CorrectNumber = 5,
                IncorrectNumber = 3,
                Score = 8
            };

            // Mock the service to return an AttemptResultDTO when FinalizeAttemptAsync is called
            _mockAttemptService.Setup(service => service.FinalizeAttemptAsync(attemptId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.FinalizeAttemptController(attemptId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AttemptResultDTO>(okResult.Value); // Ensure it's the correct type
            Assert.Equal(expectedResult, returnValue); // Ensure the returned value matches the expected result
        }



        [Fact]
        public async Task FinalizeAttemptController_ReturnsBadRequest_WhenFinalizeAttemptExceptionOccurs()
        {
            // Arrange
            var attemptId = 1;
            _mockAttemptService.Setup(service => service.FinalizeAttemptAsync(attemptId))
                .ThrowsAsync(new Exception("Already finalized attempt"));

            // Act
            var result = await _controller.FinalizeAttemptController(attemptId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Contains("Already finalized attempt", badRequestResult.Value.ToString());
        }

        [Fact]
        public async Task GetAttemptResultController_ReturnsOkResult_WhenAttemptIsFound()
        {
            // Arrange
            var attemptId = 1;
            var attemptResult = new AttemptResultDTO { Id = attemptId, Duration = 30, UserId = 1, GameId = 1, CorrectNumber = 10, IncorrectNumber = 5, Score = 50 };
            _mockAttemptService.Setup(service => service.GetAttemptResultAsync(attemptId)).ReturnsAsync(attemptResult);

            // Act
            var result = await _controller.GetAttemptResultController(attemptId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AttemptResultDTO>(okResult.Value);
            Assert.Equal(attemptResult.Id, returnValue.Id);
            Assert.Equal(attemptResult.Duration, returnValue.Duration);
            Assert.Equal(attemptResult.UserId, returnValue.UserId);
            Assert.Equal(attemptResult.GameId, returnValue.GameId);
            Assert.Equal(attemptResult.Score, returnValue.Score);
        }

        [Fact]
        public async Task GetAttemptResultController_ReturnsNotFound_WhenAttemptIsNotFound()
        {
            // Arrange
            var attemptId = 1;
            _mockAttemptService.Setup(service => service.GetAttemptResultAsync(attemptId)).ReturnsAsync((AttemptResultDTO)null);

            // Act
            var result = await _controller.GetAttemptResultController(attemptId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Not found attempt with Id {attemptId}", notFoundResult.Value);
        }
    }
}
