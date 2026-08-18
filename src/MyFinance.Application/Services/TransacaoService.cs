using MyFinance.Application.DTOs;
using MyFinance.Application.Interfaces.Repositories;
using MyFinance.Application.Interfaces.Services;
using MyFinance.Domain.Entities;
using MyFinance.Domain.Enums;
using MyFinance.Shared.Results;

namespace MyFinance.Application.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IContaRepository _contaRepository;
        private readonly IUnitofWork _unitofWork;

        public TransacaoService(ITransacaoRepository transacaoRepository, IContaRepository contaRepository, IUnitofWork unitofWork)
        {
            _transacaoRepository = transacaoRepository;
            _contaRepository = contaRepository;
            _unitofWork = unitofWork;
        }

        public async Task<Result<Guid>> CriarAsync(CriarTransacaoRequest request,CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);
            var conta = await _contaRepository.GetByIdAsync(request.ContaId);
            if (conta is null)
                return Error.NotFound("A conta informada não foi encontrada.");
            if (!conta.Ativo)
                return new Error("inactive_account", "Não é possível criar uma transação para uma conta inativa.");
            if (conta.UsuarioId != request.UsuarioId)
                return Error.Forbidden("A conta informada não pertence ao usuário da transação.");
            var transacao = new Transacao(request.ContaId,
                                          request.CategoriaId,
                                          request.UsuarioId,
                                          request.Descricao,
                                          request.Valor,
                                          request.Tipo,
                                          request.DataTransacao,
                                          request.DataPagamento,
                                          request.Status);

            switch (request.Tipo)
            {
                case TipoTransacao.Deposito:
                    conta.Deposito(request.Valor);
                    break;
                case TipoTransacao.Saque:
                    conta.Saque(request.Valor);
                    break;
                case TipoTransacao.Investimento:
                    conta.Investir(request.Valor);
                    break;
                default:
                    return Error.Validation("O tipo de transação informado não possui suporte.");
            }
            await _transacaoRepository.CreateAsync(transacao);
            await _contaRepository.UpdateAsync(conta);
            await _unitofWork.SaveChangesAsync(cancellationToken);
            return transacao.Id;
        }

        public async Task<Result<IReadOnlyList<TransacaoDto>>> ListarAsync(
            Guid usuarioId,
            CancellationToken cancellationToken = default)
        {
            if (usuarioId == Guid.Empty)
                return Error.Validation("O usuário deve ser informado.");

            var transacoes = await _transacaoRepository.ListarAsync(usuarioId, cancellationToken);
            var items = transacoes.Select(x => new TransacaoDto(
                x.Id, x.ContaId, x.CategoriaId, x.Descricao, x.Valor, x.Tipo,
                x.DataTransacao, x.DataPagamento, x.Status)).ToList();
            return items;
        }
    }
}