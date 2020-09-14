using Data;
using Domain.Core.Interfaces.Repositories;
using Domain.Models;

namespace Infrastruture.Repository.Repositories
{
    public class RepositoryProduto : RepositoryBase<Produto>, IRepositoryProduto
    {
        private readonly SqlContext _context;
        
        public RepositoryProduto(SqlContext context)
            : base(context)
        {
            _context = context;
        }
    }
}