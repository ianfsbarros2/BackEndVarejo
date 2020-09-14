using System.Collections.Generic;
using Application.DTO.DTO;

namespace Application.Interfaces
{
    public interface IApplicationServiceProduto
    {
        IEnumerable<ProdutoDTO> GetAll();
        
        ProdutoDTO GetById(int id);
        
        void Add(IEnumerable<ProdutoDTO> produtoDto);

        void Update(ProdutoDTO produtoDto, int id);

        void Remove(int id);

        void Dispose();
    }
}