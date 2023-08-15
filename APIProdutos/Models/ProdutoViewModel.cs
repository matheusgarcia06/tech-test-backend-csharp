using System.ComponentModel.DataAnnotations;

namespace TechTestBackendCSharp.Models
{
    public class ProdutoViewModel
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço do produto deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade em estoque é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser maior ou igual a zero.")]
        public int QuantidadeEmEstoque { get; set; }
    }
}
