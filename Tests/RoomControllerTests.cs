using CinemaApi.Business.Interface;
using CinemaApi.Controllers;
using CinemaApi.DTOs.Request;
using CinemaApi.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CinemaApi.Tests
{
    public class RoomControllerTests
    {
        private readonly Mock<IRoomService> _mockRoomService;
        private readonly RoomController _controller;

        public RoomControllerTests()
        {
            _mockRoomService = new Mock<IRoomService>();
            _controller = new RoomController(_mockRoomService.Object);
        }

        [Fact]
        public async Task InsertRoom_ReturnsOk_WhenRoomIsInsertedSuccessfully()
        {
            var insertRoomRequest = new InsertRoomRequest
            {
                RoomNumber = "101",
                Description = "Sala de exibição principal"
            };

            _mockRoomService.Setup(service => service.InsertNewRoom(It.IsAny<InsertRoomRequest>()))
                             .Returns(Task.CompletedTask);

            var result = await _controller.InsertRoom(insertRoomRequest);

            var okResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Sala criada com sucesso.", (okResult.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task InsertRoom_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            var insertRoomRequest = new InsertRoomRequest
            {
                RoomNumber = "101",
                Description = "Sala de exibição principal"
            };

            _mockRoomService.Setup(service => service.InsertNewRoom(It.IsAny<InsertRoomRequest>()))
                             .ThrowsAsync(new ArgumentException("A sala especificada já existe."));

            var result = await _controller.InsertRoom(insertRoomRequest);

            var badRequestResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("A sala especificada já existe.", (badRequestResult.Result as BadRequestObjectResult).Value);
        }

        [Fact]
        public async Task GetAllRooms_ReturnsOk_WithListOfRooms()
        {
            var rooms = new List<RoomResponse>
            {
                new RoomResponse
                {
                    RoomNumber = "101",
                    Description = "Sala de exibição principal"
                },
                new RoomResponse
                {
                    RoomNumber = "102",
                    Description = "Sala de exibição secundária"
                }
            };

            _mockRoomService.Setup(service => service.GetAllRooms())
                             .ReturnsAsync(rooms);

            var result = await _controller.GetAllRooms();

            var okResult = Assert.IsType<ActionResult<IEnumerable<RoomResponse>>>(result);
            var returnValue = Assert.IsType<List<RoomResponse>>((okResult.Result as OkObjectResult).Value);
            Assert.Equal(2, returnValue.Count);
            Assert.Equal("101", returnValue[0].RoomNumber);
            Assert.Equal("102", returnValue[1].RoomNumber);
        }

        [Fact]
        public async Task GetRoomByNumber_ReturnsOk_WhenRoomIsFound()
        {
            var roomNumber = "101";
            var roomResponse = new RoomResponse
            {
                RoomNumber = roomNumber,
                Description = "Sala de exibição principal"
            };

            _mockRoomService.Setup(service => service.GetRoomByNumber(roomNumber))
                             .ReturnsAsync(roomResponse);

            var result = await _controller.GetRoomByNumber(roomNumber);

            var okResult = Assert.IsType<ActionResult<RoomResponse>>(result);
            var returnValue = Assert.IsType<RoomResponse>((okResult.Result as OkObjectResult).Value);
            Assert.Equal(roomNumber, returnValue.RoomNumber);
        }

        [Fact]
        public async Task GetRoomByNumber_ReturnsNotFound_WhenRoomIsNotFound()
        {
            var roomNumber = "999";

            _mockRoomService.Setup(service => service.GetRoomByNumber(roomNumber))
                             .ThrowsAsync(new KeyNotFoundException("Sala não encontrada."));

            var result = await _controller.GetRoomByNumber(roomNumber);

            var notFoundResult = Assert.IsType<ActionResult<RoomResponse>>(result);
            Assert.Equal("Sala não encontrada.", (notFoundResult.Result as NotFoundObjectResult).Value);
        }

        [Fact]
        public async Task UpdateRoom_ReturnsOk_WhenRoomIsUpdatedSuccessfully()
        {
            var updateRoomRequest = new UpdateRoomRequest
            {
                RoomNumber = "101",
                Description = "Sala de exibição atualizada"
            };

            _mockRoomService.Setup(service => service.UpdateRoom(It.IsAny<UpdateRoomRequest>()))
                             .Returns(Task.CompletedTask);

            var result = await _controller.UpdateRoom(updateRoomRequest);

            var okResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Sala atualizada com sucesso.", (okResult.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task UpdateRoom_ReturnsNotFound_WhenRoomIsNotFound()
        {
            var updateRoomRequest = new UpdateRoomRequest
            {
                RoomNumber = "999",
                Description = "Sala não existente"
            };

            _mockRoomService.Setup(service => service.UpdateRoom(It.IsAny<UpdateRoomRequest>()))
                             .ThrowsAsync(new KeyNotFoundException("Sala não encontrada."));

            var result = await _controller.UpdateRoom(updateRoomRequest);

            var notFoundResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Sala não encontrada.", (notFoundResult.Result as NotFoundObjectResult).Value);
        }

        [Fact]
        public async Task DeleteRoom_ReturnsOk_WhenRoomIsDeletedSuccessfully()
        {
            var roomNumber = "101";

            _mockRoomService.Setup(service => service.DeleteRoom(roomNumber))
                             .Returns(Task.CompletedTask);

            var result = await _controller.DeleteRoom(roomNumber);

            var okResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Sala excluída com sucesso.", (okResult.Result as OkObjectResult).Value);
        }

        [Fact]
        public async Task DeleteRoom_ReturnsNotFound_WhenRoomIsNotFound()
        {
            var roomNumber = "999";

            _mockRoomService.Setup(service => service.DeleteRoom(roomNumber))
                             .ThrowsAsync(new KeyNotFoundException("Sala não encontrada."));

            var result = await _controller.DeleteRoom(roomNumber);

            var notFoundResult = Assert.IsType<ActionResult<string>>(result);
            Assert.Equal("Sala não encontrada.", (notFoundResult.Result as NotFoundObjectResult).Value);
        }
    }
}
