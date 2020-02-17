using System;
using System.Linq;
using Crowdfund.Core.Model;
using Crowdfund.Core.Model.Options;

namespace Crowdfund.Core.Services {
    public class BuyerService : IBuyerService 
    {
        private readonly Data.CrowdfundDbContext context_;

        public BuyerService(Data.CrowdfundDbContext context)
        {
            context_ = context ??
                throw new ArgumentException(nameof(context));
        }

        public Buyer CreateBuyer(
            Model.Options.CreateBuyerOptions options)
        {
            if (options == null) {
                return null;
            }

            if (string.IsNullOrWhiteSpace(options.FirstName) ||
              string.IsNullOrWhiteSpace(options.LastName)) {
                return null;
            }

            if(options.Age == 0 ||
                    options.Age < 18) {
                return null;
            }

            var exists = SearchBuyer(
                new SearchBuyerOptions()
                {
                    Email = options.Email,            
                }).SingleOrDefault();

            if (exists != null) {
                return null;
            }

            var buyer = new Buyer()
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                Email = options.Email,
                Age = options.Age
            };

            context_.Add(buyer);
            try {
                context_.SaveChanges();
            } catch (Exception ex) {
                
                return null;
            }

            return buyer;
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

        public Buyer SearchBuyerById(int buyerId)
        {
            if (buyerId <= 0) {
                return null;
            }

            return context_
                .Set<Buyer>()
                .SingleOrDefault(s => s.Id == buyerId);
        }

        public bool UpdateBuyer(int id, UpdateBuyerOptions options)
        {
            if (id <= 0) {
                return false;
            }

            if (options == null) {
                return false;
            }

            var buyer = SearchBuyerById(id);

            if(!string.IsNullOrWhiteSpace(options.FirstName)) 
            {
                buyer.FirstName = options.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(options.LastName)) {
                buyer.LastName = options.LastName;
            }

            if (!string.IsNullOrWhiteSpace(options.Photo)) {
                buyer.Photo = options.Photo;
            }

            if (options.Age != 0) {
                buyer.Age = options.Age;
            }

            //context_.Update(buyer);
            var success = false;
            try {
                success = context_.SaveChanges() > 0;
            } catch (Exception ex) {

            }
            return success;
        }
    }
}
