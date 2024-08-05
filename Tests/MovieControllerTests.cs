using CinemaApi.Business.Interface;
using CinemaApi.Controllers;
using CinemaApi.DTOs.Request;
using CinemaApi.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
                RoomNumber = "101"
            };

            _mockMovieService.Setup(service => service.InsertNewMovie(It.IsAny<InsertMovieRequest>()))
                             .Returns(Task.CompletedTask);

            var result = await _controller.InsertMovie(insertMovieRequest);

            var okResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Filme Inserido com sucesso.", (okResult.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task InsertMovie_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            var insertMovieRequest = new InsertMovieRequest
            {
                Name = "Inception",
                Director = "Christopher Nolan",
                Duration = new TimeSpan(2, 28, 0),
                RoomNumber = "101"
            };

            _mockMovieService.Setup(service => service.InsertNewMovie(It.IsAny<InsertMovieRequest>()))
                             .ThrowsAsync(new ArgumentException("Um filme com este nome já existe na sala especificada."));

            var result = await _controller.InsertMovie(insertMovieRequest);

            var badRequestResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Um filme com este nome já existe na sala especificada.", (badRequestResult.Result as BadRequestObjectResult).Value);
        }

        [Fact]
        public async Task InsertMovie_ReturnsOk_WhenRoomNumberIsMissing()
        {
            var insertMovieRequest = new InsertMovieRequest
            {
                Name = "Onze Homens e um segredo",
                Director = "Steven Soderbergh",
                Duration = new TimeSpan(1, 56, 0),
                RoomNumber = null
            };

            _mockMovieService.Setup(service => service.InsertNewMovie(It.IsAny<InsertMovieRequest>()))
                             .Returns(Task.CompletedTask);

            var result = await _controller.InsertMovie(insertMovieRequest);

            var okResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Filme Inserido com sucesso.", (okResult.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task InsertMovie_ReturnsBadRequest_WhenDurationIsZero()
        {
            var insertMovieRequest = new InsertMovieRequest
            {
                Name = "Onze Homens e um segredo",
                Director = "Steven Soderbergh",
                Duration = TimeSpan.Zero,
                RoomNumber = "101"
            };

            _mockMovieService.Setup(service => service.InsertNewMovie(It.IsAny<InsertMovieRequest>()))
                             .ThrowsAsync(new ArgumentException("A duração do filme deve ser maior que zero."));

            var result = await _controller.InsertMovie(insertMovieRequest);

            var badRequestResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("A duração do filme deve ser maior que zero.", (badRequestResult.Result as BadRequestObjectResult).Value);
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
                RoomNumbers = new List<string> { "101" },
                Descriptions = new List<string> { "Main Theater" }
            };

            _mockMovieService.Setup(service => service.GetMovieByName(movieName))
                             .ReturnsAsync(movieResponse);

            var result = await _controller.GetMovieByName(movieName);

            var okResult = Assert.IsType<ActionResult<MovieResponse>>(result);
            var returnValue = Assert.IsType<MovieResponse>((okResult.Result as OkObjectResult).Value);
            Assert.Equal(movieName, returnValue.Name);
        }

        [Fact]
        public async Task GetMovieByName_ReturnsNotFound_WhenMovieIsNotFound()
        {
            var movieName = "Nonexistent Movie";

            _mockMovieService.Setup(service => service.GetMovieByName(movieName))
                             .ThrowsAsync(new KeyNotFoundException("Filme não encontrado."));

            var result = await _controller.GetMovieByName(movieName);

            var notFoundResult = Assert.IsType<ActionResult<MovieResponse>>(result);
            Assert.Equal("Filme não encontrado.", (notFoundResult.Result as NotFoundObjectResult).Value);
        }

        [Fact]
        public async Task GetMovieByName_ReturnsBadRequest_WhenNameIsEmpty()
        {
            var movieName = "";

            _mockMovieService.Setup(service => service.GetMovieByName(movieName))
                             .ThrowsAsync(new ArgumentException("O nome do filme não pode ser vazio."));

            var result = await _controller.GetMovieByName(movieName);

            var badRequestResult = Assert.IsType<ActionResult<MovieResponse>>(result);
            Assert.Equal("O nome do filme não pode ser vazio.", (badRequestResult.Result as BadRequestObjectResult).Value);
        }

        [Fact]
        public async Task GetAllMovies_ReturnsOk_WithListOfMovies()
        {
            var movies = new List<MovieResponse>
            {
                new MovieResponse
                {
                    Name = "Inception",
                    Director = "Christopher Nolan",
                    Duration = new TimeSpan(2, 28, 0),
                    RoomNumbers = new List<string> { "101" },
                    Descriptions = new List<string> { "Main Theater" }
                },
                new MovieResponse
                {
                    Name = "The Matrix",
                    Director = "Wachowskis",
                    Duration = new TimeSpan(2, 16, 0),
                    RoomNumbers = new List<string> { "102" },
                    Descriptions = new List<string> { "Secondary Theater" }
                }
            };

            _mockMovieService.Setup(service => service.GetAllMovies())
                             .ReturnsAsync(movies);

            var result = await _controller.GetAllMovies();

            var okResult = Assert.IsType<ActionResult<IEnumerable<MovieResponse>>>(result);
            var returnValue = Assert.IsType<List<MovieResponse>>((okResult.Result as OkObjectResult).Value);
            Assert.Equal(2, returnValue.Count);
            Assert.Equal("Inception", returnValue[0].Name);
            Assert.Equal("The Matrix", returnValue[1].Name);
        }

        [Fact]
        public async Task UpdateMovie_ReturnsOk_WhenMovieRoomIsUpdatedSuccessfully()
        {
            var updateMovieRoomRequest = new UpdateMovieRoomRequest
            {
                Name = "Inception",
                RoomNumber = "102"
            };

            var movieResponse = new MovieResponse
            {
                Name = "Inception",
                Director = "Christopher Nolan",
                Duration = new TimeSpan(2, 28, 0),
                RoomNumbers = new List<string> { "102" },
                Descriptions = new List<string>()
            };

            _mockMovieService.Setup(service => service.UpdateMovie(updateMovieRoomRequest))
                             .ReturnsAsync(movieResponse);

            var result = await _controller.UpdateMovie(updateMovieRoomRequest);

            var okResult = Assert.IsType<ActionResult<MovieResponse>>(result);
            var returnValue = Assert.IsType<MovieResponse>((okResult.Result as OkObjectResult).Value);
            Assert.Equal("Inception", returnValue.Name);
            Assert.Contains("102", returnValue.RoomNumbers);
        }

        [Fact]
        public async Task UpdateMovie_ReturnsNotFound_WhenMovieIsNotFound()
        {
            var updateMovieRoomRequest = new UpdateMovieRoomRequest
            {
                Name = "Nonexistent Movie",
                RoomNumber = "102"
            };

            _mockMovieService.Setup(service => service.UpdateMovie(updateMovieRoomRequest))
                             .ThrowsAsync(new KeyNotFoundException("Filme não encontrado."));

            var result = await _controller.UpdateMovie(updateMovieRoomRequest);

            var notFoundResult = Assert.IsType<ActionResult<MovieResponse>>(result);
            Assert.Equal("Filme não encontrado.", (notFoundResult.Result as NotFoundObjectResult).Value);
        }

        [Fact]
        public async Task UpdateMovie_ReturnsBadRequest_WhenMovieAlreadyExistsInRoom()
        {
            var updateMovieRoomRequest = new UpdateMovieRoomRequest
            {
                Name = "Inception",
                RoomNumber = "101"
            };

            _mockMovieService.Setup(service => service.UpdateMovie(updateMovieRoomRequest))
                             .ThrowsAsync(new ArgumentException("Um filme com este nome já existe na sala especificada."));

            var result = await _controller.UpdateMovie(updateMovieRoomRequest);

            var badRequestResult = Assert.IsType<ActionResult<MovieResponse>>(result);
            Assert.Equal("Um filme com este nome já existe na sala especificada.", (badRequestResult.Result as BadRequestObjectResult).Value);
        }

        [Fact]
        public async Task RemoveMovieFromRoom_ReturnsOk_WhenMovieIsRemovedSuccessfully()
        {
            var movieName = "Inception";
            var roomNumber = "101";

            _mockMovieService.Setup(service => service.RemoveMovieFromRoom(movieName, roomNumber))
                             .Returns(Task.CompletedTask);

            var result = await _controller.RemoveMovieFromRoom(movieName, roomNumber);

            var okResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Filme removido da sala com sucesso.", (okResult.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task RemoveMovieFromRoom_ReturnsNotFound_WhenMovieOrRoomNotFound()
        {
            var movieName = "Nonexistent Movie";
            var roomNumber = "101";

            _mockMovieService.Setup(service => service.RemoveMovieFromRoom(movieName, roomNumber))
                             .ThrowsAsync(new KeyNotFoundException("O filme ou a sala não foram encontrados."));

            var result = await _controller.RemoveMovieFromRoom(movieName, roomNumber);

            var notFoundResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("O filme ou a sala não foram encontrados.", (notFoundResult.Result as NotFoundObjectResult).Value);
        }

        [Fact]
        public async Task RemoveMovieFromRoom_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            var movieName = "Inception";
            var roomNumber = "101";

            _mockMovieService.Setup(service => service.RemoveMovieFromRoom(movieName, roomNumber))
                             .ThrowsAsync(new ArgumentException("Erro ao remover o filme da sala."));

            var result = await _controller.RemoveMovieFromRoom(movieName, roomNumber);

            var badRequestResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Erro ao remover o filme da sala.", (badRequestResult.Result as BadRequestObjectResult).Value);
        }

        [Fact]
        public async Task UnscheduleMovie_ReturnsOk_WhenMovieIsUnscheduledSuccessfully()
        {
            var movieName = "Inception";

            _mockMovieService.Setup(service => service.UnscheduleMovie(movieName))
                             .Returns(Task.CompletedTask);

            var result = await _controller.UnscheduleMovie(movieName);

            var okResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Filme removido da programação com sucesso.", (okResult.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task UnscheduleMovie_ReturnsNotFound_WhenMovieIsNotFound()
        {
            var movieName = "Nonexistent Movie";

            _mockMovieService.Setup(service => service.UnscheduleMovie(movieName))
                             .ThrowsAsync(new KeyNotFoundException("Filme não encontrado."));

            var result = await _controller.UnscheduleMovie(movieName);

            var notFoundResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Filme não encontrado.", (notFoundResult.Result as NotFoundObjectResult).Value);
        }

        [Fact]
        public async Task UnscheduleMovie_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            var movieName = "Inception";

            _mockMovieService.Setup(service => service.UnscheduleMovie(movieName))
                             .ThrowsAsync(new ArgumentException("Erro ao remover o filme da programação."));

            var result = await _controller.UnscheduleMovie(movieName);

            var badRequestResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Erro ao remover o filme da programação.", (badRequestResult.Result as BadRequestObjectResult).Value);
        }
    }
}
