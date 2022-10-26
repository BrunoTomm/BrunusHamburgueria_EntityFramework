using BrunusBurguer.Models;

namespace BrunusBurguer.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; } //Retorna um objeto de lanches
        IEnumerable<Lanche> LanchesPreferidos { get; } //Retorna um objeto de lanches preferidos
        Lanche GetLancheById(int lancheId); //Metodo que retorna um unico lanche pelo seu id


    }
}
