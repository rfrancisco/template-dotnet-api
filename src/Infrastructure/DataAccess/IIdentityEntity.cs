using System;

namespace ProjectRootNamespace.Api.Infrastructure.DataAccess
{
    public interface IIdentityEntity<TKey>
    {
        TKey Id { get; set; }
    }
}