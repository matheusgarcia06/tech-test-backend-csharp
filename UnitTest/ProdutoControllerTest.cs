using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechTestBackendCSharp.Controllers;
using TechTestBackendCSharp.Services;
using TechTestBackendCSharp.Models;
using TechTestBackendCSharp.Enums;

namespace UnitTest
{
    public class ProdutoControllerTest
    {
        [Fact]
        public async Task TestCadastrarProduto_PrecoInvalido()
        {
            // Arrange
            var mockProdutoService = new Mock<IProdutoService>();
            var produtosController = new ProdutosController(mockProdutoService.Object);

            var produtoViewModel = new ProdutoViewModel
            {
                Nome = "Produto Inválido",
                Preco =  -1, // Preço inválido
                QuantidadeEmEstoque = 10
            };

            // Act
            var result = await produtosController.CadastrarProduto(produtoViewModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("O preço do produto deve ser maior que zero.", badRequestResult.Value);
        }

        [Fact]
        public async Task TestAtualizarProdutoPorId_ProdutoExistente()
        {
            // Arrange
            var mockProdutoService = new Mock<IProdutoService>();
            var produtosController = new ProdutosController(mockProdutoService.Object);

            var produtoAtualizadoViewModel = new ProdutoViewModel
            {
                Nome = "Produto Atualizado",
                Preco = 15,
                QuantidadeEmEstoque = 8
            };

            var produtoExistente = new Produto
            {
                Id = 1,
                Nome = "Produto Existente",
                Preco = 9.99M,
                QuantidadeEmEstoque = 5,
                Status = StatusProduto.Ativo
            };

            mockProdutoService.Setup(service => service.AtualizarProdutoPorIdAsync(produtoExistente.Id, It.IsAny<Produto>()))
                              .ReturnsAsync(produtoExistente);

            var result = await produtosController.AtualizarProdutoPorId(produtoAtualizadoViewModel, produtoExistente.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task TestDeletarProdutosPorIdEStatus_ProdutoExistente()
        {
            var mockProdutoService = new Mock<IProdutoService>();
            var produtosController = new ProdutosController(mockProdutoService.Object);

            var produtoExistente = new Produto
            {
                Id = 1,
                Nome = "Produto Existente",
                Preco = 9.99M,
                QuantidadeEmEstoque = 5,
                Status = StatusProduto.Ativo
            };

            mockProdutoService.Setup(service => service.DeletarProdutosPorIdEStatusAsync(produtoExistente.Id))
                              .ReturnsAsync(produtoExistente);

            var result = await produtosController.DeletarProdutosPorIdEStatus(produtoExistente.Id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

        }
    }
}
