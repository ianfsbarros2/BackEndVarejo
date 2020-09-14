using Domain.Core.Interfaces.Repositories;
using Domain.Core.Interfaces.Services;
using Domain.Models;
using MimeKit;

namespace Domain.Services.Services
{
    public class ServicePedido : ServiceBase<Pedido>, IServicePedido
    {
        private readonly IRepositoryPedido _repositoryPedido;
        
        public ServicePedido(IRepositoryPedido repositoryPedido)
            : base(repositoryPedido)
        {
            _repositoryPedido = repositoryPedido;
        }

        public void SendMail(MimeMessage mail)
        {
            _repositoryPedido.SendMail(mail);
        }
    }
}