using CP3.Data.AppData;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CP3.Data.Repositories
{
    public class BarcoRepository : IBarcoRepository
    {
        private readonly ApplicationContext _context;

        public BarcoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public BarcoEntity? ObterPorId(int id)
        {
            return _context.Barco.Find(id);
        }

        public IEnumerable<BarcoEntity>? ObterTodos()
        {
            return _context.Barco.ToList();
        }

        public BarcoEntity? Adicionar(BarcoEntity barco)
        {
            _context.Barco.Add(barco);
            _context.SaveChanges();
            return barco;
        }

        public BarcoEntity? Editar(BarcoEntity barco)
        {
            var entity = _context.Barco.Find(barco.Id);
            if (entity == null)
                return null;

            entity.Nome = barco.Nome;
            entity.Modelo = barco.Modelo;
            entity.Ano = barco.Ano;
            entity.Tamanho = barco.Tamanho;

            _context.SaveChanges();
            return entity;
        }

        public BarcoEntity? Remover(int id)
        {
            var entity = _context.Barco.Find(id);
            if (entity == null)
                return null;

            _context.Barco.Remove(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}