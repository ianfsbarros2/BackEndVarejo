using Data;
using Domain.Core.Interfaces.Repositories;
using Domain.Models;

namespace Infrastruture.Repository.Repositories
{
    public class RepositoryCliente : RepositoryBase<Cliente>, IRepositoryCliente
    {
        private readonly SqlContext _context;
        
        public RepositoryCliente(SqlContext context)
            : base(context)
        {
            _context = context;
        }
    }
}