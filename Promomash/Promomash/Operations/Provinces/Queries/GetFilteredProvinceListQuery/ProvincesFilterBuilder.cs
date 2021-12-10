using System;
using System.Linq.Expressions;

using Promomash.Common.Utils;
using Promomash.Demo.Common.Entities;

namespace Promomash.Demo.App.Operations.Provinces.Queries
{
    /// <summary>
    /// Class wich builds a filter for provinces by query params
    /// </summary>
    public class ProvincesFilterBuilder
    {
        /// <summary>
        /// Creates LINQ filter for province entities
        /// </summary>
        /// <param name="query">An object that contains filtering options for the user entity</param>
        /// <returns>Returns a composite filter from a set of basic filters by condition "And"</returns>
        public static Expression<Func<Province, bool>> Create(GetFilteredProvinceListQuery query)
        {
            var predicate = PredicateBuilder.True<Province>();

            predicate = predicate
                .And(WithTitle(query.Title))
                .And(WithCountry(query.CountryId));

            return predicate;
        }

        private static Expression<Func<Province, bool>> WithTitle(string title)
        {
            var predicate = PredicateBuilder.True<Province>();
            
            if (!string.IsNullOrEmpty(title))
            {
                predicate = predicate.And(p => p.Title.Contains(title));
            }

            return predicate;
        }

        private static Expression<Func<Province, bool>> WithCountry(long? countryId)
        {
            var predicate = PredicateBuilder.True<Province>();
            
            if (countryId != null)
            {
                predicate = predicate.And(p => p.CountryId == countryId);
            }

            return predicate;
        }
    }
}
