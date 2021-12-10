using System;
using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

namespace Promomash.Demo.App.Common.Exceptions
{
    /// <summary>
    /// Validation exception of application layer
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Constructor with failures
        /// </summary>
        /// <param name="failures">List of validation failures</param>
        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        /// <summary>
        /// Dictionary of validation failures
        /// </summary>
        public IDictionary<string, string[]> Failures { get; }
    }
}
