using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.sharedDatas;
using System.Linq.Expressions;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.bases;
internal interface IRepository<T, U> where T: BaseEntity<U>
{
    Task<T?> Get(Expression<Func<T, bool>> predicts, params string[] includes);

    Task<ListResult<T>> GetAllAsc(Expression<Func<T, bool>> predicts, Expression<Func<T, object>> selector, params string[] includes);

    Task<ListResult<T>> GetAllDesc(Expression<Func<T, bool>> predicts, Expression<Func<T, object>> selector, params string[] includes);

    Task<ListResult<T>> GetPagingAsc(Expression<Func<T, bool>> predicts, Expression<Func<T, object>> selector, int pageSize, int page, params string[] includes);

    Task<ListResult<T>> GetPagingDesc(Expression<Func<T, bool>> predicts, Expression<Func<T, object>> selector, int pageSize, int page, params string[] includes);

    Task<int> Count(Expression<Func<T, bool>> predicts);

    Task<decimal> Sum(Expression<Func<T, bool>> predicts, Expression<Func<T, decimal>> selector);

    Task<T> Create(T entity);

    T Update(T entity);

    void Delete(T entity);
}
