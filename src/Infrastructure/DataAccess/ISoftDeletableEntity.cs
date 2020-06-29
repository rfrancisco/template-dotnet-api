namespace projectRootNamespace.Api.Infrastructure.DataAccess
{
    public interface ISoftDeletableEntity
    {
        /// <summary>
        /// Identifies if the object was flagged as deleted.
        /// Deleted records are usualy ignored by GetAll methods.
        /// </summary>
        bool Deleted { get; set; }
    }
}
