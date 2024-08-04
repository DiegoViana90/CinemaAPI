using CinemaApi.Business.Interface;
using CinemaApi.DTOs.Request;
using CinemaApi.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet("GetAllRooms")]
        [SwaggerOperation(Summary = "Obtém todas as salas",
         Description = "Obtém a lista de todas as salas")]
        [SwaggerResponse(200, "Lista de salas obtida com sucesso", typeof(IEnumerable<RoomResponse>))]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<IEnumerable<RoomResponse>>> GetAllRooms()
        {
            try
            {
                var rooms = await _roomService.GetAllRooms();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetRoomByNumber")]
        [SwaggerOperation(Summary = "Obtém uma sala pelo número",
         Description = "Obtém os detalhes de uma sala pelo número")]
        [SwaggerResponse(200, "Sala encontrada", typeof(RoomResponse))]
        [SwaggerResponse(404, "Sala não encontrada")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<RoomResponse>> GetRoomByNumber(string roomNumber)
        {
            try
            {
                var room = await _roomService.GetRoomByNumber(roomNumber);
                return Ok(room);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateRoom")]
        [SwaggerOperation(Summary = "Atualiza uma sala",
         Description = "Atualiza os detalhes de uma sala existente")]
        [SwaggerResponse(200, "Sala atualizada com sucesso", typeof(string))]
        [SwaggerResponse(404, "Sala não encontrada")]
        [SwaggerResponse(400, "Solicitação inválida")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<string>> UpdateRoom(UpdateRoomRequest updateRoomRequest)
        {
            try
            {
                await _roomService.UpdateRoom(updateRoomRequest);
                return Ok("Sala atualizada com sucesso.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRoom")]
        [SwaggerOperation(Summary = "Exclui uma sala",
         Description = "Exclui uma sala pelo número")]
        [SwaggerResponse(200, "Sala excluída com sucesso", typeof(string))]
        [SwaggerResponse(404, "Sala não encontrada")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<string>> DeleteRoom(string roomNumber)
        {
            try
            {
                await _roomService.DeleteRoom(roomNumber);
                return Ok("Sala excluída com sucesso.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
