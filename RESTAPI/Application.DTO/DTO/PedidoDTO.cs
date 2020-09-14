using System;
using System.Collections.Generic;
namespace Application.DTO.DTO
{
    public class PedidoDTO
    {
        public int? CodigoPedido{ get; set; }
        public DateTime DataPedido { get; set; }
        public string Observacao { get; set; }
        public int FormaPagamento { get; set; }
        public ClienteDTO Cliente { get; set; }
        public IEnumerable<ProdutoDTO> Produtos { get; set; }
    }
}