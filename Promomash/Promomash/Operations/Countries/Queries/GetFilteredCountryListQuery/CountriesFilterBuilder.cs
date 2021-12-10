using System;
using System.Linq.Expressions;

using Promomash.Common.Utils;
using Promomash.Demo.Common.Entities;

namespace Promomash.Demo.App.Operations.Countries.Queries
{
    /// <summary>
    /// Class wich builds a filter for countries by query params
    /// </summary>
    public class CountriesFilterBuilder
    {
        /// <summary>
        /// Creates LINQ filter for country entities
        /// </summary>
        /// <param name="query">An object that contains filtering options for the user entity</param>
        /// <returns>Returns a composite filter from a set of basic filters by condition "And"</returns>
        public static Expression<Func<Country, bool>> Create(GetFilteredCountryListQuery query)
        {
            var predicate = PredicateBuilder.True<Country>();

            predicate = predicate
                .And(WithTitle(query.Title));

            return predicate;
        }

        private static Expression<Func<Country, bool>> WithTitle(string title)
        {
            var predicate = PredicateBuilder.True<Country>();
            
            if (!string.IsNullOrEmpty(title))
            {
                predicate = predicate.And(p => p.Title.Contains(title));
            }

            return predicate;
        }
    }
}
