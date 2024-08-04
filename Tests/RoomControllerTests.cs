using CinemaApi.Business.Interface;
using CinemaApi.Controllers;
using CinemaApi.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
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

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Sala criada com sucesso.", okResult.Value);
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

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("A sala especificada já existe.", badRequestResult.Value);
        }

        [Fact]
        public async Task InsertRoom_ReturnsBadRequest_WhenRoomNumberIsEmpty()
        {
            var insertRoomRequest = new InsertRoomRequest
            {
                RoomNumber = "",
                Description = "Sala de exibição principal"
            };

            _mockRoomService.Setup(service => service.InsertNewRoom(It.IsAny<InsertRoomRequest>()))
                             .ThrowsAsync(new ArgumentException("O número da sala não pode ser vazio."));

            var result = await _controller.InsertRoom(insertRoomRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("O número da sala não pode ser vazio.", badRequestResult.Value);
        }

        [Fact]
        public async Task InsertRoom_ReturnsBadRequest_WhenRoomNumberIsNotNumeric()
        {
            var insertRoomRequest = new InsertRoomRequest
            {
                RoomNumber = "A101",
                Description = "Sala de exibição principal"
            };

            _mockRoomService.Setup(service => service.InsertNewRoom(It.IsAny<InsertRoomRequest>()))
                             .ThrowsAsync(new ArgumentException("O número da sala deve conter apenas números."));

            var result = await _controller.InsertRoom(insertRoomRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("O número da sala deve conter apenas números.", badRequestResult.Value);
        }

        [Fact]
        public async Task InsertRoom_ReturnsBadRequest_WhenRoomNumberIsNotThreeDigits()
        {
            var insertRoomRequest = new InsertRoomRequest
            {
                RoomNumber = "1001",
                Description = "Sala de exibição principal"
            };

            _mockRoomService.Setup(service => service.InsertNewRoom(It.IsAny<InsertRoomRequest>()))
                             .ThrowsAsync(new ArgumentException("O número da sala deve conter exatamente 3 dígitos."));

            var result = await _controller.InsertRoom(insertRoomRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("O número da sala deve conter exatamente 3 dígitos.", badRequestResult.Value);
        }
        
    }
}
