using Microsoft.EntityFrameworkCore;
using TechTestBackendCSharp.Data;
using TechTestBackendCSharp.Enums;
using TechTestBackendCSharp.Models;

namespace TechTestBackendCSharp.Services.ProdutoService
{
    public class ProdutoSqlServerService : IProdutoService
    {
        private readonly AppDbContext _context;

        public ProdutoSqlServerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync()
        {
            return await _context.Produtos.Where(x => x.Status == StatusProduto.Ativo).ToListAsync();
        }

        public async Task<Produto> ObterProdutoPorIdAsync(int id)
        {
            return await _context.Produtos.AsNoTracking().FirstAsync(x => x.Id == id);
        }
        public async Task<Produto> CadastrarProdutoAsync(Produto novoProduto)
        {
            var produtoEntry = await _context.Produtos.AddAsync(novoProduto);
            await _context.SaveChangesAsync();
            return produtoEntry.Entity;
        }

        public async Task<Produto> AtualizarProdutoPorIdAsync(int id, Produto produtoAtualizado)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

            if (produto != null)
            {
                if (!string.IsNullOrEmpty(produtoAtualizado.Nome))
                {
                    produto.Nome = produtoAtualizado.Nome;
                }

                if (produtoAtualizado.Preco > 0)
                {
                    produto.Preco = produtoAtualizado.Preco;
                }

                if (produtoAtualizado.QuantidadeEmEstoque >= 0)
                {
                    produto.QuantidadeEmEstoque = produtoAtualizado.QuantidadeEmEstoque;
                }
                
                await _context.SaveChangesAsync();
            }

            return produto;
        }
        
        public async Task<Produto> DeletarProdutoPorIdAsync(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

            if (produto != null)
            {
                _context.Remove(produto);
                await _context.SaveChangesAsync();
            }

            return produto; 
        }

        public async Task<Produto> DeletarProdutosPorIdEStatusAsync(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(x => x.Id == id);

            if (produto != null)
            {
                if (produto.Status == StatusProduto.Inativo)
                {
                    return null; 
                }

                produto.Status = StatusProduto.Inativo;
                await _context.SaveChangesAsync();
            }

            return produto;
        }
    }
}
