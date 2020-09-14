using System.Collections.Generic;
using Application.DTO.DTO;
using Application.Interfaces;
using Domain.Core.Interfaces.Services;
using Infrastruture.CrossCutting.Adapter.Interfaces;

namespace Application.Services
{
    public class ApplicationServiceProduto : IApplicationServiceProduto
    {
        private readonly IServiceProduto _serviceProduto;
        private readonly IMapperProduto _mapperProduto;
        
        public ApplicationServiceProduto(IServiceProduto serviceProduto
            , IMapperProduto mapperProduto)
                                              
        {
            _serviceProduto = serviceProduto;
            _mapperProduto = mapperProduto;
        }
        
        public IEnumerable<ProdutoDTO> GetAll()
        {
            var entitiesProduto = _serviceProduto.GetAll();
            Dispose();
            return _mapperProduto.MapperToDTOList(entitiesProduto);
        }

        public ProdutoDTO GetById(int id)
        {
            var entityProduto = _serviceProduto.GetById(id);
            Dispose();
            return entityProduto == null ? null : _mapperProduto.MapperToDTO(entityProduto);
        }

        public void Add(IEnumerable<ProdutoDTO> obj)
        {
            var entitiesProduto = _mapperProduto.MapperToEntityList(obj);
            _serviceProduto.Add(entitiesProduto);
            Dispose();
        }

        public void Update(ProdutoDTO produtoDto, int id)
        {
            produtoDto.CodigoProduto = id;
            var entityProduto = _mapperProduto.MapperToEntity(produtoDto);
            _serviceProduto.Update(entityProduto);
            Dispose();
        }

        public void Remove(int id)
        {
            var produtoDto = new ProdutoDTO {CodigoProduto = id};
            var entityProduto = _mapperProduto.MapperToEntity(produtoDto);
            _serviceProduto.Remove(entityProduto);
            Dispose();
        }

        public void Dispose()
        {
            _serviceProduto.Dispose();
        }
    }
}