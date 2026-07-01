using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinance.Domain.Enums
{
    public enum TipoTransacao : int
    {
        Deposito = 1,
        Saque = 2,
        Investimento = 3
    }
}