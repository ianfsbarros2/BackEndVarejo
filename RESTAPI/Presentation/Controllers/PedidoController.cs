using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTO.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    [Route("pedidos")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IApplicationServicePedido _applicationServicePedido;
        
        public PedidoController(IApplicationServicePedido applicationServicePedido)
        {
            _applicationServicePedido = applicationServicePedido;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                var pedidos = _applicationServicePedido.GetAll().ToList();
                
                if (!pedidos.Any())
                {
                    return StatusCode(404);
                }

                return Ok(pedidos);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpGet("{codigoPedido}")]
        public ActionResult<string> GetById(int codigoPedido)
        {
            if(codigoPedido < 0) {return StatusCode(400);}
            
            try
            {
                var pedido = _applicationServicePedido.GetById(codigoPedido);
                if (pedido == null) return StatusCode(404);
                return Ok(pedido);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost]
        public ActionResult Post([FromBody] IEnumerable<PedidoDTO> pedidoDtos)
        {
            try
            {
                var listaValida = pedidoDtos.ToList().Any();
            }
            catch(Exception)
            {
                return StatusCode(400);
            }
            try
            {
                _applicationServicePedido.Add(pedidoDtos);
                return StatusCode(201);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost("{codigoPedido}/sendmail")]
        public ActionResult SendMail(int codigoPedido)
        {
            if (codigoPedido < 0)
                return StatusCode(404);
            try
            {
                var pedido = _applicationServicePedido.GetById(codigoPedido);
                if (pedido == null)
                {
                    return StatusCode(404);
                }
                _applicationServicePedido.ArrangeMailParameters(pedido);
                _applicationServicePedido.BuildMailBody(pedido);
                _applicationServicePedido.SendMail();
                return StatusCode(200);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPut]
        public ActionResult Put([FromBody] PedidoDTO pedidoDTO,[FromQuery] int codigoPedido)
        {
            if(pedidoDTO == null || codigoPedido < 0){return StatusCode(400);}
            
            try
            {
                _applicationServicePedido.Update(pedidoDTO, codigoPedido);
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
        public ActionResult Delete([FromQuery] int codigoPedido)
        {
            if(codigoPedido < 0){return StatusCode(400);}
            
            try
            {
                _applicationServicePedido.Remove(codigoPedido);
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