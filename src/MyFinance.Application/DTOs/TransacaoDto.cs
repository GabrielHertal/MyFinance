using MyFinance.Domain.Enums;

namespace MyFinance.Application.DTOs;

public sealed record TransacaoDto(Guid Id,Guid ContaId,Guid CategoriaId,string Descricao,decimal Valor,TipoTransacao Tipo,DateTime DataTransacao,DateTime? DataPagamento,StatusTransacao Status);
