using System.Transactions;
using TechTestBackendCSharp.Models;

namespace TechTestBackendCSharp.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ObterTodosProdutosAsync();
        Task<Produto> ObterProdutoPorIdAsync(int id);
        Task<Produto> CadastrarProdutoAsync(Produto novoProduto);
        Task<Produto> AtualizarProdutoPorIdAsync(int id, Produto produtoAtualizado);
        Task<Produto> DeletarProdutoPorIdAsync(int id);
        Task<Produto> DeletarProdutosPorIdEStatusAsync(int id);
    }
}
