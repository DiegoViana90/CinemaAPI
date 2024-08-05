using CinemaApi.Business.Interface;
using CinemaApi.DTOs.Request;
using CinemaApi.DTOs.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CinemaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost("InsertNewMovie")]
        [SwaggerOperation(Summary = "Adiciona um novo filme",
        Description = "Adiciona um novo filme ao sistema")]
        [SwaggerResponse(200, "Filme Criado com sucesso", typeof(string))]
        [SwaggerResponse(201, "Filme Criado com sucesso", typeof(string))]
        [SwaggerResponse(400, "Solicitação inválida")]
        [SwaggerResponse(404, "Não encontrado")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<string>> InsertMovie(InsertMovieRequest insertMovieRequest)
        {
            try
            {
                await _movieService.InsertNewMovie(insertMovieRequest);
                return Ok("Filme Inserido com sucesso.");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMovieByName")]
        [SwaggerOperation(Summary = "Obtém um filme pelo nome",
         Description = "Obtém os detalhes de um filme pelo nome")]
        [SwaggerResponse(200, "Filme encontrado", typeof(MovieResponse))]
        [SwaggerResponse(404, "Filme não encontrado")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<MovieResponse>> GetMovieByName(string name)
        {
            try
            {
                var movieResponse = await _movieService.GetMovieByName(name);
                return Ok(movieResponse);
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

        [HttpPut("UpdateMovieRoom")]
        [SwaggerOperation(Summary = "Atualiza o número da sala de um filme",
        Description = "Atualiza o número da sala de um filme existente no sistema.")]
        [SwaggerResponse(200, "Número da sala atualizado com sucesso", typeof(MovieResponse))]
        [SwaggerResponse(404, "Filme não encontrado")]
        [SwaggerResponse(400, "Solicitação inválida")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<MovieResponse>> UpdateMovie([FromBody] UpdateMovieRoomRequest updateMovieRoomRequest)
        {
            try
            {
                var movieResponse = await _movieService.UpdateMovie(updateMovieRoomRequest);
                return Ok(movieResponse);
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

        [HttpGet("GetAllMovies")]
        [SwaggerOperation(Summary = "Obtém todos os filmes", 
        Description = "Obtém a lista de todos os filmes")]
        [SwaggerResponse(200, "Lista de filmes obtida com sucesso", typeof(IEnumerable<MovieResponse>))]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAllMovies()
        {
            try
            {
                var movies = await _movieService.GetAllMovies();
                return Ok(movies);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveMovieFromRoom")]
        [SwaggerOperation(Summary = "Remove um filme de uma sala",
        Description = "Remove a associação de um filme com uma sala específica.")]
        [SwaggerResponse(200, "Filme removido da sala com sucesso", typeof(string))]
        [SwaggerResponse(404, "Filme ou sala não encontrados")]
        [SwaggerResponse(400, "Solicitação inválida")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<string>> RemoveMovieFromRoom(string movieName, string roomNumber)
        {
            try
            {
                await _movieService.RemoveMovieFromRoom(movieName, roomNumber);
                return Ok("Filme removido da sala com sucesso.");
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

        [HttpDelete("UnscheduleMovie")]
        [SwaggerOperation(Summary = "Remove um filme de cartaz",
        Description = "Remove um filme da programação de todas as salas.")]
        [SwaggerResponse(200, "Filme removido da programação com sucesso", typeof(string))]
        [SwaggerResponse(404, "Filme não encontrado")]
        [SwaggerResponse(400, "Solicitação inválida")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<string>> UnscheduleMovie(string movieName)
        {
            try
            {
                await _movieService.UnscheduleMovie(movieName);
                return Ok("Filme removido da programação com sucesso.");
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
