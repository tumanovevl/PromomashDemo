using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Promomash.Common.Interfaces;
using Promomash.Demo.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Promomash.Demo.Infra.Context
{
    public class DemoDataGenerator
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PromomashDemoContext(
                serviceProvider.GetRequiredService<DbContextOptions<PromomashDemoContext>>(),
                serviceProvider.GetService<IDateTime>(),
                true))
            {
                var uow = new UnitOfWork(context);

                var countryCount = await uow.CountryRepository.GetAll().CountAsync();
                if (countryCount > 0)
                {
                    return;
                }

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

                uow.Commit();
            }

        }
    }
}
