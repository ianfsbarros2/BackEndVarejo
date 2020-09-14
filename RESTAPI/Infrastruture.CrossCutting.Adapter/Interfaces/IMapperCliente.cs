using System.Collections.Generic;
using Application.DTO.DTO;
using Domain.Models;

namespace Infrastruture.CrossCutting.Adapter.Interfaces
{
    public interface IMapperCliente
    {
        Cliente MapperToEntity(ClienteDTO clienteDto);
        IEnumerable<ClienteDTO> MapperToDTOList(IEnumerable<Cliente> clientes);
        IEnumerable<Cliente> MapperToEntityList(IEnumerable<ClienteDTO> clienteDtos);
        ClienteDTO MapperToDTO(Cliente cliente);
    }
}