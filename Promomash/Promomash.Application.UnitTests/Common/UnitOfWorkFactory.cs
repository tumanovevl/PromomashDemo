using System;

using Promomash.Demo.Common.Entities;
using Promomash.Demo.Infra.Context;

namespace Promomash.Application.UnitTests.Common
{
    /// <summary>
    /// UnitOfWork factory
    /// </summary>
    public class UnitOfWorkFactory
    {
        /// <summary>
        /// Creates and initialize PromomashDemo UOW
        /// </summary>
        /// <param name="context">DB context</param>
        /// <returns>Returns new instance of PromomashDemo UOW with dummy data</returns>
        public static UnitOfWork Create(PromomashDemoContext context)
        {
            var uow = new UnitOfWork(context);

            uow.CountryRepository.Create(
                new Country { Id = 1, Title = "Country 1" });
            uow.CountryRepository.Create(
                new Country { Id = 2, Title = "Country 2" });

            uow.ProvinceRepository.Create(
                new Province { Id = 1, CountryId = 1, Title = "Province 1.1" });
            uow.ProvinceRepository.Create(
                new Province { Id = 2, CountryId = 1, Title = "Province 1.2" });
            uow.ProvinceRepository.Create(
                new Province { Id = 3, CountryId = 1, Title = "Province 1.3" });
            uow.ProvinceRepository.Create(
                new Province { Id = 4, CountryId = 2, Title = "Province 2.1" });
            uow.ProvinceRepository.Create(
                new Province { Id = 5, CountryId = 2, Title = "Province 2.2" });
            uow.ProvinceRepository.Create(
                new Province { Id = 6, CountryId = 2, Title = "Province 2.3" });

            uow.UserRepository.Create(
                new User
                {
                    Id = 1,
                    Login = "user1@mail.com",
                    Password = "P@ssw0rd",
                    CountryId = 1,
                    ProvinceId = 1
                });

            uow.Commit();

            return uow;
        }
    }
}