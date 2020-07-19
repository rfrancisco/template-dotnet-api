namespace projectRootNamespace.Api.Infrastructure.DataAccess
{
    public interface ISoftDeletableEntity
    {
        bool Deleted { get; set; }
    }
}
