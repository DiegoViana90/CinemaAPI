using CinemaApi.Business.Interface;
using CinemaApi.DTOs.Request;
using CinemaApi.DTOs.Response;
using CinemaApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;

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
    }
}
