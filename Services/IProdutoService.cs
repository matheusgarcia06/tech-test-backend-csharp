using System.Transactions;
using TechTestBackendCSharp.Models;

namespace TechTestBackendCSharp.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ObterTodosProdutosAsync();
        Task<Produto> ObterProdutoPorIdAsync(int id);
        Task<IEnumerable<Produto>> CadastrarProdutoAsync(ProdutoViewModel novoProduto);
        Task<Produto> AtualizarProdutoPorIdAsync(int id, ProdutoViewModel produtoAtualizado);
        Task<Produto> DeletarProdutoPorIdAsync(int id);
    }
}
