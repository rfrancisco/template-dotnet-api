namespace ProjectName.Api.Infrastructure.DataAccess
{
    public interface IDeletableEntity
    {
        /// <summary>
        /// Identifies if the object was flagged as deleted.
        /// Deleted records are usualy ignored by GetAll methods.
        /// </summary>
        bool Deleted { get; set; }
    }
}