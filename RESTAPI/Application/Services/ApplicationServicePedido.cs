using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.DTO.DTO;
using Application.Interfaces;
using Domain.Core.Interfaces.Services;
using Infrastruture.CrossCutting.Adapter.Interfaces;
using MimeKit;

namespace Application.Services
{
    public class ApplicationServicePedido: IApplicationServicePedido
    {
        private readonly IServicePedido _servicePedido;
        private readonly IMapperPedido _mapperPedido;
        private readonly StringBuilder _mailHtml;
        private readonly MimeMessage _mailMessage;
        
        public ApplicationServicePedido(IServicePedido servicePedido
            , IMapperPedido mapperPedido)
                                              
        {
            _servicePedido = servicePedido;
            _mapperPedido = mapperPedido;
            _mailHtml = new StringBuilder();
            _mailMessage = new MimeMessage(); 
        }
        
        public IEnumerable<PedidoDTO> GetAll()
        {
            var entitiesPedido = _servicePedido.GetAll();
            Dispose();
            return _mapperPedido.MapperToDTOList(entitiesPedido);
        }

        public PedidoDTO GetById(int id)
        {
            var entityPedido = _servicePedido.GetById(id);
            return entityPedido == null ? null : _mapperPedido.MapperToDTO(entityPedido);
        }

        public void Add(IEnumerable<PedidoDTO> pedidoDtos)
        {
            var entitiesPedido = _mapperPedido.MapperToEntityList(pedidoDtos);
            _servicePedido.Add(entitiesPedido);
            Dispose();
        }

        public void Update(PedidoDTO pedidoDto, int id)
        {
            pedidoDto.CodigoPedido = id;
            var entityPedido = _mapperPedido.MapperToEntity(pedidoDto);
            _servicePedido.Update(entityPedido);
            Dispose();
        }

        public void Remove(int id)
        {
            var pedidoDto = new PedidoDTO {CodigoPedido = id};
            var entityPedido = _mapperPedido.MapperToEntity(pedidoDto);
            _servicePedido.Remove(entityPedido);
            Dispose();
        }

        public void SendMail()
        {
            _servicePedido.SendMail(_mailMessage);
        }
        public void ArrangeMailParameters(PedidoDTO pedidoDto)
        {
            var cliente = pedidoDto.Cliente;
            var from = new MailboxAddress("Admin", "smtpclient000@gmail.com");
            var to = new MailboxAddress(cliente.Nome, cliente.Email);
            _mailMessage.From.Add(from);
            _mailMessage.To.Add(to);
        }
        
        public void BuildMailBody(PedidoDTO pedidoDto)
        {
            decimal valorTotalPedido = 0;
            var bodyBuilder = new BodyBuilder();
            
            _mailMessage.Subject = "Informações do pedido " + pedidoDto.CodigoPedido;

            _mailHtml.AppendLine("<h2><strong><u>Dados do pedido</u></strong></h2>");
            _mailHtml.AppendFormat("<h4>Código do pedido: </h4>{0}", pedidoDto.CodigoPedido);
            _mailHtml.AppendFormat("<h4>Data do pedido: </h4>{0}", pedidoDto.DataPedido);
            _mailHtml.AppendFormat("<h4>Observação: </h4>{0}", pedidoDto.Observacao);
            _mailHtml.AppendFormat("<h4>Forma de pagamento: </h4>{0}", pedidoDto.FormaPagamento.ToString());
            
            _mailHtml.AppendLine("<h2><strong><u>Dados do cliente</u></strong></h2>");
            _mailHtml.AppendFormat("<h4>Código do cliente: </h4>{0}", pedidoDto.Cliente.CodigoCliente);
            _mailHtml.AppendFormat("<h4>Nome: </h4>{0}", pedidoDto.Cliente.Nome);
            _mailHtml.AppendFormat("<h4>CPF: </h4>{0}", pedidoDto.Cliente.Cpf);
            _mailHtml.AppendFormat("<h4>Sexo: </h4>{0}", pedidoDto.Cliente.Sexo.ToString());
            _mailHtml.AppendFormat("<h4>Email: </h4>{0}", pedidoDto.Cliente.Email);
            
            _mailHtml.AppendLine("<h2><strong><u>Produtos do pedido</u></strong></h2>");
            
            if (pedidoDto.Produtos != null)
            {
                foreach (var produto in pedidoDto.Produtos)
                {
                    _mailHtml.AppendLine("<h4>----------------------------------</h4>");
                    _mailHtml.AppendFormat("<h4>Código do produto: </h4>{0}", produto.CodigoProduto);
                    _mailHtml.AppendFormat("<h4>Nome do produto: </h4>{0}", produto.Nome);
                    _mailHtml.AppendFormat("<h4>Fabricação: </h4>{0}", produto.Fabricacao.ToString());
                    _mailHtml.AppendFormat("<h4>Tamanho: </h4>{0}", produto.Tamanho);
                    _mailHtml.AppendFormat("<h4>Valor:</h4>{0}", produto.Valor);
                    _mailHtml.AppendLine("<h4>----------------------------------</h4>");
                }

                var grouped = pedidoDto.Produtos
                    .GroupBy(produto => produto.CodigoProduto)
                    .ToDictionary(group => @group.Key,
                        group => @group.Sum(item => item.Valor));

                _mailHtml.AppendLine("<h2><strong><u>Valor total por produto:</u></strong></h2>");

                foreach (var (key, value) in grouped)
                {
                    _mailHtml.AppendLine("<h4>----------------------------------</h4>");
                    _mailHtml.AppendFormat("<h4>Código do produto: </h4>{0}", key);
                    _mailHtml.AppendFormat("<h4>Valor total: </h4>{0}", value);
                    _mailHtml.AppendLine("<h4>----------------------------------</h4>");
                    valorTotalPedido += value;
                }
            }
            _mailHtml.AppendFormat("<h2><strong><u>Valor total do pedido: </u></strong></h2>{0}", valorTotalPedido);
            
            bodyBuilder.HtmlBody = _mailHtml.ToString();
            bodyBuilder.TextBody = "";
            _mailMessage.Body = bodyBuilder.ToMessageBody();
        }
        public void Dispose()
        {
            _servicePedido.Dispose();
        }
    }
}