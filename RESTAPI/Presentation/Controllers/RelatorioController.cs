using System;
using System.Collections.Generic;
using System.Linq;
using Application.DTO.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("relatorios")]
    [ApiController]
    public class RelatorioController : Controller
    {
        private readonly IApplicationServiceRelatorio _applicationServiceRelatorio;
        
        public RelatorioController(IApplicationServiceRelatorio applicationServiceRelatorio)
        {
            _applicationServiceRelatorio = applicationServiceRelatorio;
        }
        
        [HttpGet("impostos/ano/{ano}/mes/{mes}/cliente/{id}/")]
        public ActionResult<decimal> GetValorImpostoMensal(int ano, int mes, int id)
        {
            List<PedidoDTO> listaPedidos;
            if (ano <= 1900 || ano >= DateTime.Now.Year || mes <= 0 || mes > 12 || id < 0)
            {
                return StatusCode(400);
            }
            try
            {
                var pedidosClienteMes =
                    _applicationServiceRelatorio.GetPedidosClienteMes(ano, mes, id);
                
                if (pedidosClienteMes != null)
                {
                    listaPedidos = pedidosClienteMes.ToList();
                    
                    if (!listaPedidos.Any())
                    {
                        return StatusCode(404);
                    }
                }
                else
                {
                    return StatusCode(404);
                }
                
                if (listaPedidos.SelectMany(ped => ped.Produtos)
                    .Any(p => p.Fabricacao == 0))
                {
                    return StatusCode(404);
                }
                return Ok(_applicationServiceRelatorio.CalculateValorImpostoMensal(listaPedidos));
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }
    }
}