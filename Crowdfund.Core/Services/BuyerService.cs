using System;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Model;
using Crowdfund.Core.Model.Options;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Core.Services {
    public class BuyerService : IBuyerService 
    {
        private readonly Data.CrowdfundDbContext context_;

        public BuyerService(Data.CrowdfundDbContext context)
        {
            context_ = context ??
                throw new ArgumentException(nameof(context));
        }

        public async Task<ApiResult<Buyer>> CreateBuyerAsync(
            Model.Options.CreateBuyerOptions options)
        {
            if (options == null) {
                return new ApiResult<Buyer>(
                    StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.FirstName) ||
              string.IsNullOrWhiteSpace(options.LastName)) {
                return new ApiResult<Buyer>(
                   StatusCode.BadRequest, "Null FirstName Or LastName");
            }

            if (options.Age == 0 ||
                    options.Age < 18) {
                return new ApiResult<Buyer>(
                   StatusCode.BadRequest, "Age is Invalid");
            }

            var exists = await SearchBuyer(
                new SearchBuyerOptions()
                {
                    Email = options.Email,            
                }).SingleOrDefaultAsync();

            if (exists != null) {
                return new ApiResult<Buyer>(
                   StatusCode.InternalServerError, "Backer is already exist");
            }

            var buyer = new Buyer()
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                Email = options.Email,
                Age = options.Age
            };

            await context_.AddAsync(buyer);
            try {
               await context_.SaveChangesAsync();
            } catch (Exception ex) {
                
                return null;
            }

            return ApiResult<Buyer>.CreateSuccess(buyer);
        }

       public IQueryable<Buyer> SearchBuyer(
            Model.Options.SearchBuyerOptions options)
        {
            if (options == null) {
                return null;
            }

            var query = context_
                .Set<Buyer>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.FirstName)) {
                query = query.Where(c =>
                    c.FirstName == options.FirstName);
            }

            if (!string.IsNullOrWhiteSpace(options.LastName)) {
                query = query.Where(c =>
                    c.LastName == options.LastName);
            }

            if (!string.IsNullOrWhiteSpace(options.Email)) {
                query = query.Where(c =>
                    c.Email == options.Email);
            }

            return query.Take(500);
        }

        public async Task<ApiResult<Buyer>> SearchBuyerByIdAsync(int buyerId)
        {
            if (buyerId <= 0) {
                return new ApiResult<Buyer>(
                    StatusCode.BadRequest, "Invalid Id");
            }

            var buyer = await context_
                .Set<Buyer>()
                .SingleOrDefaultAsync(s => s.Id == buyerId);

            if (buyer == null ) {
                return new ApiResult<Buyer>(
                    StatusCode.NotFound, "Backer doesn't exist");
            }

            return ApiResult<Buyer>.CreateSuccess(buyer);
        }

        public async Task<ApiResult<Buyer>> UpdateBuyerAsync(int id, UpdateBuyerOptions options)
        {
            if (id <= 0) {
                return new ApiResult<Buyer>(
                    StatusCode.BadRequest, "Invalid Id");
            }

            if (options == null) {
                return new ApiResult<Buyer>(
                    StatusCode.BadRequest, "Invalid Options");
            }

            var buyer = await SearchBuyerByIdAsync(id);

            if(!string.IsNullOrWhiteSpace(options.FirstName)) 
            {
                buyer.Data.FirstName = options.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(options.LastName)) {
                buyer.Data.LastName = options.LastName;
            }

            if (!string.IsNullOrWhiteSpace(options.Photo)) {
                buyer.Data.Photo = options.Photo;
            }

            if (options.Age != 0) {
                buyer.Data.Age = options.Age;
            }

            var success = false;
            try {
                success = await context_.SaveChangesAsync() > 0;
            } catch (Exception ex) {

            }
            return ApiResult<Buyer>.CreateSuccess(buyer.Data);
        }
    }
}
