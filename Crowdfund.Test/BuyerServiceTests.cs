using System;
using System.Linq;

using Xunit;
using Autofac;

using Crowdfund.Core.Services;
using Crowdfund.Core.Model.Options;


namespace Crowdfund.Test {
    public partial class BuyerServiceTests
                    : IClassFixture<CrowdfundFixture> {

        private readonly IBuyerService bsvc_;

        public BuyerServiceTests(CrowdfundFixture fixture)
        {
            bsvc_ = fixture.Container.Resolve<IBuyerService>();
        }

        [Fact]
        public void CreateBuyerSuccess()
        {
            var ran =  DateTime.Now.Second;
            var option = new CreateBuyerOptions()
            {
                FirstName = $"Alex{DateTime.Now.Second}",
                LastName = "Athanasiou",
                Email = $"alejandro{ran}@gmail.com",
                Age = 58
            };

            var buyer = bsvc_.CreateBuyer(option);

            Assert.NotNull(buyer);

            var search = bsvc_.SearchBuyer(
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
        public void CreateBuyerFail_EmailIsNotUnique()
        {
            var option = new CreateBuyerOptions()
            {
                FirstName = $"Alex{DateTime.Now.Second}",
                LastName = "Athanasiou",
                Email = "alejandro@gmail.com"
            };

            var buyer = bsvc_.CreateBuyer(option);

            Assert.NotNull(buyer);

            var search = bsvc_.SearchBuyer(
                new SearchBuyerOptions()
                {
                    Email = "alejandro@gmail.com"
                }
                ).ToList();

            Assert.NotNull(search);
            Assert.Equal(option.FirstName, buyer.FirstName);
            Assert.Equal(option.LastName, buyer.LastName);
            Assert.Equal(option.Email, buyer.Email);
        }

        [Fact]
        public void CreateBuyerFail_Age()
        {
            var option = new CreateBuyerOptions()
            {
                FirstName = $"Alex{DateTime.Now.Second}",
                LastName = "Athanasiou",
                Email = $"alejandro{DateTime.Now.Second}@gmail.com",
                Age = 10
            };

            var buyer = bsvc_.CreateBuyer(option);

            Assert.NotNull(buyer);
        }

        [Fact]
        public void SearchBuyerById_Success()
        {
            var search = bsvc_.SearchBuyer(
                new SearchBuyerOptions()
                {
                    Email = "alejandro@gmail.com"
                }
                ).SingleOrDefault();

            Assert.NotNull(search);

            var idSearch = bsvc_.SearchBuyerById(search.Id);
            Assert.Equal(search.Email, idSearch.Email);
        }

        [Fact]
        public void UpdateBuyer_Success()
        {
            var option = new UpdateBuyerOptions()
            {
                Age = 35
            };

            var isUpdated = bsvc_.UpdateBuyer(1, option);

            Assert.True(isUpdated);
        }
    }
}
