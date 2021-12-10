using System.Threading;
using System.Threading.Tasks;

using Shouldly;

using Xunit;

using AutoMapper;

using Promomash.Demo.Common.Interfaces;
using Promomash.Application.UnitTests.Common;
using Promomash.Demo.App.Operations.Users.Queries;
using Promomash.Demo.App.Common.Exceptions;

namespace Promomash.Application.UnitTests.Operations.Users
{
    /// <summary>
    /// GetUserDetailQuery tests
    /// </summary>
    [Collection("QueryCollection")]
    public class GetUserDetailQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fixture">Query tests fixture</param>
        public GetUserDetailQueryHandlerTests(QueryTestFixture fixture)
        {
            _unitOfWork = fixture.UnitOfWork;
            _mapper = fixture.Mapper;
        }

        /// <summary>
        /// Should return UserVm when given a valid user id
        /// </summary>
        [Fact]
        public async Task Handle_GivenValidId_GetUserDetail()
        {
            // Arrange
            long validId = 1;
            var sut = new GetUserDetailQueryHandler(_unitOfWork, _mapper);
            // Act
            var result = await sut.Handle(new GetUserDetailQuery { Id = validId }, CancellationToken.None);
            // Assert
            result.ShouldBeOfType<UserVm>();
            result.Id.ShouldBe(1);
        }

        /// <summary>
        /// Should throws NotFoundException when given an invalid user id
        /// </summary>        
        [Fact]
        public async Task Handle_GivenInvalidId_GetUserDetail_ThrowsNotFoundException()
        {
            // Arrange
            long invalidId = -1;
            var sut = new GetUserDetailQueryHandler(_unitOfWork, _mapper);
            var command = new GetUserDetailQuery { Id = invalidId };
            // Act and Assert
            await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(command, CancellationToken.None));
        }
    }
}