using System;

namespace Promomash.Demo.App.Common.Exceptions
{
    /// <summary>
    /// Bad request exception of application layer
    /// </summary>
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        public BadRequestException(string message)
            : base(message)
        {

        }
    }
}
