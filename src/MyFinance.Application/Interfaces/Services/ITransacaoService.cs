using MyFinance.Application.DTOs;

namespace MyFinance.Application.Interfaces.Services
{
    public interface ITransacaoService
    {
        Task CriarAsync(CriarTransacaoRequest request, CancellationToken cancellationToken = default);
    }
}