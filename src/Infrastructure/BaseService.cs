using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectRootNamespace.Api.DataAccess;
using System.Linq;
using System.Linq.Expressions;
using projectRootNamespace.Api.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Http;
using projectRootNamespace.Api.Infrastructure.Exceptions;

namespace projectRootNamespace.Api.Infrastructure
{
    public abstract class BaseService<TEntity, TEntityKey> where TEntity : class, IIdentityEntity<TEntityKey>
    {
        protected readonly MainDbContext _db;
        private readonly IHttpContextAccessor _httpContext;
        protected IQueryable<TEntity> _baseQuery;

        public BaseService(MainDbContext dbContext, IHttpContextAccessor httpContext)
        {
            _db = dbContext;
            _httpContext = httpContext;
            _baseQuery = dbContext.Set<TEntity>().AsNoTracking();
        }

        protected virtual IQueryable<TEntity> GetManyQuery => _baseQuery;

        protected async Task<IEnumerable<TEntity>> DbFindMany()
        {
            return await GetManyQuery
                .ToListAsync();
        }

        protected async Task<IEnumerable<M>> DbFindMany<M>(Expression<Func<TEntity, M>> selector)
        {
            return await GetManyQuery
                .Select(selector)
                .ToListAsync();
        }

        protected async Task<IEnumerable<TEntity>> DbFindMany(Expression<Func<TEntity, bool>> where)
        {
            return await GetManyQuery
                .Where(where)
                .ToListAsync();
        }

        protected async Task<IEnumerable<M>> DbFindMany<M>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, M>> selector)
        {
            var query = GetManyQuery;

            if (where != null)
                query = query.Where(where);

            return await query
                .Select(selector)
                .ToListAsync();
        }

        protected async Task<IEnumerable<M>> DbFindMany<M, TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, M>> selector, Expression<Func<TEntity, TKey>> orderBy)
        {
            var query = GetManyQuery;

            if (where != null)
                query = query.Where(where);

            return await query
                .OrderBy(orderBy)
                .Select(selector)
                .ToListAsync();
        }

        protected async Task<IEnumerable<M>> DbFindManyDescending<M, TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, M>> selector, Expression<Func<TEntity, TKey>> orderBy)
        {
            var query = GetManyQuery;

            if (where != null)
                query = query.Where(where);

            return await query
                .OrderByDescending(orderBy)
                .Select(selector)
                .ToListAsync();
        }

        protected async Task<PagedResultsDTO<M>> DbFindManyDescending<M, TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, M>> selector, Expression<Func<TEntity, TKey>> orderBy, int page, int pageSize)
        {
            if (page == 0)
                page = 1;

            if (pageSize == 0)
                pageSize = int.MaxValue;

            var query = GetManyQuery;
            if (where != null)
                query = query.Where(where);

            var count = await query.CountAsync();
            var items = await query.OrderByDescending(orderBy).Skip((page - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync();

            return new PagedResultsDTO<M>(items, count, page, pageSize);
        }

        protected async Task<PagedResultsDTO<TEntity>> DbFindMany(int page, int pageSize)
        {
            if (page == 0)
                page = 1;

            if (pageSize == 0)
                pageSize = int.MaxValue;

            var count = await GetManyQuery.CountAsync();
            var items = await GetManyQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResultsDTO<TEntity>(items, count, page, pageSize);
        }

        protected async Task<PagedResultsDTO<TEntity>> DbFindMany<M>(Expression<Func<TEntity, bool>> where, int page, int pageSize)
        {
            if (page == 0)
                page = 1;

            if (pageSize == 0)
                pageSize = int.MaxValue;

            var query = GetManyQuery;
            if (where != null)
                query = query.Where(where);

            var count = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResultsDTO<TEntity>(items, count, page, pageSize);
        }

        protected async Task<PagedResultsDTO<M>> DbFindMany<M>(Expression<Func<TEntity, M>> selector, int page, int pageSize)
        {
            if (page == 0)
                page = 1;

            if (pageSize == 0)
                pageSize = int.MaxValue;

            var count = await GetManyQuery.CountAsync();
            var items = await GetManyQuery.Skip((page - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync();

            return new PagedResultsDTO<M>(items, count, page, pageSize);
        }

        protected async Task<PagedResultsDTO<M>> DbFindMany<M>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, M>> selector, int page, int pageSize)
        {
            if (page == 0)
                page = 1;

            if (pageSize == 0)
                pageSize = int.MaxValue;

            var query = GetManyQuery;
            if (where != null)
                query = query.Where(where);

            var count = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync();

            return new PagedResultsDTO<M>(items, count, page, pageSize);
        }

        protected async Task<PagedResultsDTO<M>> DbFindMany<M, TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, M>> selector, Expression<Func<TEntity, TKey>> orderBy, int page, int pageSize)
        {
            if (page == 0)
                page = 1;

            if (pageSize == 0)
                pageSize = int.MaxValue;

            var query = GetManyQuery;
            if (where != null)
                query = query.Where(where);

            var count = await query.CountAsync();
            var items = await query.OrderBy(orderBy).Skip((page - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync();

            return new PagedResultsDTO<M>(items, count, page, pageSize);
        }

        protected async Task<TEntity> DbFind(Expression<Func<TEntity, bool>> where)
        {
            var entity = await DbFindOrDefault(where);
            if (entity == null)
                throw new ApiNotFoundException();

            return entity;
        }

        protected async Task<TEntity> DbFindOrDefault(Expression<Func<TEntity, bool>> where)
        {
            return await _baseQuery
                .AsTracking()
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(where);
        }

        protected async Task<bool> DbExists(Expression<Func<TEntity, bool>> where)
        {
            var count = await _baseQuery
                .IgnoreQueryFilters()
                .CountAsync(where);

            return count == 0 ? false : true;
        }

        protected async Task<TEntity> DbCreate(TEntity entity)
        {
            var dbEntity = _db.Attach(entity);
            // Force Added state for cases where the primary key is already set 
            dbEntity.State = EntityState.Added;

            if (entity is IAuditableEntity)
            {
                var user = GetAuthenticatedUser();
                var date = DateTime.UtcNow;
                (entity as IAuditableEntity).CreatedBy = user;
                (entity as IAuditableEntity).CreatedOn = date;
                (entity as IAuditableEntity).UpdatedBy = user;
                (entity as IAuditableEntity).UpdatedOn = date;
            }

            await _db.SaveChangesAsync();

            return entity;
        }

        protected async Task DbUpdate(TEntityKey key, Action<TEntity> onUpdate)
        {
            var entity = await DbFind(x => x.Id.Equals(key));
            onUpdate(entity);
            await DbUpdate(entity);
        }

        protected async Task DbUpdate(TEntity entity)
        {
            var local = _db.Set<TEntity>()
                    .Local
                    .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            if (entity is IAuditableEntity)
            {
                var user = GetAuthenticatedUser();
                var date = DateTime.UtcNow;
                (entity as IAuditableEntity).UpdatedBy = user;
                (entity as IAuditableEntity).UpdatedOn = date;
            }

            if (local != null)
                _db.Entry(local).CurrentValues.SetValues(entity);
            else
                _db.Entry(entity).State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }

        protected async Task DbCreateOrUpdate(TEntity createEntity, Action<TEntity> onUpdate)
        {
            var entity = await DbFindOrDefault(x => x.Id.Equals(createEntity.Id));
            if (entity == null)
            {
                try
                {
                    await DbCreate(createEntity);
                }
                catch (Exception)
                {
                    await DbUpdate(createEntity.Id, onUpdate);
                }
            }
            else
            {
                onUpdate(entity);
                await DbUpdate(entity);
            }
        }

        protected async Task DbDelete(Expression<Func<TEntity, bool>> where)
        {
            var entity = await _db.Set<TEntity>().FirstOrDefaultAsync(where);

            if (entity == null)
                throw new ApiNotFoundException();

            if (entity is ISoftDeletableEntity)
            {
                // Perform soft delete
                ((ISoftDeletableEntity)entity).Deleted = true;
                if (entity is IAuditableEntity)
                {
                    var user = GetAuthenticatedUser();
                    var date = DateTime.UtcNow;
                    (entity as IAuditableEntity).UpdatedBy = user;
                    (entity as IAuditableEntity).UpdatedOn = date;
                }
                _db.Set<TEntity>().Update(entity);
            }
            else
            {
                // Perform hard delete
                _db.Set<TEntity>().Remove(entity);
            }

            await _db.SaveChangesAsync();
        }

        private string GetAuthenticatedUser()
        {
            return _httpContext?.HttpContext?.User?.Identity?.Name ?? "unknown";
        }
    }

    #region DTOs

    public class PagedResultsDTO<M>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;
        public List<M> Records { get; set; }

        public PagedResultsDTO()
        {
        }

        public PagedResultsDTO(List<M> items, int count, int pageIndex, int pageSize)
        {
            Page = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Records = items;
            TotalRecords = count;
        }
    }

    #endregion
}
