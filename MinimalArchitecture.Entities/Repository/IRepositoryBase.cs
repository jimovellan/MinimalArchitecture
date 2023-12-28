using System.Linq.Expressions;

namespace MinimalArchitecture.Entities.Repository;
public interface IRepositoryBase<TEntity> where TEntity: BaseEntity
{
    IQueryable<TEntity> GetQuery();
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default, bool cache = false);
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition, IList<Expression<Func<TEntity, object>>> includes, CancellationToken cancellationToken = default, bool cache = false);
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);

    void Insert(TEntity entity);
    void Insert(IEnumerable<TEntity> entity);
    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entity);
    void Delete(TEntity entity);
    void Delete(IEnumerable<TEntity> entities);

    Task<IEnumerable<TEntity>> GetWithSpecAsync(SpecificationPattern<TEntity> spec, CancellationToken cancellation = default);
}
