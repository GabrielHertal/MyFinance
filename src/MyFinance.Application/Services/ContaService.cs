using MyFinance.Application.DTOs;
using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Application.Interfaces.Services;
using MyFinance.Domain.Entities;
using MyFinance.Shared.Results;

namespace MyFinance.Application.Services
{
    public class ContaService : ITransacaoService
    {
        private readonly IContaRepository _contarepository;
        private readonly IUnitofWork _unitofwork;
        public ContaService(IContaRepository contarepository, IUnitofWork unitofwork)
        {
            _contarepository = contarepository;
            _unitofwork = unitofwork;
        }
        public async Task<Result<Guid>> CreateContaAsync(CriarContaRequest request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);
            var conta = new Conta(request.UsuarioId
                                 , request.nome
                                 , request.saldoInicial
                                 , request.tipo);
            try
            {
                await _contarepository.CreateContaAsync(conta);
                await _unitofwork.SaveChangesAsync(cancellationToken);
                return conta.Id;
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(new Error("Erro ao criar conta: ", ex.Message));
                throw;
            }
        }
        public async Task<Result<IReadOnlyList<ContaDto>>> GetAllContasAsync(Guid usuarioId, CancellationToken cancellationToken = default)
        {
            try
            {
                var contas = await _contarepository.GetAllContasAsync();
                var contasDto = contas.Select(c =>
                    new ContaDto(c.Id, c.Nome, c.Saldo, c.UsuarioId, c.Tipo, c.Ativo)
                ).ToList();
                return Result<IReadOnlyList<ContaDto>>.Success(contasDto);
            }
            catch (Exception ex)
            {
                return Result<IReadOnlyList<ContaDto>>.Failure(new Error("Erro ao obter contas: ", ex.Message));
                throw;
            }
        }
    }
}