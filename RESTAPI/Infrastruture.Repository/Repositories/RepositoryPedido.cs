using Data;
using Domain.Core.Interfaces.Repositories;
using Domain.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Infrastruture.Repository.Repositories
{
    public class RepositoryPedido : RepositoryBase<Pedido>, IRepositoryPedido
    {

        private readonly SqlContext _context;
        
        public RepositoryPedido(SqlContext context)
            : base(context)
        {
            _context = context;
        }

        public void SendMail(MimeMessage mail)
        {
            var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls );
            client.Authenticate("smtpclient000@gmail.com", "74'M\"!XF&B#/n$e7");
            client.Send(mail);
        }
    }
}