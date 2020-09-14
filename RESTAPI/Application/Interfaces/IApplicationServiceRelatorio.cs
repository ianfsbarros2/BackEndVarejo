using System.Collections.Generic;
using Application.DTO.DTO;

namespace Application.Interfaces
{
    public interface IApplicationServiceRelatorio
    {
        IEnumerable<PedidoDTO> GetPedidosClienteMes(int ano, int mes, int id);
        decimal CalculateValorImpostoMensal(IEnumerable<PedidoDTO> pedidosClienteMes);
        void Dispose();
    }
}