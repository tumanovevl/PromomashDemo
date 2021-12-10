using System;

namespace Promomash.Demo.App.Common.Exceptions
{
    /// <summary>
    /// Not  found exception of application layer
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Entity type name</param>
        /// <param name="key">Entity key field</param>
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found")
        {

        }
    }
}
