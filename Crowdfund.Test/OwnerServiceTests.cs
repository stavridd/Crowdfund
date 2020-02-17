using System;
using System.Linq;

using Xunit;
using Autofac;

using Crowdfund.Core.Services;
using Crowdfund.Core.Model.Options;

namespace Crowdfund.Test {
    public partial class OwnerServiceTests
                    : IClassFixture<CrowdfundFixture> {
        private readonly IOwnerService osvc_;

        public OwnerServiceTests(CrowdfundFixture fixture)
        {
            osvc_ = fixture.Container.Resolve<IOwnerService>();
        }

        [Fact]
        public void CreateOwnerSuccess()
        {
            var ran = DateTime.Now.Second;
            var option = new CreateOwnerOptions()
            {
                FirstName = $"Dimitris{DateTime.Now.Second}",
                LastName = "Stavridis",
                Email = $"stavriddim{ran}@gmail.com",
                Age = 35
            };

            var owner = osvc_.CreateOwner(option);

            Assert.NotNull(owner);

            var search = osvc_.SearchOwner(
                new SearchOwnerOptions()
                {
                    Email = $"stavriddim{ran}@gmail.com"
                }
                ).ToList();

            Assert.NotNull(search);
            Assert.Equal(option.FirstName, owner.FirstName);
            Assert.Equal(option.LastName, owner.LastName);
            Assert.Equal(option.Email, owner.Email);
        }

        [Fact]
        public void CreateOwnerFail_EmailIsNotUnique()
        {
            var option = new CreateOwnerOptions()
            {
                FirstName = $"Dimitris{DateTime.Now.Second}",
                LastName = "Stavridis",
                Email = "stavriddim@gmail.com"
            };

            var owner = osvc_.CreateOwner(option);

            Assert.NotNull(owner);

            var search = osvc_.SearchOwner(
                new SearchOwnerOptions()
                {
                    Email = "stavriddim@gmail.com"
                }
                ).ToList();

            Assert.NotNull(search);
            Assert.Equal(option.FirstName, owner.FirstName);
            Assert.Equal(option.LastName, owner.LastName);
            Assert.Equal(option.Email, owner.Email);
        }

        [Fact]
        public void CreateOwnerFail_Age()
        {
            var option = new CreateOwnerOptions()
            {
                FirstName = $"Alex{DateTime.Now.Second}",
                LastName = "Athanasiou",
                Email = $"alejandro{DateTime.Now.Second}@gmail.com",
                Age = 10
            };

            var owner = osvc_.CreateOwner(option);

            Assert.NotNull(owner);
        }

        [Fact]
        public void SearchOwnerById_Success()
        {
            var search = osvc_.SearchOwner(
                new SearchOwnerOptions()
                {
                    Email = "stavriddim@gmail.com"
                }
                ).SingleOrDefault();

            Assert.NotNull(search);

            var idSearch = osvc_.SearchOwnerById(search.Id);
            Assert.Equal(search.Email, idSearch.Email);
        }

        [Fact]
        public void UpdateOwner_Success()
        {
            var option = new UpdateOwnerOptions()
            {
                FirstName = $"Dimitris{DateTime.Now.Second}",                             
            };

            var isUpdated = osvc_.UpdateOwner(3, option);

            Assert.True(isUpdated);
        }
    }
}