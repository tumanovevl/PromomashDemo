using System;

using Promomash.Common.Interfaces;

namespace Promomash.Demo.Common.Entities
{
    public class Country: IEntity, IAuditable, ISoftDeletable
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Date and time when this entity was created (in UTC)
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time when this entity was updated
        /// </summary>
        public DateTime EditedAt { get; set; }

        /// <summary>
        /// Is reason soft-deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
