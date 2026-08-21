using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Application.DTOs;
using MyFinance.Application.Interfaces.Services;

namespace MyFinance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class CartaoController : ControllerBase
    {
        private readonly ICartaoService _cartaoservice;
        public CartaoController(ICartaoService cartaoService)
        {
            _cartaoservice = cartaoService;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCartaoAsync([FromBody] CriarCartaoRequest request, CancellationToken cancellationToken = default)
        {
            var result = await _cartaoservice.CreateCartaoAsync(request, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
        [HttpGet("GetAll/{usuarioId}")]
        public async Task<IActionResult> GetAllCartoesAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            var result = await _cartaoservice.GetAllCartoesAsync(cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
        [HttpGet("GetById/{cartaoId}")]
        public async Task<IActionResult> GetCartaoByIdAsync(Guid cartaoId, CancellationToken cancellationToken = default)
        {
            var result = await _cartaoservice.GetCartaoByIdAsync(cartaoId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
        [HttpGet("GetByUsuarioId/{usuarioId}")]
        public async Task<IActionResult> GetCartaoByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            var result = await _cartaoservice.GetCartoesByUserAsync(usuarioId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
        [HttpPut("Update/{cartaoId}")]
        public async Task<IActionResult> UpdateCartaoAsync(Guid cartaoId, [FromBody] CartaoDto request, CancellationToken cancellationToken = default)
        {
            var result = await _cartaoservice.UpdateCartaoAsync(request, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Error);
        }
        [HttpDelete("Delete/{cartaoId}")]
        public async Task<IActionResult> DeleteCartaoAsync(Guid cartaoId, CancellationToken cancellationToken = default)
        {
            var result = await _cartaoservice.DeleteCartaoAsync(cartaoId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Error);
        }
        [HttpPut("Activate/{cartaoId}")]
        public async Task<IActionResult> AtivarCartaoAsync(Guid cartaoId, CancellationToken cancellationToken = default)
        {
            var result = await _cartaoservice.ActivateCartaoAsync(cartaoId, cancellationToken);
            if (result.IsSuccess)
                return Ok(result.IsSuccess);
            return BadRequest(result.Error);
        }
    }
}