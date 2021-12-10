using System;

using Moq;

using Promomash.Common.Interfaces;

namespace Promomash.Application.Tests.Common
{
    /// <summary>
    /// Base mocks class for command and query tests
    /// </summary>
    public class MocksBase
    {
        /// <summary>
        /// Operations date and time
        /// </summary>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// Date time mock
        /// </summary>
        public Mock<IDateTime> DateTimeMock { get; private set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public MocksBase()
        {
            DateTime = new DateTime(3001, 1, 1);
            DateTimeMock = new Mock<IDateTime>();
            DateTimeMock.Setup(m => m.UtcNow).Returns(DateTime);
        }
    }
}