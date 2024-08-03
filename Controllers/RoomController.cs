using CinemaApi.Business.Interface;
using CinemaApi.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost("InsertNewRoom")]
        [SwaggerOperation(Summary = "Adiciona uma nova sala",
         Description = "Adiciona uma nova sala ao sistema")]
        [SwaggerResponse(200, "Sala criada com sucesso", typeof(string))]
        [SwaggerResponse(400, "Solicitação inválida")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<string>> InsertRoom(InsertRoomRequest insertRoomRequest)
        {
            try
            {
                await _roomService.InsertNewRoom(insertRoomRequest);
                return Ok("Sala criada com sucesso.");
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
