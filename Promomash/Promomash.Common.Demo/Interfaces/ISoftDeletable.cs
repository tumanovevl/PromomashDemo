namespace Promomash.Common.Interfaces
{
    /// <summary>
    /// Interface for entities that should not be deleted from database
    /// </summary>
    public interface ISoftDeletable
    {
        /// <summary>
        /// Is entity soft-deleted or not
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
