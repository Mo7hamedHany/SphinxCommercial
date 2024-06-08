using Microsoft.EntityFrameworkCore;
using SphinxCommercial.Core.Interfaces;
using SphinxCommercial.Core.Interfaces.Repositories;
using SphinxCommercial.Core.Models;
using SphinxCommercial.Repository.Data.Contexts;
using SphinxCommercial.Repository.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphinxCommercial.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseModel<TKey>
    {
        private readonly AppDataContext _context;

        public GenericRepository(AppDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetAsync(TKey id) => (await _context.Set<TEntity>().FindAsync(id))!;
        
        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);

        public async Task<int> CountAsync(ISpecification<TEntity> specification) => await ApplySpecifications(specification).CountAsync();

        public async Task<TEntity> GetWithSpecsAsync(ISpecification<TEntity> specification) => (await ApplySpecifications(specification).FirstOrDefaultAsync())!;

        public async Task<IEnumerable<TEntity>> GetAllWithSpecsAsync(ISpecification<TEntity> specification) => await ApplySpecifications(specification).ToListAsync();



        private IQueryable<TEntity> ApplySpecifications(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity, TKey>.BuildQuery(_context.Set<TEntity>(), specification);
        }
    }
}
