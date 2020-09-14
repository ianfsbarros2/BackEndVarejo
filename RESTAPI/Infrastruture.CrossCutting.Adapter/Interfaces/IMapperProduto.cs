using System.Collections.Generic;
using Application.DTO.DTO;
using Domain.Models;

namespace Infrastruture.CrossCutting.Adapter.Interfaces
{
    public interface IMapperProduto
    {
        Produto MapperToEntity(ProdutoDTO produtoDto);

        public IEnumerable<Produto> MapperToEntityList(IEnumerable<ProdutoDTO> produtos);
        
        public IEnumerable<ProdutoDTO> MapperToDTOList(IEnumerable<Produto> produtoDtos);
        
        ProdutoDTO MapperToDTO(Produto produtos);
    }
}