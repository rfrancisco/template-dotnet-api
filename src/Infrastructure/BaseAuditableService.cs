using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjectName.Api.DataAccess;
using ProjectName.Api.Infrastructure.DataAccess;
using ProjectName.Api.Infrastructure.Security;

namespace ProjectName.Api.Infrastructure
{
    public abstract class BaseAuditableService<T, TKey> : BaseService<T, TKey> where T : class, IAuditableEntity, IIdentityEntity<TKey>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticatedUserProvider _authenticatedUserProvider;

        public BaseAuditableService(
            MainDbContext dbContext,
            IAuthenticatedUserProvider authenticatedUserProvider,
            IHttpContextAccessor httpContextAccessor) : base(dbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticatedUserProvider = authenticatedUserProvider;

            // Exclude deleted records by default if T implements the IDeletableEntity interface
            if (typeof(IDeletableEntity).IsAssignableFrom(typeof(T)))
                _baseQuery = _baseQuery.Where(x => !(x as IDeletableEntity).Deleted);
        }

        protected string Username
        {
            get
            {
                string username = null;
                var user = _httpContextAccessor.HttpContext.User;
                if (user != null && user.Identity.IsAuthenticated)
                    username = _authenticatedUserProvider.GetAuthenticatedUser(_httpContextAccessor.HttpContext.User).UniqueIdentifier;

                return username ?? "system";
            }
        }

        protected new async Task<T> DbCreate(T entity)
        {
            entity.CreatedBy = Username;
            entity.CreatedOn = DateTime.UtcNow;
            entity.UpdatedBy = Username;
            entity.UpdatedOn = DateTime.UtcNow;

            return await base.DbCreate(entity);
        }

        protected new async Task DbUpdate(T entity)
        {
            entity.UpdatedBy = Username;
            entity.UpdatedOn = DateTime.UtcNow;

            await base.DbUpdate(entity);
        }

        protected new async Task DbDelete(Expression<Func<T, bool>> where)
        {
            var entity = await DbGet(where);

            if (typeof(IDeletableEntity).IsAssignableFrom(typeof(T)))
            {
                (entity as IDeletableEntity).Deleted = true;
                await DbUpdate(entity);
            }
            else
            {
                throw new NotSupportedException("The entity does not support soft deletes. Call DbDeleteHard instead");
            }

        }
    }
}
