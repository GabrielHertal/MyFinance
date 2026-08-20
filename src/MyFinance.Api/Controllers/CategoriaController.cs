using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Application.DTOs;
using MyFinance.Application.Interfaces.Services;
using MyFinance.Shared.Results;

namespace MyFinance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaservice;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaservice = categoriaService;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategoria([FromBody] CriarCategoriaRequest request, CancellationToken cancellationToken)
        {
            var result = await _categoriaservice.CreateCategoriaAsync(request, cancellationToken);
            if (result.IsSuccess)
                return Created(string.Empty, new { CategoriaId = result.Value });
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCategorias([FromBody] Guid UserId, CancellationToken cancellationToken)
        {
            var result = await _categoriaservice.GetAllCategoriasAsync(UserId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetCategoriaById([FromBody] Guid CategoriaId, CancellationToken cancellationToken)
        {
            var result = await _categoriaservice.GetCategoriaByIdAsync(CategoriaId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetCategoriasByUser([FromBody] Guid UserId, CancellationToken cancellationToken)
        {
            var result = await _categoriaservice.GetCategoriasByUserAsync(UserId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCategoria([FromBody] CategoriaDto request, CancellationToken cancellationToken)
        {
            var result = await _categoriaservice.UpdateCategoriaAsync(request, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCategoria([FromBody] Guid CategoriaId, CancellationToken cancellationToken)
        {
            var result = await _categoriaservice.DeleteCategoriaAsync(CategoriaId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
        [HttpPut("Activate")]
        public async Task<IActionResult> ActivateCategoria([FromBody] Guid CategoriaId, CancellationToken cancellationToken)
        {
            var result = await _categoriaservice.ActivateCategoriaAsync(CategoriaId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(new { Erro = result.Errors.Select(x => new { x.Code, x.Message }) });
        }
    }
}