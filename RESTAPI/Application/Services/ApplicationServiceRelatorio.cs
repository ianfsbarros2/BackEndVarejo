using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTO.DTO;
using Application.Interfaces;
using Domain.Core.Interfaces.Services;
using Infrastruture.CrossCutting.Adapter.Interfaces;

namespace Application.Services
{
    public class ApplicationServiceRelatorio: IApplicationServiceRelatorio
    {
        private readonly IServicePedido _servicePedido;
        private readonly IMapperPedido _mapperPedido;
        
        public ApplicationServiceRelatorio(IServicePedido servicePedido, IMapperPedido mapperPedido)
        {
            _servicePedido = servicePedido;
            _mapperPedido = mapperPedido;
        }
        public IEnumerable<PedidoDTO> GetPedidosClienteMes(int ano, int mes, int id)
        {
            if (ano == 0 || ano > 2999)
            {
                return new List<PedidoDTO>();
            }
            var pedidos = _mapperPedido.MapperToDTOList(_servicePedido.GetAll()).ToList();
            Dispose();
            if (!pedidos.Any())
            {
                return pedidos;
            }
            var pedidosCliente = pedidos
                .Where(p => p.Cliente.CodigoCliente == id);
            if (pedidosCliente.Equals(Enumerable.Empty<PedidoDTO>()))
                return null; 
            var pedidosClienteMes = pedidosCliente
                .Where(p => p.DataPedido.Year == ano && p.DataPedido.Month == mes);
            
            return pedidosClienteMes;
        }
        public decimal CalculateValorImpostoMensal(IEnumerable<PedidoDTO> listPedidos)
        {
            var pedidosClienteMes = listPedidos.ToList();
            decimal valorImposto = 0;
            
            if (!pedidosClienteMes.Any())
            {
                return 0;
            }
            foreach (var produto in pedidosClienteMes.SelectMany(pedido => pedido.Produtos))
            {
                if (produto == null || produto.Valor == 0)
                {
                    continue;
                }
                switch (produto.Fabricacao)
                {
                    case 0:
                        throw new Exception();
                    case 1:
                        if (produto.Valor > 100)
                            valorImposto += (produto.Valor / 10);
                        break;
                    case 2:
                        valorImposto += (produto.Valor * 0.15m);
                        break;
                }
            }
            return valorImposto;
        }
        public void Dispose()
        {
            _servicePedido.Dispose();
        }
    }
}