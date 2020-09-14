using System.Collections.Generic;
using Application.DTO.DTO;
using Domain.Models;
using Domain.Models.Enum;
using Infrastruture.CrossCutting.Adapter.Interfaces;

namespace Infrastruture.CrossCutting.Adapter.Map
{
    public class MapperCliente: IMapperCliente
    {
        #region properties

        private readonly List<ClienteDTO> _clienteDtos = new List<ClienteDTO>();
        private readonly List<Cliente> _clienteEntities = new List<Cliente>();

        #endregion
        
        #region methods
        
        public Cliente MapperToEntity(ClienteDTO clienteDTO)
        {
            if (clienteDTO == null)
            {
                return null;
            }
            var cliente = new Cliente
            {
                Id = clienteDTO.CodigoCliente,
                Cpf = clienteDTO.Cpf,
                Email = clienteDTO.Email,
                Nome = clienteDTO.Nome,
                Sexo = (SexoEnum)clienteDTO.Sexo
            };
            
            return cliente;
        }

        public IEnumerable<ClienteDTO> MapperToDTOList(IEnumerable<Cliente> clientes)
        {
            foreach (var cliente in clientes)
            {
                _clienteDtos.Add(MapperToDTO(cliente));
            }

            return _clienteDtos;
        }
        public IEnumerable<Cliente> MapperToEntityList(IEnumerable<ClienteDTO> clientes)
        {
            foreach (var cliente in clientes)
            {
                _clienteEntities.Add(MapperToEntity(cliente));
            }

            return _clienteEntities;
        }

        public ClienteDTO MapperToDTO(Cliente cliente)
        {
            if (cliente == null)
            {
                return null;
            }
            var clienteDTO = new ClienteDTO
            {
                CodigoCliente = cliente.Id,
                Cpf = cliente.Cpf,
                Email = cliente.Email,
                Nome = cliente.Nome,
                Sexo = (int)cliente.Sexo
            };

            return clienteDTO;
        }
        
        #endregion
    }
}