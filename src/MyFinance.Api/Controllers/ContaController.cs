using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Application.DTOs;
using MyFinance.Application.Interfaces.Services;

namespace MyFinance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContaController : ControllerBase
    {
        private readonly IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            _contaService = contaService;
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateConta([FromBody] CriarContaRequest request, CancellationToken cancellationToken)
        {
            var result = await _contaService.CreateContaAsync(request, cancellationToken);
            if (result.IsSuccess)
                return Created(string.Empty, new { ContaId = result.Value });
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllContas([FromBody]Guid UserId, CancellationToken cancellationToken)
        {
            var result = await _contaService.GetAllContasAsync(UserId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetContaById([FromBody] Guid ContaId, CancellationToken cancellationToken)
        {
            var result = await _contaService.GetContaByIdAsync(ContaId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetContasByUser([FromBody] Guid UserId, CancellationToken cancellationToken)
        {
            var result = await _contaService.GetContasByUserAsync(UserId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPut("Activate")]
        public async Task<IActionResult> ActivateConta([FromBody] Guid ContaId, CancellationToken cancellationToken)
        {
            var result = await _contaService.ActivateContaAsync(ContaId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateConta([FromBody] ContaDto request, CancellationToken cancellationToken)
        {
            var result = await _contaService.UpdateContaAsync(request, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [Authorize(Roles = "Admin,User")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteConta([FromBody] Guid ContaId, CancellationToken cancellationToken)
        {
            var result = await _contaService.DeleteContaAsync(ContaId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
    }
}