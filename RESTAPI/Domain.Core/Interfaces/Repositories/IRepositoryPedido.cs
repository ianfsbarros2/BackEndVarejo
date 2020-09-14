using Domain.Models;
using MimeKit;

namespace Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryPedido : IRepositoryBase<Pedido>
    {
        void SendMail(MimeMessage mail);
    }
}