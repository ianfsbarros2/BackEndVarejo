#nullable enable
using System;
using Domain.Models.Enum;

namespace Domain.Models
{
    public class Pedido : Base
    {
        public DateTime DataPedido { get; set; }
        public string? Observacao { get; set; }
        public FormaPagamentoEnum FormaPagamento { get; set; }
        public int? IdCliente { get; set; }
    }
}