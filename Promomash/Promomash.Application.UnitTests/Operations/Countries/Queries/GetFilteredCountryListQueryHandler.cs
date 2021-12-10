using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Shouldly;

using Xunit;

using Promomash.Application.UnitTests.Common;
using Promomash.Demo.App.Operations.Countries.Queries;
using Promomash.Demo.Common.Interfaces;

namespace Promomash.Application.Tests.Operations.Countries.Queries
{
    /// <summary>
    /// GetFilteredCountryListQuery tests
    /// </summary>
    [Collection("QueryCollection")]
    public class GetFilteredCountryListQueryHandlerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fixture">Query tests fixture</param>
        public GetFilteredCountryListQueryHandlerTests(QueryTestFixture fixture)
        {
            _unitOfWork = fixture.UnitOfWork;
            _mapper = fixture.Mapper;
        }

        /// <summary>
        /// Should return one page of countries with two items
        /// </summary>        
        [Fact]
        public async Task Handle_GivenPage1PageSize100_GetFilteredCountryList()
        {
            // Arrange
            var sut = new GetFilteredCountryListQueryHandler(_unitOfWork, _mapper);
            // Act
            var result = await sut.Handle(new GetFilteredCountryListQuery { Page = 1, PageSize = 100 }, CancellationToken.None);
            // Assert
            result.ShouldBeOfType<CountryListVm>();
            result.PagedList.Items.Count.ShouldBe(2);
            result.PagedList.CurrentPage.ShouldBe(1);
            result.PagedList.FirstRowOnPage.ShouldBe(1);
            result.PagedList.LastRowOnPage.ShouldBe(2);
            result.PagedList.PageCount.ShouldBe(1);
            result.PagedList.PageSize.ShouldBe(100);
            result.PagedList.RowCount.ShouldBe(2);
        }

        /// <summary>
        /// Should return two pages of countries with one item
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Handle_GivenPage2PageSize1_GetCountryList()
        {
            // Arrange
            var sut = new GetFilteredCountryListQueryHandler(_unitOfWork, _mapper);
            // Act
            var result = await sut.Handle(new GetFilteredCountryListQuery { Page = 1, PageSize = 1 }, CancellationToken.None);
            // Assert
            result.ShouldBeOfType<CountryListVm>();
            result.PagedList.Items.Count.ShouldBe(1);
            result.PagedList.CurrentPage.ShouldBe(1);
            result.PagedList.FirstRowOnPage.ShouldBe(1);
            result.PagedList.LastRowOnPage.ShouldBe(1);
            result.PagedList.PageCount.ShouldBe(2);
            result.PagedList.PageSize.ShouldBe(1);
            result.PagedList.RowCount.ShouldBe(2);
        }

        /// <summary>
        /// Should return one country whose name contains "2"
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Handle_GivenOneFilteredItem_GetCountryList()
        {
            // Arrange
            var sut = new GetFilteredCountryListQueryHandler(_unitOfWork, _mapper);
            // Act
            var result = await sut.Handle(new GetFilteredCountryListQuery {Title="2", Page = 1, PageSize = 20 }, CancellationToken.None);
            // Assert
            result.ShouldBeOfType<CountryListVm>();
            result.PagedList.Items.Count.ShouldBe(1);
            result.PagedList.CurrentPage.ShouldBe(1);
            result.PagedList.FirstRowOnPage.ShouldBe(1);
            result.PagedList.LastRowOnPage.ShouldBe(1);
            result.PagedList.PageCount.ShouldBe(1);
            result.PagedList.PageSize.ShouldBe(20);
            result.PagedList.RowCount.ShouldBe(1);
        }
    }
}
