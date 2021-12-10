using System.Threading;

using Xunit;

using Shouldly;

using Promomash.Application.UnitTests.Common;
using Promomash.Demo.App.Operations.Users.Commands;
using Promomash.Demo.Common.Entities;

namespace Promomash.Application.UnitTests.Operations.Users
{
    /// <summary>
    /// Tests for CreateUserCommand
    /// </summary>
    public class CreateUserCommandTests : CommandTestBase
    {
        private readonly CreateUserCommandHandler _sut;

        /// <summary>
        /// Constructor
        /// </summary>
        public CreateUserCommandTests()
            : base()
        {
            _sut = new CreateUserCommandHandler(_unitOfWork);
        }

        /// <summary>
        /// Should create a new user when given a valid request
        /// </summary>
        [Fact]
        public async void Handle_GivenValidRequest_CreateUser()
        {
            // Arrange
            var newUserLogin = "user2@mail.com";
            var newUserPassword = "P@ssw0rd";
            var newUserCounryId = 1;
            var newUserProvinceId = 1;
            // Act
            var result = await _sut.Handle(
                new CreateUserCommand
                {
                    Login = newUserLogin,
                    Password = newUserPassword,
                    CountryId = newUserCounryId,
                    ProvinceId = newUserProvinceId
                }
                , CancellationToken.None);
            User createdUser = await _unitOfWork.UserRepository.FindOneAsync(x => x.Id == result);
            // Assert
            createdUser.ShouldNotBeNull();
            createdUser.Login.ShouldBe(newUserLogin);
            createdUser.CountryId.ShouldBe(newUserCounryId);
            createdUser.ProvinceId.ShouldBe(newUserProvinceId);

            BCrypt.Net.BCrypt.Verify(newUserPassword, createdUser.Password).ShouldBe(true);
        }
    }
}