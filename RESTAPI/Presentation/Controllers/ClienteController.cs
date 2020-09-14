using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTO.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.Controllers
{
    [Route("clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IApplicationServiceCliente _applicationServiceCliente;
        
        public ClienteController(IApplicationServiceCliente applicationServiceCliente)
        {
            _applicationServiceCliente = applicationServiceCliente;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                var clientes = _applicationServiceCliente.GetAll().ToList();
                if (!clientes.Any())
                {
                    return StatusCode(404);
                }
                return Ok(clientes);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpGet("{codigoCliente}")]
        public ActionResult<string> GetById(int codigoCliente)
        {
            if(codigoCliente < 0) {return StatusCode(400);}
            
            try
            {
                var cliente = _applicationServiceCliente.GetById(codigoCliente);
                if (cliente == null) return StatusCode(404);
                return Ok(cliente);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPost]
        public ActionResult Post([FromBody] IEnumerable<ClienteDTO> clienteDtos)
        {
            try
            {
                var listaValida = clienteDtos.ToList().Any();
            }
            catch(Exception)
            {
                return StatusCode(400);
            }
            try
            {
                _applicationServiceCliente.Add(clienteDtos);
                return StatusCode(201);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
        
        [HttpPut]
        public ActionResult Put([FromBody] ClienteDTO clienteDTO,[FromQuery] int codigoCliente)
        {
            if(clienteDTO == null || codigoCliente < 0){return StatusCode(400);}

            try
            {
                _applicationServiceCliente.Update(clienteDTO, codigoCliente);
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
        public ActionResult Delete([FromQuery] int codigoCliente)
        {
            if(codigoCliente < 0){return StatusCode(400);}

            try
            {
                _applicationServiceCliente.Remove(codigoCliente);
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