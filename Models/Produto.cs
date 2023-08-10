namespace TechTestBackendCSharp.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public decimal ValorTotal => Preco * QuantidadeEmEstoque;
    }
}
