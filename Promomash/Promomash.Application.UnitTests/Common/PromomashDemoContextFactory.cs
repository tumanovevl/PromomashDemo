using System;

using Microsoft.EntityFrameworkCore;

using Promomash.Common.Interfaces;
using Promomash.Demo.Infra.Context;

namespace Promomash.Application.UnitTests.Common
{
    /// <summary>
    /// PromomashDemoContext factory 
    /// </summary>
    public class PromomashDemoContextFactory
    {
        /// <summary>
        /// Creates InMemory DB context
        /// </summary>
        /// <returns>Returns new instance of PromomashDemoContext</returns>
        public static PromomashDemoContext Create(IDateTime dateTime)
        {
            var options = new DbContextOptionsBuilder<PromomashDemoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new PromomashDemoContext(options, dateTime, true);

            context.Database.EnsureCreated();

            return context;
        }

        /// <summary>
        /// Remove InMemory database and dispose DB context
        /// </summary>
        /// <param name="context">PromomashDemoContext</param>
        public static void Destroy(PromomashDemoContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}