namespace MyFinance.Application.Services
{
    using MyFinance.Application.DTOs;
    using MyFinance.Application.Interfaces.Repositories;
    using MyFinance.Application.Interfaces.Services;
    using MyFinance.Domain.Entities;
    using MyFinance.Domain.Enums;

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

        public async Task CriarAsync(CriarTransacaoRequest request,CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);
            var conta = await _contaRepository.GetByIdAsync(request.ContaId)
                ?? throw new KeyNotFoundException("A conta informada não foi encontrada.");
            if (!conta.Ativo)
                throw new InvalidOperationException("Não é possível criar uma transação para uma conta inativa.");
            if (conta.UsuarioId != request.UsuarioId)
                throw new InvalidOperationException("A conta informada não pertence ao usuário da transação.");
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
                    throw new NotSupportedException("O tipo de transação informado não possui suporte.");
            }
            await _transacaoRepository.CreateAsync(transacao);
            await _contaRepository.UpdateAsync(conta);
            await _unitofWork.SaveChangesAsync(cancellationToken);
        }
    }
}