using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using Promomash.Demo.Common.Models;

namespace Promomash.Infra.Extensions
{
    /// <summary>
    /// Extensions for IQueryable
    /// </summary>
    public static class PagedQueryExtensions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class that divides the supplied superset into subsets the size of the supplied pageSize. The instance then only containes the objects contained in the subset specified by index.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="query">The collection of objects to be divided into subsets. If the collection implements <see cref="IOrderedQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="page">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>       
        /// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one.</exception>
        public static PagedList<T> GetPaged<T>(
            this IQueryable<T> query,
            int page,
            int pageSize
            ) where T : class
        {

            if (page < 1)
            {
                throw new ArgumentOutOfRangeException($"Page = {page}. Page cannot be below 1.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException($"PageSize = {pageSize}. PageSize cannot be less than 1.");
            }

            var result = new PagedList<T>();

            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class that divides the supplied superset into subsets the size of the supplied pageSize. The instance then only containes the objects contained in the subset specified by index.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain.</typeparam>
        /// <param name="query">The collection of objects to be divided into subsets. If the collection implements <see cref="IOrderedQueryable{T}"/>, it will be treated as such.</param>
        /// <param name="page">The one-based index of the subset of objects to be contained by this instance.</param>
        /// <param name="pageSize">The maximum size of any individual subset.</param>       
        /// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one.</exception>
        public static PagedList<T> GetPaged<T>(
            this IEnumerable<T> items,
            int page,
            int pageSize
            ) where T : class
        {

            if (page < 1)
            {
                throw new ArgumentOutOfRangeException($"Page = {page}. Page cannot be below 1.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException($"PageSize = {pageSize}. PageSize cannot be less than 1.");
            }

            var result = new PagedList<T>();

            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = items.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Items = items.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}"/> class that divides the supplied superset into subsets the size of the supplied pageSize async. The instance then only containes the objects contained in the subset specified by index.
        /// </summary>
        /// <typeparam name="T">The type of object the collection should contain</typeparam>
        /// <param name="query">The collection of objects to be divided into subsets. If the collection implements <see cref="IOrderedQueryable{T}"/>, it will be treated as such</param>
        /// <param name="page">The one-based index of the subset of objects to be contained by this instance</param>
        /// <param name="pageSize">The maximum size of any individual subset</param>       
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete</param>
        /// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero</exception>
        /// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one</exception>        
        public async static Task<PagedList<T>> GetPagedAsync<T>(
            this IQueryable<T> query,
            int page,
            int pageSize,
            CancellationToken cancellationToken
            ) where T : class
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException($"Page = {page}. Page cannot be below 1.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException($"PageSize = {pageSize}. PageSize cannot be less than 1.");
            }

            var result = new PagedList<T>();

            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = await query.CountAsync(cancellationToken);

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Items = await query.Skip(skip).Take(pageSize).ToListAsync(cancellationToken);

            return result;
        }
    }
}
