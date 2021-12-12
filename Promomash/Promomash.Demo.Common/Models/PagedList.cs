using System.Collections.Generic;

namespace Promomash.Demo.Common.Models
{
    /// <summary>
    /// Represents a subset of a collection of objects that can be individually accessed by index
    /// and containing metadata about the superset collection of objects this subset was created from
    /// </summary>    
    /// <typeparam name = "T">The type of object the collection should contain</typeparam>
    public class PagedList<T> : PagedListBase where T : class
    {
        /// <summary>
        /// The subset of items contained only within this one page of the superset.
        /// </summary>
        public IList<T> Items { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PagedList()
        {
            Items = new List<T>();
        }
    }
}
