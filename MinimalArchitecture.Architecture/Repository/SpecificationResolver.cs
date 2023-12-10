using Microsoft.EntityFrameworkCore;
using MinimalArchitecture.Common.Extensions;
using MinimalArchitecture.Entities.Repository;


namespace MinimalArchitecture.Architecture.Repository
{
    public class SpecificationResolver : ISpecificationResolver
    {
        private DbContext _ctx;
        public SpecificationResolver(AppDBContext context)
        {
            _ctx = context;
        }
        public async Task<IEnumerable<T>> Execute<T>(SpecificationPattern<T> spec, CancellationToken cancellation = default) where T : BaseEntity
        {
            var query = _ctx.Set<T>().AsQueryable();

            if (spec.Includes.HasElements())
            {
                query = spec.Includes
                            .Aggregate(query, (acum, include) => acum.Include(include));
            }

            if(spec.Filter is not null)
            {
                query = query.Where(spec.Filter);
            }

            if (!spec.Caching)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync(cancellation);
        }
    }
}
