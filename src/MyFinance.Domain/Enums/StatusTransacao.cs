using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinance.Domain.Enums
{
    public enum StatusTransacao : int
    {
        Pendente = 0,
        Pago = 1,
        Cancelado = 2,
        Estornado = 3
    }
}
