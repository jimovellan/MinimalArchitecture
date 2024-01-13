using MinimalArchitecture.Entities.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Repository
{
    public class SqlServerRepository<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private readonly DbContext _ctx;
        private readonly ISpecificationResolver _specificationResolver;

        public SqlServerRepository(AppDBContext context, ISpecificationResolver specificationResolver)
        {
            _ctx = context;
            _specificationResolver = specificationResolver;
        }
        public Task<int> CountAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default)
        {
            return _ctx.Set<T>().Where(where).CountAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            _ctx.Update(entity);
        }
        public void Update(IEnumerable<T> entities)
        {
            _ctx.UpdateRange(entities);
        }

        public void Delete(T entity)
        {
            _ctx.Set<T>().Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _ctx.Set<T>().RemoveRange(entities);
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default)
        {
            return await _ctx.Set<T>().AnyAsync(where, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default, bool cache = false)
        {
            return await GetAsync(condition, null ,cancellationToken, cache);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> condition, IList<Expression<Func<T, object>>> includes, CancellationToken cancellationToken = default, bool cache = false)
        {
            var query = _ctx.Set<T>().AsQueryable();

            if(condition != null)
            {
                query = query.Where(condition);
            }

            if(includes != null)
            {
               query =  includes.Aggregate(query,(acum, elem) => acum.Include(elem));
            }

            if (!cache)
            {
                query.AsNoTracking();
            }

            return await query.ToListAsync();
        }

        public IQueryable<T> GetQuery()
        {
           return _ctx.Set<T>().AsQueryable();
        }

        public async Task<IEnumerable<T>> GetWithSpecAsync(SpecificationPattern<T> spec, CancellationToken cancellation = default)
        {
            return await _specificationResolver.Execute(spec, cancellation);
        }

        public void Insert(T entity)
        {
            _ctx.Set<T>().Add(entity);

           
        }

        public void Insert(IEnumerable<T> entities)
        {
            _ctx.Set<T>().AddRange(entities);

        }

    }
}
