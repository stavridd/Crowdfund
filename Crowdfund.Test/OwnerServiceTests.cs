using Xunit;
using System;
using Autofac;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Services;
using Crowdfund.Core.Model.Options;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Test {
    public partial class OwnerServiceTests
                    : IClassFixture<CrowdfundFixture> {

        private readonly IOwnerService osvc_;
        private readonly IProjectService psvc_;

        public OwnerServiceTests(CrowdfundFixture fixture)
        {
            osvc_ = fixture.Container.Resolve<IOwnerService>();
            psvc_ = fixture.Container.Resolve<IProjectService>();
        }

        [Fact]
        public async Task CreateOwnerSuccess()
        {
            var ran = DateTime.Now.Second;
            var option = new CreateOwnerOptions()
            {
                FirstName = $"Dimitris{DateTime.Now.Second}",
                LastName = "Stavridis",
                Email = $"stavriddim{ran}@gmail.com",
                Age = 35
            };

            var owner = await osvc_.CreateOwnerAsync(option);

            Assert.NotNull(owner);

            var search = await osvc_.SearchOwner(
                new SearchOwnerOptions()
                {
                    Email = $"stavriddim{ran}@gmail.com"
                }
                ).ToListAsync();

            Assert.NotNull(search);
            Assert.Equal(option.FirstName, owner.Data.FirstName);
            Assert.Equal(option.LastName, owner.Data.LastName);
            Assert.Equal(option.Email, owner.Data.Email);
        }

        [Fact]
        public async Task CreateOwnerFail_EmailIsNotUnique()
        {
            var ran = DateTime.Now.Second;
            var option = new CreateOwnerOptions()
            {
                FirstName = $"Dimitris{DateTime.Now.Second}",
                LastName = "Stavridis",
                Email = $"stavriddim{ran}@gmail.com",
                Age = 80
            };

            var owner = await osvc_.CreateOwnerAsync(option);

            Assert.NotNull(owner);

            var option2 = new CreateOwnerOptions()
            {
                FirstName = $"Dimitris{DateTime.Now.Second}",
                LastName = "Stavridis",
                Email = $"stavriddim{ran}@gmail.com",
                Age = 80
            };

            var owner2 = await osvc_.CreateOwnerAsync(option2);

            Assert.Null(owner2.Data);
        }

        [Fact]
        public async Task CreateOwnerFail_AgeAsync()
        {
            var option = new CreateOwnerOptions()
            {
                FirstName = $"Alex{DateTime.Now.Second}",
                LastName = "Athanasiou",
                Email = $"alejandro{DateTime.Now.Second}@gmail.com",
                Age = 10
            };

            var owner = await osvc_.CreateOwnerAsync(option);

            Assert.NotNull(owner);
        }

        [Fact]
        public async Task SearchOwnerById_SuccessAsync()
        {
            var search = await osvc_.SearchOwner(
                new SearchOwnerOptions()
                {
                    Email = "stavriddim28@gmail.com"
                }
                ).SingleOrDefaultAsync();

            Assert.NotNull(search);

            var idSearch = await osvc_.SearchOwnerByIdAsync(search.Id);
            Assert.Equal(search.Email, idSearch.Data.Email);
        }

        [Fact]
        public async Task UpdateOwner_SuccessAsync()
        {
            var option = new UpdateOwnerOptions()
            {
                FirstName = $"Dimitris77777",                             
            };

            var isUpdated = await osvc_.UpdateOwnerAsync(2, option);

            Assert.Equal(option.FirstName, isUpdated.Data.FirstName);
        }

        [Fact]
        public async Task IsOwnerAlloedToSee()
        {

            var project = await psvc_.SearchProjectByIdAsync(1);
            var IsAllowed = await osvc_.IsOwnerAllowedToSeeAsync(1,project.Data);

            Assert.True(IsAllowed);


        }
    }
}