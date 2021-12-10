using System;

using Promomash.Common.Interfaces;

namespace Promomash.Demo.Common.Entities
{
    public class User : IEntity, IAuditable, ISoftDeletable
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User's country id
        /// </summary>
        public long CountryId { get; set; }

        /// <summary>
        /// User's province id
        /// </summary>
        public long ProvinceId { get; set; }

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

        /// <summary>
        /// User's country
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// User's province
        /// </summary>
        public Province Province { get; set; }
    }
}
