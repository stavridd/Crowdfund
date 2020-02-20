using System;
using System.Linq;

using Xunit;
using Autofac;

using Crowdfund.Core.Services;
using Crowdfund.Core.Model.Options;
using System.Threading.Tasks;

namespace Crowdfund.Test {
    public partial class BuyerServiceTests
                    : IClassFixture<CrowdfundFixture> {

        private readonly IBuyerService bsvc_;

        public BuyerServiceTests(CrowdfundFixture fixture)
        {
            bsvc_ = fixture.Container.Resolve<IBuyerService>();
        }

        [Fact]
        public async Task CreateBuyerSuccess()
        {
            var ran =  DateTime.Now.Second;
            var option = new CreateBuyerOptions()
            {
                FirstName = $"Alex{DateTime.Now.Second}",
                LastName = "Athanasiou",
                Email = $"alejandro{ran}@gmail.com",
                Age = 58
            };

            var buyer = await bsvc_.CreateBuyerAsync(option);

            Assert.NotNull(buyer);

            var search = await bsvc_.SearchBuyerAsync(
                new SearchBuyerOptions()
                {
                    Email = $"alejandro{ran}@gmail.com"
                }
                ).ToList();

            Assert.NotNull(search);
            Assert.Equal(option.FirstName, buyer.FirstName);
            Assert.Equal(option.LastName, buyer.LastName);
            Assert.Equal(option.Email, buyer.Email);
        }

        [Fact]
        public async Task CreateBuyerFail_EmailIsNotUnique()
        {
            var option = new CreateBuyerOptions()
            {
                FirstName = $"Alex{DateTime.Now.Second}",
                LastName = "Athanasiou",
                Email = "alejandro@gmail.com"
            };

            var buyer = await bsvc_.CreateBuyerAsync(option);

            Assert.NotNull(buyer);

            var search = await bsvc_.SearchBuyerAsync(
                new SearchBuyerOptions()
                {
                    Email = "alejandro@gmail.com"
                }
                ).ToListAsync();

            Assert.NotNull(search);
            Assert.Equal(option.FirstName, buyer.FirstName);
            Assert.Equal(option.LastName, buyer.LastName);
            Assert.Equal(option.Email, buyer.Email);
        }

        [Fact]
        public async Task CreateBuyerFail_AgeAsync()
        {
            var option = new CreateBuyerOptions()
            {
                FirstName = $"Alex{DateTime.Now.Second}",
                LastName = "Athanasiou",
                Email = $"alejandro{DateTime.Now.Second}@gmail.com",
                Age = 10
            };

            var buyer = await bsvc_.CreateBuyerAsync(option);

            Assert.NotNull(buyer);
        }

        [Fact]
        public async Task SearchBuyerById_Success()
        {
            var search = await bsvc_.SearchBuyerAsync(
                new SearchBuyerOptions()
                {
                    Email = "alejandro@gmail.com"
                }
                ).SingleOrDefaultAsync();

            Assert.NotNull(search);

            var idSearch = await bsvc_.SearchBuyerByIdAsync(search.Id);
            Assert.Equal(search.Email, idSearch.Email);
        }

        [Fact]
        public async Task UpdateBuyer_Success()
        {
            var option = new UpdateBuyerOptions()
            {
                Age = 35
            };

            var isUpdated = await bsvc_.UpdateBuyerAsync(1, option);

            Assert.True(isUpdated);
        }
    }
}
