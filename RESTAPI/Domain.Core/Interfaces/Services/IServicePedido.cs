using Domain.Models;
using MimeKit;

namespace Domain.Core.Interfaces.Services
{
    public interface IServicePedido: IServiceBase<Pedido>
    {
        void SendMail(MimeMessage mail);
    }
}