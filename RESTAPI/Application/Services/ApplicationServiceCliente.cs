using System.Collections.Generic;
using System.Linq;
using Application.DTO.DTO;
using Application.Interfaces;
using Domain.Core.Interfaces.Services;
using Infrastruture.CrossCutting.Adapter.Interfaces;

namespace Application.Services
{
    public class ApplicationServiceCliente : IApplicationServiceCliente
    {
        private readonly IServiceCliente _serviceCliente;
        private readonly IMapperCliente _mapperCliente;
        
        public ApplicationServiceCliente(IServiceCliente serviceCliente
            , IMapperCliente mapperCliente)
                                              
        {
            _serviceCliente = serviceCliente;
            _mapperCliente = mapperCliente;
        }
        
        public IEnumerable<ClienteDTO> GetAll()
        {
            var entitiesCliente = _serviceCliente.GetAll();
            Dispose();
            return _mapperCliente.MapperToDTOList(entitiesCliente);
        }

        public ClienteDTO GetById(int id)
        {
            var entityCliente = _serviceCliente.GetById(id);
            Dispose();
            return entityCliente == null ? null : _mapperCliente.MapperToDTO(entityCliente);
        }

        public void Add(IEnumerable<ClienteDTO> clienteDtos)
        {
            var entitiesCliente = clienteDtos.Select(cliente => _mapperCliente.MapperToEntity(cliente)).ToList();
            _serviceCliente.Add(entitiesCliente);
            Dispose();
        }

        public void Update(ClienteDTO clienteDto, int id)
        {
            clienteDto.CodigoCliente = id;
            var entityCliente = _mapperCliente.MapperToEntity(clienteDto);
            _serviceCliente.Update(entityCliente);
            Dispose();
        }

        public void Remove(int id)
        {
            var obj = new ClienteDTO {CodigoCliente = id};
            var entityCliente = _mapperCliente.MapperToEntity(obj);
            _serviceCliente.Remove(entityCliente);
            Dispose();
        }

        public void Dispose()
        {
            _serviceCliente.Dispose();
        }
    }
}