using BrunusBurguer.Context;
using BrunusBurguer.Models;
using BrunusBurguer.Repositories.Interfaces;

namespace BrunusBurguer.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias; //Retorna as categorias do banco,  em uma coleção de objetos categoria
    }
}
