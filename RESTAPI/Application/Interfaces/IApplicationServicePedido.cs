using System.Collections.Generic;
using Application.DTO.DTO;

namespace Application.Interfaces
{
    public interface IApplicationServicePedido
    {
        IEnumerable<PedidoDTO> GetAll();
        PedidoDTO GetById(int id);
        void Add(IEnumerable<PedidoDTO> pedidoDto);
        void Update(PedidoDTO pedidoDto, int id);
        void Remove(int id);
        void SendMail();
        void ArrangeMailParameters(PedidoDTO pedidoDto);
        void BuildMailBody(PedidoDTO pedidoDto);
        void Dispose();
    }
}