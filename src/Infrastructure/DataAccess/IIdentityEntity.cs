using System;

namespace ProjectName.Api.Infrastructure.DataAccess
{
    public interface IIdentityEntity<TKey>
    {
        TKey Id { get; set; }
    }
}