using BrunusBurguer.Models;

namespace BrunusBurguer.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; } //Somente leitura, coleção de objetos categoria
    }
}
