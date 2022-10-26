using BrunusBurguer.Context;
using BrunusBurguer.Models;
using BrunusBurguer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrunusBurguer.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => 
                                    _context.Lanches.Include(x => x.Categoria); // => exoression bodied member, simplifica a implementaçao

        public IEnumerable<Lanche> LanchesPreferidos => 
                                    _context.Lanches.Where(_ => _.IsLanchePreferido)
                                                    .Include(x => x.Categoria);

        public Lanche GetLancheById(int lancheId) => 
                                    _context.Lanches.FirstOrDefault(_ => _.LancheId == lancheId);

    }
}
