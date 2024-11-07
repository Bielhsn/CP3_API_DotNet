using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly BarcoApplicationService _barcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void ObterTodosBarcos_DeveRetornarListaDeBarcos()
        {
            // Arrange
            var barcos = new List<BarcoEntity> { new BarcoEntity { Id = 1, Nome = "Barco Teste" } };
            _repositoryMock.Setup(repo => repo.ObterTodos()).Returns(barcos);

            // Act
            var result = _barcoService.ObterTodosBarcos();

            // Assert
            Assert.Equal(barcos, result);
        }

        [Fact]
        public void ObterBarcoPorId_DeveRetornarBarcoQuandoIdExiste()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco Teste" };
            _repositoryMock.Setup(repo => repo.ObterPorId(1)).Returns(barco);

            // Act
            var result = _barcoService.ObterBarcoPorId(1);

            // Assert
            Assert.Equal(barco, result);
        }

        [Fact]
        public void AdicionarBarco_DeveAdicionarENaoRetornarNulo()
        {
            // Arrange
            var dtoMock = new Mock<IBarcoDto>();
            dtoMock.SetupGet(d => d.Nome).Returns("Barco Novo");
            dtoMock.SetupGet(d => d.Modelo).Returns("Modelo X");
            dtoMock.SetupGet(d => d.Ano).Returns(2020);
            dtoMock.SetupGet(d => d.Tamanho).Returns(30.0);

            var barco = new BarcoEntity { Id = 1, Nome = "Barco Novo" };
            _repositoryMock.Setup(repo => repo.Adicionar(It.IsAny<BarcoEntity>())).Returns(barco);

            // Act
            var result = _barcoService.AdicionarBarco(dtoMock.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco Novo", result.Nome);
        }

        [Fact]
        public void EditarBarco_DeveEditarENaoRetornarNuloQuandoIdExiste()
        {
            // Arrange
            var dtoMock = new Mock<IBarcoDto>();
            dtoMock.SetupGet(d => d.Nome).Returns("Barco Atualizado");
            dtoMock.SetupGet(d => d.Modelo).Returns("Modelo Y");
            dtoMock.SetupGet(d => d.Ano).Returns(2021);
            dtoMock.SetupGet(d => d.Tamanho).Returns(35.0);

            var barcoExistente = new BarcoEntity { Id = 1, Nome = "Barco Existente" };
            _repositoryMock.Setup(repo => repo.ObterPorId(1)).Returns(barcoExistente);
            _repositoryMock.Setup(repo => repo.Editar(It.IsAny<BarcoEntity>())).Returns((BarcoEntity b) => b);

            // Act
            var result = _barcoService.EditarBarco(1, dtoMock.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco Atualizado", result.Nome);
        }

        [Fact]
        public void RemoverBarco_DeveRemoverERetornarBarcoQuandoIdExiste()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco a Remover" };
            _repositoryMock.Setup(repo => repo.Remover(1)).Returns(barco);

            // Act
            var result = _barcoService.RemoverBarco(1);

            // Assert
            Assert.Equal(barco, result);
        }
    }
}