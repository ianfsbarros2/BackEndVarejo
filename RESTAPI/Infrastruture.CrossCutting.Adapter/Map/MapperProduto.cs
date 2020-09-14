using System.Collections.Generic;
using Application.DTO.DTO;
using Domain.Core.Interfaces.Repositories;
using Domain.Models;
using Domain.Models.Enum;
using Infrastruture.CrossCutting.Adapter.Interfaces;

namespace Infrastruture.CrossCutting.Adapter.Map
{
    public class MapperProduto: IMapperProduto
    {
        #region properties

        private readonly List<ProdutoDTO> _produtoDtos = new List<ProdutoDTO>();
        private readonly List<Produto> _produtoEntities = new List<Produto>();
        private readonly IRepositoryProduto _repositoryProduto;

        public MapperProduto(IRepositoryProduto repositoryProduto)
        {
            _repositoryProduto = repositoryProduto;
        }

        #endregion
        
        #region methods
        
        public Produto MapperToEntity(ProdutoDTO produtoDto)
        {
            var produto = new Produto
            {
              Id = produtoDto.CodigoProduto,
              Fabricacao = (FabricacaoEnum)produtoDto.Fabricacao,
              Nome = produtoDto.Nome,
              Tamanho = produtoDto.Tamanho,
              Valor = produtoDto.Valor,
              IdPedido = produtoDto.CodigoProduto == null 
                  ? null
                  : _repositoryProduto.GetById(produtoDto.CodigoProduto.Value).IdPedido
            };
            return produto;
        }
        public ProdutoDTO MapperToDTO(Produto produto)
        {
            var produtoDto = new ProdutoDTO
            {
                CodigoProduto = produto.Id,
                Fabricacao = (int)produto.Fabricacao,
                Nome = produto.Nome,
                Tamanho = produto.Tamanho,
                Valor = produto.Valor
            };
            return produtoDto;
        }
        public IEnumerable<ProdutoDTO> MapperToDTOList(IEnumerable<Produto> produtos)
        {
            foreach (var produto in produtos)
            {
                _produtoDtos.Add(MapperToDTO(produto));
            }
            return _produtoDtos;
        }
        public IEnumerable<Produto> MapperToEntityList(IEnumerable<ProdutoDTO> produtos)
        {
            foreach (var produto in produtos)
            {
                _produtoEntities.Add(MapperToEntity(produto));
            }
            return _produtoEntities;
        }
        #endregion
    }
}