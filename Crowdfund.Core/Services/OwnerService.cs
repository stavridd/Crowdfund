using System;
using System.Linq;
using Crowdfund.Core.Model;
using Crowdfund.Core.Model.Options;

namespace Crowdfund.Core.Services {
    public class OwnerService : IOwnerService 
    {
        private readonly Data.CrowdfundDbContext context_;

        public OwnerService(Data.CrowdfundDbContext context)
        {
            context_ = context ??
                throw new ArgumentException(nameof(context));
        }

        public Owner CreateOwner(
            Model.Options.CreateOwnerOptions options)
        {
            if (options == null) {
                return null;
            }

            if (string.IsNullOrWhiteSpace(options.FirstName) ||
              string.IsNullOrWhiteSpace(options.LastName)) {
                return null;
            }

            if (options.Age == 0 ||
                    options.Age < 18) {
                return null;
            }

            var exists = SearchOwner(
                new SearchOwnerOptions()
                {
                    Email = options.Email,            
                }).SingleOrDefault();

            if (exists != null) {
                return null;
            }

            var owner = new Owner()
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                Email = options.Email,
                Age = options.Age
            };

            context_.Add(owner);
            try {
                context_.SaveChanges();
            } catch (Exception ex) {
                
                return null;
            }

            return owner;
        }

       public IQueryable<Owner> SearchOwner(
            Model.Options.SearchOwnerOptions options)
        {
            if (options == null) {
                return null;
            }

            var query = context_
                .Set<Owner>()
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

        public Owner SearchOwnerById(int ownerId)
        {
            if (ownerId <= 0) {
                return null;
            }

            return context_
                .Set<Owner>()
                .SingleOrDefault(s => s.Id == ownerId);
        }

        public bool UpdateOwner(int id, UpdateOwnerOptions options)
        {
            if (id <= 0) {
                return false;
            }

            if (options == null) {
                return false;
            }

            var owner = SearchOwnerById(id);

            if(!string.IsNullOrWhiteSpace(options.FirstName)) 
            {
                owner.FirstName = options.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(options.LastName)) {
                owner.LastName = options.LastName;
            }

            if (!string.IsNullOrWhiteSpace(options.Photo)) {
                owner.Photo = options.Photo;
            }

            if (options.Age != 0) {
                owner.Age = options.Age;
            }

            //context_.Update(owner);
            var success = false;
            try {
                success = context_.SaveChanges() > 0;
            } catch (Exception ex) {

            }
            return success;
        }

        public bool AddReward (int ownerId, Reward reward)
        {
            if (reward == null) {
                return false;
            }

            var owner = SearchOwnerById(ownerId);
            owner.Rewards.Add(reward);
            return true; 
        }
    }
}
