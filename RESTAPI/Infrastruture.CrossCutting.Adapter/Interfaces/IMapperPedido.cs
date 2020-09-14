using System.Collections.Generic;
using Application.DTO.DTO;
using Domain.Models;

namespace Infrastruture.CrossCutting.Adapter.Interfaces
{
    public interface IMapperPedido
    {
        Pedido MapperToEntity(PedidoDTO pedidoDto);
        IEnumerable<PedidoDTO> MapperToDTOList(IEnumerable<Pedido> pedidos);
        IEnumerable<Pedido> MapperToEntityList(IEnumerable<PedidoDTO> pedidoDtos);
        PedidoDTO MapperToDTO(Pedido pedido);
    }
}