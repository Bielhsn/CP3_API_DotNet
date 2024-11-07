using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _barcoRepository;

        public BarcoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationContext(options);
            _barcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public void ObterPorId_DeveRetornarBarcoQuandoIdExiste()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco Teste" };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            // Act
            var result = _barcoRepository.ObterPorId(1);

            // Assert
            Assert.Equal(barco, result);
        }

        [Fact]
        public void ObterTodos_DeveRetornarListaDeBarcos()
        {
            // Arrange
            var barcos = new List<BarcoEntity>
            {
                new BarcoEntity { Id = 1, Nome = "Barco Teste 1" },
                new BarcoEntity { Id = 2, Nome = "Barco Teste 2" }
            };
            _context.Barco.AddRange(barcos);
            _context.SaveChanges();

            // Act
            var result = _barcoRepository.ObterTodos().ToList();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Adicionar_DeveAdicionarNovoBarco()
        {
            // Arrange
            var barco = new BarcoEntity { Nome = "Barco Novo", Modelo = "Modelo Z", Ano = 2022, Tamanho = 25.0 };

            // Act
            var result = _barcoRepository.Adicionar(barco);
            var savedBarco = _context.Barco.Find(result.Id);

            // Assert
            Assert.NotNull(savedBarco);
            Assert.Equal("Barco Novo", savedBarco.Nome);
        }

        [Fact]
        public void Editar_DeveEditarBarcoExistente()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco Original", Modelo = "Modelo X", Ano = 2020, Tamanho = 30.0 };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            barco.Nome = "Barco Atualizado";

            // Act
            var result = _barcoRepository.Editar(barco);
            var updatedBarco = _context.Barco.Find(result.Id);

            // Assert
            Assert.NotNull(updatedBarco);
            Assert.Equal("Barco Atualizado", updatedBarco.Nome);
        }

        [Fact]
        public void Remover_DeveRemoverBarcoExistente()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco a Remover" };
            _context.Barco.Add(barco);
            _context.SaveChanges();

            // Act
            var result = _barcoRepository.Remover(1);
            var removedBarco = _context.Barco.Find(1);

            // Assert
            Assert.NotNull(result);
            Assert.Null(removedBarco);
        }
    }
}