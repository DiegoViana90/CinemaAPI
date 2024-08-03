using CinemaApi.Business.Interface;
using CinemaApi.DTOs.Request;
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
        [SwaggerOperation(Summary = "Adiciona um novo filme", Description = "Adiciona um novo filme ao sistema")]
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
    }
}
