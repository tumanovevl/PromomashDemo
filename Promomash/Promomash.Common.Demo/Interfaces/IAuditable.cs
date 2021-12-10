using System;

namespace Promomash.Common.Interfaces
{
    /// <summary>
    /// Interface for entities that should track creation & update time
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Date and time when this entity was created
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time when this entity was updated
        /// </summary>
        DateTime EditedAt { get; set; }
    }
}
