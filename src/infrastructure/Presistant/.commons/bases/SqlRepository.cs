using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.extensions;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.primitives;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.sharedDatas;
using DomainDrivenDesignArchitucture.Infrastructure.Presistant.contexts;
using System.Linq.Expressions;

namespace DomainDrivenDesignArchitucture.Infrastructure.Presistant.commons.bases;
internal class SqlRepository<T, U> : IRepository<T, U> where T : BaseEntity<U>
{
    private readonly DbSet<T> _set;

    internal SqlRepository(SqlContext context)
    {
        _set = context.Set<T>();
    }

    public async Task<T?> Get(Expression<Func<T, bool>> predicts, params string[] includes)
        => await _set
        .ApplyIncludes<T, U>(includes)
        .ApplyPredict<T, U>(predicts)
        .FirstOrDefaultAsync();

    public async Task<ListResult<T>> GetAllAsc(Expression<Func<T, bool>> predicts, Expression<Func<T, object>> selector, params string[] includes)
    {
        var queryable = _set
            .ApplyIncludes<T, U>(includes)
            .ApplyPredict<T, U>(predicts)
            .ApplyOrderAsc<T, U>(selector);

        return new ListResult<T>(await queryable.CountAsync(), await queryable.ToListAsync());
    }

    public async Task<ListResult<T>> GetAllDesc(Expression<Func<T, bool>> predicts, Expression<Func<T, object>> selector, params string[] includes)
    {
        var queryable = _set
            .ApplyIncludes<T, U>(includes)
            .ApplyPredict<T, U>(predicts)
            .ApplyOrderDesc<T, U>(selector);

        return new ListResult<T>(await queryable.CountAsync(), await queryable.ToListAsync());
    }

    public async Task<ListResult<T>> GetPagingAsc(Expression<Func<T, bool>> predicts, Expression<Func<T, object>> selector, int pageSize, int page, params string[] includes)
    {
        var queryable = _set
            .ApplyIncludes<T, U>(includes)
            .ApplyPredict<T, U>(predicts)
            .ApplyOrderAsc<T, U>(selector);

        return new ListResult<T>(await queryable.CountAsync(), await queryable.ApplyPaging<T, U>(pageSize, page).ToListAsync());
    }

    public async Task<ListResult<T>> GetPagingDesc(Expression<Func<T, bool>> predicts, Expression<Func<T, object>> selector, int pageSize, int page, params string[] includes)
    {
        var queryable = _set
            .ApplyIncludes<T, U>(includes)
            .ApplyPredict<T, U>(predicts)
            .ApplyOrderDesc<T, U>(selector);

        return new ListResult<T>(await queryable.CountAsync(), await queryable.ApplyPaging<T, U>(pageSize, page).ToListAsync());
    }

    public async Task<int> Count(Expression<Func<T, bool>> predicts)
        => await _set
        .ApplyPredict<T, U>(predicts)
        .CountAsync();

    public async Task<decimal> Sum(Expression<Func<T, bool>> predicts, Expression<Func<T, decimal>> selector) 
        => await _set
        .ApplyPredict<T, U>(predicts)
        .SumAsync(selector);

    public async Task<T> Create(T entity)
    {
        await _set.AddAsync(entity);
        return entity;
    }

    public T Update(T entity)
    {
        _set.Update(entity);
        return entity;
    }

    public void Delete(T entity)
    {
        _set.Remove(entity);
    }
}
