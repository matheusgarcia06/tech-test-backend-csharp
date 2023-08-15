using System.Text.Json;
using TechTestBackendCSharp.Enums;
using TechTestBackendCSharp.Models;

namespace TechTestBackendCSharp.Services.ProdutoService
{
    public class ProdutoFileService : IProdutoService
    {
        private readonly string _filePath;

        public ProdutoFileService(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync()
        {
            return await LerProdutosDoArquivo();
        }

        public async Task<Produto> ObterProdutoPorIdAsync(int id)
        {
            var produtos = await LerProdutosDoArquivo();

            foreach (var produto in produtos)
            {
                if (produto.Id == id)
                {
                    return produto;
                }
            }

            return null;
        }

        public async Task<Produto> CadastrarProdutoAsync(Produto novoProduto)
        {
            var produtosJson = await File.ReadAllTextAsync(_filePath);
            var produtos = JsonSerializer.Deserialize<List<Produto>>(produtosJson) ?? new List<Produto>();

            novoProduto.Id = produtos.Count + 1;
            produtos.Add(novoProduto);

            var produtosAtualizadosJson = JsonSerializer.Serialize(produtos);
            await File.WriteAllTextAsync(_filePath, produtosAtualizadosJson);

            return novoProduto;
        }

        public async Task<Produto> AtualizarProdutoPorIdAsync(int id, Produto produtoAtualizado)
        {
            var produtos = await LerProdutosDoArquivo();

            var produtoExistente = produtos.FirstOrDefault(p => p.Id == id);
            if (produtoExistente != null)
            {
                produtoExistente.Nome = produtoAtualizado.Nome;
                produtoExistente.Preco = produtoAtualizado.Preco;
                produtoExistente.QuantidadeEmEstoque = produtoAtualizado.QuantidadeEmEstoque;

                await EscreverProdutosNoArquivo(produtos);
            }

            return produtoExistente;
        }

        public async Task<Produto> DeletarProdutoPorIdAsync(int id)
        {
            var produtos = await LerProdutosDoArquivo();

            var produtoParaDeletar = produtos.FirstOrDefault(p => p.Id == id);
            if (produtoParaDeletar != null)
            {
                produtos.Remove(produtoParaDeletar);
                await EscreverProdutosNoArquivo(produtos);
            }

            return produtoParaDeletar;
        }

        public async Task<Produto> DeletarProdutosPorIdEStatusAsync(int id)
        {
            var produtos = await LerProdutosDoArquivo();

            var produtoParaInativar = produtos.FirstOrDefault(p => p.Id == id && p.Status == StatusProduto.Ativo);
            if (produtoParaInativar != null)
            {
                produtoParaInativar.Status = StatusProduto.Inativo;
                await EscreverProdutosNoArquivo(produtos);
            }

            return produtoParaInativar;
        }

        private async Task<List<Produto>> LerProdutosDoArquivo()
        {
            if (File.Exists(_filePath))
            {
                var produtosJson = await File.ReadAllTextAsync(_filePath);
                return JsonSerializer.Deserialize<List<Produto>>(produtosJson);
            }
            else
            {
                await File.WriteAllTextAsync(_filePath, "[]");
                return new List<Produto>();
            }
        }

        private async Task EscreverProdutosNoArquivo(List<Produto> produtos)
        {
            var produtosJson = JsonSerializer.Serialize(produtos);
            await File.WriteAllTextAsync(_filePath, produtosJson);
        }
    }
}
