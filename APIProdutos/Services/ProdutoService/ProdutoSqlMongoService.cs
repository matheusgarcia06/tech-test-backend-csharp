using TechTestBackendCSharp.Models;

namespace TechTestBackendCSharp.Services.ProdutoService
{
    public class ProdutoSqlMongoService : IProdutoService
    {
        private readonly IProdutoService _sqlRepository;
        private readonly ProdutoSqlMongoService _mongoRepository;

        public ProdutoSqlMongoService(IProdutoService sqlRepository, ProdutoSqlMongoService mongoRepository)
        {
            _sqlRepository = sqlRepository;
            _mongoRepository = mongoRepository;
        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync()
        {
            return await _sqlRepository.ObterTodosProdutosAsync();
        }

        public async Task<Produto> ObterProdutoPorIdAsync(int id)
        {
            return await _sqlRepository.ObterProdutoPorIdAsync(id);
        }

        public async Task<Produto> CadastrarProdutoAsync(Produto novoProduto)
        {
            await _sqlRepository.CadastrarProdutoAsync(novoProduto);
            await _mongoRepository.CadastrarProdutoAsync(novoProduto);

            return novoProduto;
        }

        public async Task<Produto> AtualizarProdutoPorIdAsync(int id, Produto produtoAtualizado)
        {
            await _sqlRepository.AtualizarProdutoPorIdAsync(id, produtoAtualizado);
            await _mongoRepository.AtualizarProdutoPorIdAsync(id, produtoAtualizado);

            return produtoAtualizado;
        }

        public async Task<Produto> DeletarProdutoPorIdAsync(int id)
        {
            var produto = await _sqlRepository.DeletarProdutoPorIdAsync(id);
            await _mongoRepository.DeletarProdutoPorIdAsync(id);

            return produto;
        }

        public async Task<Produto> DeletarProdutosPorIdEStatusAsync(int id)
        {
            var produto = await _sqlRepository.DeletarProdutosPorIdEStatusAsync(id);
            await _mongoRepository.DeletarProdutosPorIdEStatusAsync(id);

            return produto;
        }
    }
}
