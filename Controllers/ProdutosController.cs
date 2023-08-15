using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using TechTestBackendCSharp.Enums;
using TechTestBackendCSharp.Models;
using TechTestBackendCSharp.Services;

namespace TechTestBackendCSharp.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [Route("produtos")]
        public async Task<IActionResult> ObterTodosProdutos()
        {
            try
            {
                var produtos = await _produtoService.ObterTodosProdutosAsync();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Não foi possível obter a lista de produtos. Detalhes do erro: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("produtos/{id}")]
        public async Task<IActionResult> ObterProdutoPorId([FromRoute] int id)
        {
            try
            {
                var produto = await _produtoService.ObterProdutoPorIdAsync(id);

                if (produto == null)
                    return NotFound("Produto não encontrado");

                if (produto.Status == StatusProduto.Inativo)
                    return BadRequest("Este produto está inativo.");

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter detalhes do produto: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("produtos")]
        public async Task<IActionResult> CadastrarProduto([FromBody] ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (produtoViewModel.Preco <= 0)
                return BadRequest("O preço do produto deve ser maior que zero.");

            var produto = new Produto
            {
                Nome = produtoViewModel.Nome,
                Preco = produtoViewModel.Preco,
                QuantidadeEmEstoque = produtoViewModel.QuantidadeEmEstoque,
                DataDeCriacao = DateTime.Now
            };

            try
            {
                await _produtoService.CadastrarProdutoAsync(produto);

                return Created($"v1/todos/{produto.Id}", produto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao cadastrar o produto: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("produtos/{id}")]
        public async Task<IActionResult> AtualizarProdutoPorId([FromBody] ProdutoViewModel produtoViewModel, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var produtoAtualizado = new Produto
            {
                Nome = produtoViewModel.Nome,
                Preco = produtoViewModel.Preco,
                QuantidadeEmEstoque = produtoViewModel.QuantidadeEmEstoque
            };

            try
            {
                var produto = await _produtoService.AtualizarProdutoPorIdAsync(id, produtoAtualizado);

                if (produto == null)
                    return NotFound("Produto não encontrado");


                var retorno = new
                {
                    Produto = produto,
                    Mensagem = $"Produto {produto.Id} foi atualizado com sucesso."
                };

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar o produto: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("produtos/{id}")]
        public async Task<IActionResult> DeletarProdutoPorId([FromRoute] int id)
        {
            try
            {
                var produto = await _produtoService.DeletarProdutoPorIdAsync(id);

                if (produto == null)
                    return NotFound("Produto não encontrado");

                var retorno = new
                {
                    Produto = produto,
                    Mensagem = $"Produto {produto.Id} foi deletado com sucesso."
                };

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar o produto: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("produtos/{id}/status")]
        public async Task<IActionResult> DeletarProdutosPorIdEStatus([FromRoute] int id)
        {
            try
            {
                var produto = await _produtoService.DeletarProdutosPorIdEStatusAsync(id);

                if (produto == null)
                    return NotFound("Produto não encontrado ou já está inativo");

                var retorno = new
                {
                    Produto = produto,
                    Mensagem = $"Produto {produto.Id} foi inativado com sucesso."
                };

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao inativar o produto: {ex.Message}");
            }
        }
    }
}
