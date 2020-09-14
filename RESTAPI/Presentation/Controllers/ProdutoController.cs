using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTO.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    [Route("produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IApplicationServiceProduto _applicationServiceProduto;
        
        public ProdutoController(IApplicationServiceProduto applicationServiceProduto)
        {
            _applicationServiceProduto = applicationServiceProduto;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                var produtos = _applicationServiceProduto.GetAll().ToList();
                
                if (!produtos.Any())
                {
                    return StatusCode(404);
                }

                return Ok(produtos);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpGet("{codigoProduto}")]
        public ActionResult<string> GetById(int codigoProduto)
        {
            if(codigoProduto < 0) {return StatusCode(400);}
            
            try
            {
                var produto = _applicationServiceProduto.GetById(codigoProduto);
                if (produto == null) return StatusCode(404);
                return Ok(produto);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(404);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost]
        public ActionResult Post([FromBody] IEnumerable<ProdutoDTO> produtoDtos)
        {
            try
            {
                var listaValida = produtoDtos.ToList().Any();
            }
            catch(Exception)
            {
                return StatusCode(400);
            }
            try
            {
                _applicationServiceProduto.Add(produtoDtos);
                return StatusCode(201);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPut]
        public ActionResult Put([FromBody] ProdutoDTO produtoDTO,[FromQuery] int codigoProduto)
        {
            if(produtoDTO == null || codigoProduto < 0){return StatusCode(400);}
            
            try
            {
                _applicationServiceProduto.Update(produtoDTO, codigoProduto);
                return StatusCode(204);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(404);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpDelete]
        public ActionResult Delete([FromQuery] int codigoProduto)
        {
            if(codigoProduto < 0){return StatusCode(400);}
            
            try
            {
                _applicationServiceProduto.Remove(codigoProduto);
                return StatusCode(202);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(404);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
    }
}