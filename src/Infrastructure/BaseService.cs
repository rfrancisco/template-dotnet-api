using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectName.Api.DataAccess;
using System.Linq;
using System.Linq.Expressions;
using ProjectName.Api.Infrastructure.DataAccess;

namespace ProjectName.Api.Infrastructure
{
    public abstract class BaseService<T, IKey> where T : class, IIdentityEntity<IKey>
    {
        protected readonly MainDbContext _db;
        protected IQueryable<T> _baseQuery;

        public BaseService(MainDbContext dbContext)
        {
            _db = dbContext;
            _baseQuery = dbContext.Set<T>().AsNoTracking();
        }

        protected virtual IQueryable<T> GetManyQuery => _baseQuery;

        protected async Task<IEnumerable<T>> DbGetMany()
        {
            return await GetManyQuery
                .ToListAsync();
        }

        protected async Task<IEnumerable<M>> DbGetMany<M>(Expression<Func<T, M>> selector)
        {
            return await GetManyQuery
                .Select(selector)
                .ToListAsync();
        }

        protected async Task<IEnumerable<T>> DbGetMany(Expression<Func<T, bool>> where)
        {
            return await GetManyQuery
                .Where(where)
                .ToListAsync();
        }

        protected async Task<IEnumerable<M>> DbGetMany<M>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector)
        {
            var query = GetManyQuery;

            if (where != null)
                query = query.Where(where);

            return await query
                .Select(selector)
                .ToListAsync();
        }

        protected async Task<IEnumerable<M>> DbGetMany<M, TKey>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector, Expression<Func<T, TKey>> orderBy)
        {
            var query = GetManyQuery;

            if (where != null)
                query = query.Where(where);

            return await query
                .OrderBy(orderBy)
                .Select(selector)
                .ToListAsync();
        }
        protected async Task<IEnumerable<M>> DbGetManyDescending<M, TKey>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector, Expression<Func<T, TKey>> orderBy)
        {
            var query = GetManyQuery;

            if (where != null)
                query = query.Where(where);

            return await query
                .OrderByDescending(orderBy)
                .Select(selector)
                .ToListAsync();
        }

        protected async Task<PagedResults<M>> DbGetManyDescending<M, TKey>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector, Expression<Func<T, TKey>> orderBy, int page, int pageSize)
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

            return new PagedResults<M>(items, count, page, pageSize);
        }

        protected async Task<PagedResults<T>> DbGetMany(int page, int pageSize)
        {
            if (page == 0)
                page = 1;

            if (pageSize == 0)
                pageSize = int.MaxValue;

            var count = await GetManyQuery.CountAsync();
            var items = await GetManyQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResults<T>(items, count, page, pageSize);
        }

        protected async Task<PagedResults<T>> DbGetMany<M>(Expression<Func<T, bool>> where, int page, int pageSize)
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

            return new PagedResults<T>(items, count, page, pageSize);
        }

        protected async Task<PagedResults<M>> DbGetMany<M>(Expression<Func<T, M>> selector, int page, int pageSize)
        {
            if (page == 0)
                page = 1;

            if (pageSize == 0)
                pageSize = int.MaxValue;

            var count = await GetManyQuery.CountAsync();
            var items = await GetManyQuery.Skip((page - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync();

            return new PagedResults<M>(items, count, page, pageSize);
        }

        protected async Task<PagedResults<M>> DbGetMany<M>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector, int page, int pageSize)
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

            return new PagedResults<M>(items, count, page, pageSize);
        }

        protected async Task<PagedResults<M>> DbGetMany<M, TKey>(Expression<Func<T, bool>> where, Expression<Func<T, M>> selector, Expression<Func<T, TKey>> orderBy, int page, int pageSize)
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

            return new PagedResults<M>(items, count, page, pageSize);
        }

        protected async Task<T> DbGet(Expression<Func<T, bool>> where)
        {
            var entity = await DbGetOrDefault(where);
            if (entity == null)
                throw new ApiNotFoundException();

            return entity;
        }

        protected async Task<T> DbGetOrDefault(Expression<Func<T, bool>> where)
        {
            return await _baseQuery.AsTracking().FirstOrDefaultAsync(where);
        }

        protected async Task<bool> DbExists(Expression<Func<T, bool>> where)
        {
            var count = await _baseQuery
                .CountAsync(where);

            return count == 0 ? false : true;
        }

        protected async Task<T> DbCreate(T entity)
        {
            var dbEntity = _db.Attach(entity);
            // Force Added state for cases where the primary key is already set 
            dbEntity.State = EntityState.Added;
            await _db.SaveChangesAsync();

            return entity;
        }

        protected async Task DbUpdate(IKey key, Action<T> onUpdate)
        {
            var entity = await DbGet(x => x.Id.Equals(key));
            onUpdate(entity);
            await DbUpdate(entity);
        }

        protected async Task DbUpdate(T entity)
        {
            var local = _db.Set<T>()
                    .Local
                    .FirstOrDefault(entry => entry.Id.Equals(entity.Id));

            if (local != null)
                _db.Entry(local).CurrentValues.SetValues(entity);
            else
                _db.Entry(entity).State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }

        protected async Task DbCreateOrUpdate(T createEntity, Action<T> onUpdate)
        {
            var entity = await DbGetOrDefault(x => x.Id.Equals(createEntity.Id));
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

        protected async Task DbDelete(Expression<Func<T, bool>> where)
        {
            var entity = await _db.Set<T>().FirstOrDefaultAsync(where);

            if (entity == null)
                throw new ApiNotFoundException();

            if (entity is IDeletableEntity)
            {
                // Perform soft delete
                ((IDeletableEntity)entity).Deleted = true;
                _db.Set<T>().Update(entity);
            }
            else
            {
                // Perform hard delete
                _db.Set<T>().Remove(entity);
            }

            await _db.SaveChangesAsync();
        }

        protected async Task DbHardDelete(Expression<Func<T, bool>> where)
        {
            var entity = await _db.Set<T>().FirstOrDefaultAsync(where);

            if (entity == null)
                throw new ApiNotFoundException();

            await DbHardDelete(entity);
        }

        protected async Task DbHardDelete(T entity)
        {
            _db.Set<T>().Remove(entity);

            await _db.SaveChangesAsync();
        }
    }

    #region Models

    public class PagedResults<M>
    {
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public List<M> Records { get; set; }
        public int TotalRecords { get; set; }

        public PagedResults()
        {
        }

        public PagedResults(List<M> items, int count, int pageIndex, int pageSize)
        {
            Page = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Records = items;
            TotalRecords = count;
        }

        public bool HasPreviousPage
        {
            get { return (Page > 1); }
        }

        public bool HasNextPage
        {
            get { return (Page < TotalPages); }
        }
    }

    #endregion
}
