using Microsoft.AspNetCore.Mvc;
using TechChallenge.Api.Controllers.Shared;
using TechChallenge.Domain.Entities.Responses;
using TechChallenge.Domain.Exceptions;
using TechChallenge.Domain.Interfaces.Services;

namespace TechChallenge.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    public class DDDController : ApiControllerBase
    {
        private readonly IDDDService _idDDService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDDService"></param>
        public DDDController(IDDDService idDDService)
        {
            _idDDService = idDDService;
        }


        /// <summary>
        /// Comando responsável em obter todos DDDs 
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("obtertodos")]
        public async Task<ActionResult> ObterTodosDDDs()
        {
            try
            {
                var ddds = await _idDDService.Get();

                return Ok(new BaseResponse
                {
                    Message = "Busca por DDDs realizado com sucesso!",
                    Success = true,
                    Errors = null,
                    Data = ddds
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseException.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseException.ApplicationErrorMessage());
            }
        }
    }
}
