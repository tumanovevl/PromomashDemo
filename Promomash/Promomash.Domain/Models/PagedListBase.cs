using System;

namespace Promomash.Demo.Common.Models
{
    /// <summary>
    /// PagedList base class
    /// </summary>
    public abstract class PagedListBase
    {
        /// <summary>
        /// One-based index of this subset within the superset.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Total number of subsets within the superset.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Maximum size any individual subset.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Total number of objects contained within the superset.
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// One-based index of the first item in the paged subset.
        /// </summary>
        public int FirstRowOnPage
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        /// <summary>
        /// One-based index of the last item in the paged subset.
        /// </summary>
        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }
}
