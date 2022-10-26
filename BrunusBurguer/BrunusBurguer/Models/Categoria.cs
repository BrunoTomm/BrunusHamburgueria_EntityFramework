using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrunusBurguer.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        [Required(ErrorMessage = "Informe o nome da Categoria")]
        [StringLength(100, ErrorMessage = "O Tamanho Máximo é 100 caracteres")]
        [Display(Name = "Nome")]
        public string CategoriaNome { get; set; }
        [Required(ErrorMessage = "Informe a descrição da Categoria")]
        [StringLength(200, ErrorMessage = "O Tamanho Máximo é 200 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        //Propriedade de navegacao
        public List<Lanche> Lanches { get; set; } //Relacionamento 1 para MUITOS, uma categoria, pode ter, MUITOS Lanches

    }
}
