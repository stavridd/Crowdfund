using System;
using System.Linq;

using Xunit;
using Autofac;

using Crowdfund.Core.Services;
using Crowdfund.Core.Model.Options;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

            var search = bsvc_.SearchBuyer(
                new SearchBuyerOptions()
                {
                    Email = $"alejandro{ran}@gmail.com"
                }
                ).ToList();

            Assert.NotNull(search);
            Assert.Equal(option.FirstName, buyer.Data.FirstName);
            Assert.Equal(option.LastName, buyer.Data.LastName);
            Assert.Equal(option.Email, buyer.Data.Email);
        }

        [Fact]
        public async Task CreateBuyerFail_EmailIsNotUnique()
        {
            Random random = new Random();
            var num = random.Next(1, 1000);
            var option = new CreateBuyerOptions()
            {
                FirstName = $"Alex{DateTime.Now.Second}",
                LastName = "Athanasiou",
                Email = $"alejandro{num}@gmail.com",
                Age = 19
            };

            var buyer = await bsvc_.CreateBuyerAsync(option);

            Assert.NotNull(buyer);

            var option2 = new CreateBuyerOptions()
            {
                FirstName = $"Alex{DateTime.Now.Second}",
                LastName = "Athanassiou",
                Email = $"alejandro{num}@gmail.com",
                Age = 19
            };

            var buyer2 = await bsvc_.CreateBuyerAsync(option2);

            Assert.Null(buyer2.Data);

           
            
        }

        [Fact]
        public async Task CreateBuyerFail_Age()
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
            var search = await bsvc_.SearchBuyer(
                new SearchBuyerOptions()
                {
                    Email = "alejandro43@gmail.com"
                }
                ).SingleOrDefaultAsync();

            Assert.NotNull(search);

            var idSearch = await bsvc_.SearchBuyerByIdAsync(search.Id);
            Assert.Equal(search.Email, idSearch.Data.Email);
        }

        [Fact]
        public async Task UpdateBuyer_Success()
        {
            var option = new UpdateBuyerOptions()
            {
                Age = 35
            };

            var isUpdated = await bsvc_.UpdateBuyerAsync(1, option);

            Assert.Equal(option.Age, isUpdated.Data.Age);
        }
    }
}
