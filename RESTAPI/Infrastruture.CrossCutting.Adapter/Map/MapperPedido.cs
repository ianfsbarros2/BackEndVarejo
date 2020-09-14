using System.Collections.Generic;
using System.Linq;
using Application.DTO.DTO;
using Domain.Core.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enum;
using Infrastruture.CrossCutting.Adapter.Interfaces;

namespace Infrastruture.CrossCutting.Adapter.Map
{
    public class MapperPedido: IMapperPedido
    {
        #region properties

        private readonly List<PedidoDTO> _pedidoDtos = new List<PedidoDTO>();
        private readonly List<Pedido> _pedidoEntities = new List<Pedido>();
        private readonly IMapperCliente _mapperCliente;
        private readonly IMapperProduto _mapperProduto;
        private readonly IRepositoryCliente _repositoryCliente;
        private readonly IRepositoryProduto _repositoryProduto;
        
        #endregion

        public MapperPedido(IMapperCliente mapperCliente, IMapperProduto mapperProduto,
            IRepositoryCliente repositoryCliente, IRepositoryProduto repositoryProduto)
        {
            _mapperCliente = mapperCliente;
            _mapperProduto = mapperProduto;
            _repositoryCliente = repositoryCliente;
            _repositoryProduto = repositoryProduto;
        }
        #region methods
        
        public Pedido MapperToEntity(PedidoDTO pedidoDto)
        {
            var pedido = new Pedido
            {
                Id = pedidoDto.CodigoPedido,
                Observacao = pedidoDto.Observacao,
                DataPedido = pedidoDto.DataPedido,
                FormaPagamento = (FormaPagamentoEnum) pedidoDto.FormaPagamento,
                IdCliente = pedidoDto.Cliente.CodigoCliente
            };
            
            return pedido;
        }
        public PedidoDTO MapperToDTO(Pedido pedido)
        {
            var pedidoDTO = new PedidoDTO
            {
                Cliente = 
                    pedido.IdCliente == null
                    ? new ClienteDTO{} 
                    : _mapperCliente.MapperToDTO(_repositoryCliente.GetById(pedido.IdCliente.Value)),
                
                Observacao = pedido.Observacao,
                
                Produtos = _mapperProduto
                    .MapperToDTOList(_repositoryProduto.GetAll()
                        .Where(p => p.IdPedido == pedido.Id)),
                
                CodigoPedido = pedido.Id,
                DataPedido = pedido.DataPedido,
                FormaPagamento = (int)pedido.FormaPagamento
            };
            
            return pedidoDTO;
        }
        public IEnumerable<PedidoDTO> MapperToDTOList(IEnumerable<Pedido> pedidos)
        {
            foreach (var pedido in pedidos)
            {
                _pedidoDtos.Add(MapperToDTO(pedido));
            }

            return _pedidoDtos;
        }

        public IEnumerable<Pedido> MapperToEntityList(IEnumerable<PedidoDTO> pedidos)
        {
            foreach (var pedido in pedidos)
            {
                _pedidoEntities.Add(MapperToEntity(pedido));
            }

            return _pedidoEntities;
        }
        
        #endregion
        
    }
}