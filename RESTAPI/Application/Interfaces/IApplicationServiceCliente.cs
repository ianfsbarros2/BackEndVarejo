using System.Collections.Generic;
using Application.DTO.DTO;

namespace Application.Interfaces
{
    public interface IApplicationServiceCliente
    {
        IEnumerable<ClienteDTO> GetAll();
        
        ClienteDTO GetById(int id);
        
        void Add(IEnumerable<ClienteDTO> clienteDTO);

        void Update(ClienteDTO clienteDTO, int id);

        void Remove(int id);

        void Dispose();
    }
}