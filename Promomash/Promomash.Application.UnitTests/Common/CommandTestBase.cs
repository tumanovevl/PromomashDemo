using System;

using Promomash.Application.Tests.Common;
using Promomash.Demo.Common.Interfaces;
using Promomash.Demo.Infra.Context;

namespace Promomash.Application.UnitTests.Common
{
    /// <summary>
    /// Base class to perform test on commands
    /// </summary>
    public class CommandTestBase : MocksBase, IDisposable
    {
        private readonly PromomashDemoContext _context;
        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor which initialize a DB connext and UOW
        /// </summary>
        public CommandTestBase()
        {
            _context = PromomashDemoContextFactory.Create(base.DateTimeMock.Object);
            _unitOfWork = UnitOfWorkFactory.Create(_context);
        }

        /// <summary>
        /// Destructor which release a DB context
        /// </summary>
        public void Dispose()
        {
            PromomashDemoContextFactory.Destroy(_context);
        }
    }
}