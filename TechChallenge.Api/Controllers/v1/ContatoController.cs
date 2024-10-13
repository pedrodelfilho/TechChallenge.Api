using AutoMapper;
using TechChallenge.Api.Controllers.Shared;
using TechChallenge.Api.Resources;
using TechChallenge.Domain.Entities.Models;
using TechChallenge.Domain.Entities.Requests;
using TechChallenge.Domain.Entities.Responses;
using TechChallenge.Domain.Exceptions;
using TechChallenge.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace TechChallenge.Api.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    public class ContatoController : ApiControllerBase
    {
        private readonly IContatoService _contatoService;
        private readonly IMapper _mapper;
        private readonly ILogger<ContatoController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contatoService"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>

        public ContatoController(IContatoService contatoService, IMapper mapper, ILogger<ContatoController> logger)
        {
            _contatoService = contatoService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Controle responsável por cadastrar novo contato
        /// </summary>
        /// <param name="contatoRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("cadastrar")]
        public async Task<ActionResult> CadastrarContato([FromBody] RegistrarContatoRequest contatoRequest)
        {
            try
            {
                _logger.LogInformation(ApiConstants.CADASTRO_CONTATO);
                var contato = _mapper.Map<Contato>(contatoRequest);

                var contatoCreated = await _contatoService.Create(contato, contatoRequest.NrDDD);

                return Ok(new BaseResponse
                {
                    Message = ApiConstants.CONTATO_CADASTRADO,
                    Success = true,
                    Errors = [],
                    Data = contatoCreated
                });
            }
            catch (DomainException ex)
            {
                _logger.LogError(ApiConstants.ERRO_CADASTRO_CONTATO, ex.Message);
                return BadRequest(ResponseException.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiConstants.ERRO_CADASTRO_CONTATO, ex.Message);
                return StatusCode(500, ResponseException.ApplicationErrorMessage());
            }


        }

        /// <summary>
        /// Comando responsável por remover contato
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("remover/{id}")]
        public async Task<ActionResult> RemoverContato(long id)
        {
            try
            {
                _logger.LogInformation(ApiConstants.REMOVER_CONTATO);
                await _contatoService.Remove(id);

                return Ok(new BaseResponse
                {
                    Message = ApiConstants.CONTATO_REMOVIDO,
                    Success = true,
                    Errors = [],
                    Data = id
                });
            }
            catch (DomainException ex)
            {
                _logger.LogError(ApiConstants.ERRO_REMOVER_CONTATO, ex.Message);
                return BadRequest(ResponseException.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiConstants.ERRO_REMOVER_CONTATO, ex.Message);
                return StatusCode(500, ResponseException.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Comando responsável em atualizar contato
        /// </summary>
        /// <param name="contatoRequest"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("atualizar")]
        public async Task<ActionResult> AtualizarContato([FromBody] AtualizarContatoRequest contatoRequest)
        {
            try
            {
                _logger.LogInformation(ApiConstants.ATUALIZAR_CONTATO);
                var contato = _mapper.Map<Contato>(contatoRequest);
                var contatoUpdate = await _contatoService.Update(contato, contatoRequest.NrDDD);

                return Ok(new BaseResponse
                {
                    Message = ApiConstants.CONTATO_ATUALIZADO,
                    Success = true,
                    Errors = [],
                    Data = contatoUpdate
                });
            }
            catch (DomainException ex)
            {
                _logger.LogError(ApiConstants.ERRO_ATUALIZAR_CONTATO, ex.Message);
                return BadRequest(ResponseException.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiConstants.ERRO_ATUALIZAR_CONTATO, ex.Message);
                return StatusCode(500, ResponseException.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Comando responsável em obter todos contatos
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("obtertodos")]
        public async Task<ActionResult> ObterTodosContatos()
        {
            try
            {
                _logger.LogInformation(ApiConstants.CONSULTAR_CONTATO);
                var allContatos = await _contatoService.Get();

                return Ok(new BaseResponse
                {
                    Message = ApiConstants.CONTATO_CONSULTADO,
                    Success = true,
                    Errors = [],
                    Data = allContatos
                });
            }
            catch (DomainException ex)
            {
                _logger.LogError(ApiConstants.ERRO_CONSULTAR_CONTATO, ex.Message);
                return BadRequest(ResponseException.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiConstants.ERRO_CONSULTAR_CONTATO, ex.Message);
                return StatusCode(500, ResponseException.ApplicationErrorMessage());
            }
        }

        /// <summary>
        /// Comando responsável por obter contato pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("obterporid/{id}")]
        public async Task<ActionResult> ObterContatoPorId(long id)
        {
            try
            {
                _logger.LogInformation(ApiConstants.CONSULTAR_CONTATO);
                var contato = await _contatoService.Get(id);

                return Ok(new BaseResponse
                {
                    Message = ApiConstants.CONTATO_CONSULTADO,
                    Success = true,
                    Errors = [],
                    Data = contato
                });
            }
            catch (DomainException ex)
            {
                _logger.LogError(ApiConstants.ERRO_CONSULTAR_CONTATO, ex.Message);
                return BadRequest(ResponseException.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ApiConstants.ERRO_CONSULTAR_CONTATO, ex.Message);
                return StatusCode(500, ResponseException.ApplicationErrorMessage());
            }
        }
    }
}
