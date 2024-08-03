using CinemaApi.Business.Interface;
using CinemaApi.Controllers;
using CinemaApi.DTOs.Request;
using CinemaApi.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CinemaApi.Tests
{
    public class MovieControllerTests
    {
        private readonly Mock<IMovieService> _mockMovieService;
        private readonly MovieController _controller;

        public MovieControllerTests()
        {
            _mockMovieService = new Mock<IMovieService>();
            _controller = new MovieController(_mockMovieService.Object);
        }

        [Fact]
        public async Task InsertMovie_ReturnsOk_WhenMovieIsInsertedSuccessfully()
        {
            var insertMovieRequest = new InsertMovieRequest
            {
                Name = "Inception",
                Director = "Christopher Nolan",
                Duration = new TimeSpan(2, 28, 0),
                RoomNumber = 1
            };

            _mockMovieService.Setup(service => service.InsertNewMovie(It.IsAny<InsertMovieRequest>()))
                             .Returns(Task.CompletedTask);

            var result = await _controller.InsertMovie(insertMovieRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Filme Inserido com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task InsertMovie_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            var insertMovieRequest = new InsertMovieRequest
            {
                Name = "Inception",
                Director = "Christopher Nolan",
                Duration = new TimeSpan(2, 28, 0),
                RoomNumber = 1
            };

            _mockMovieService.Setup(service => service.InsertNewMovie(It.IsAny<InsertMovieRequest>()))
                             .ThrowsAsync(new ArgumentException("Um filme com este nome já existe."));

            var result = await _controller.InsertMovie(insertMovieRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Um filme com este nome já existe.", badRequestResult.Value);
        }

        [Fact]
        public async Task InsertMovie_ReturnsOk_WhenRoomNumberIsMissing()
        {
            var insertMovieRequest = new InsertMovieRequest
            {
                Name = "Onze Homens e um Segredo",
                Director = "Steven Soderbergh",
                Duration = new TimeSpan(1, 16, 0),
                RoomNumber = null
            };

            _mockMovieService.Setup(service => service.InsertNewMovie(It.IsAny<InsertMovieRequest>()))
                             .Returns(Task.CompletedTask);

            var result = await _controller.InsertMovie(insertMovieRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Filme Inserido com sucesso.", okResult.Value);
        }

        [Fact]
        public async Task InsertMovie_ReturnsBadRequest_WhenDurationIsZero()
        {
            var insertMovieRequest = new InsertMovieRequest
            {
                Name = "Onze Homens e um Segredo",
                Director = "Steven Soderbergh",
                Duration = TimeSpan.Zero,
                RoomNumber = 1
            };

            _mockMovieService.Setup(service => service.InsertNewMovie(It.IsAny<InsertMovieRequest>()))
                             .ThrowsAsync(new ArgumentException("A duração do filme deve ser maior que zero."));

            var result = await _controller.InsertMovie(insertMovieRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("A duração do filme deve ser maior que zero.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetMovieByName_ReturnsOk_WhenMovieIsFound()
        {
            var movieName = "Inception";
            var movieResponse = new MovieResponse
            {
                Name = movieName,
                Director = "Christopher Nolan",
                Duration = new TimeSpan(2, 28, 0),
                RoomNumber = 1
            };

            _mockMovieService.Setup(service => service.GetMovieByName(movieName))
                             .ReturnsAsync(movieResponse);

            var result = await _controller.GetMovieByName(movieName);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<MovieResponse>(okResult.Value);
            Assert.Equal(movieName, returnValue.Name);
        }

        [Fact]
        public async Task GetMovieByName_ReturnsNotFound_WhenMovieIsNotFound()
        {
            var movieName = "Nonexistent Movie";

            _mockMovieService.Setup(service => service.GetMovieByName(movieName))
                             .ThrowsAsync(new KeyNotFoundException("Filme não encontrado."));

            var result = await _controller.GetMovieByName(movieName);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("Filme não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetMovieByName_ReturnsBadRequest_WhenNameIsEmpty()
        {
            var movieName = "";

            _mockMovieService.Setup(service => service.GetMovieByName(movieName))
                             .ThrowsAsync(new ArgumentException("O nome do filme não pode ser vazio."));

            var result = await _controller.GetMovieByName(movieName);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("O nome do filme não pode ser vazio.", badRequestResult.Value);
        }
    }
}
