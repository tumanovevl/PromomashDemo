using System;

using Promomash.Common.Interfaces;

namespace Promomash.Common.Infra.Common
{
    /// <summary>
    /// IDateTime implementation
    /// </summary>
    public class MachineDateTime : IDateTime
    {
        /// <summary>
        /// Current date and time (in UTC)
        /// </summary>
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
