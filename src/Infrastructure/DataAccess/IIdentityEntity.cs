using System;

namespace projectRootNamespace.Api.Infrastructure.DataAccess
{
    public interface IIdentityEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
