using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Model;
using Crowdfund.Core.Model.Options;
using Microsoft.EntityFrameworkCore;


namespace Crowdfund.Core.Services {
    public class OwnerService : IOwnerService 
    {
        private readonly Data.CrowdfundDbContext context_;
        //private readonly IProjectService projects_;

        public OwnerService(Data.CrowdfundDbContext context)
      //      IProjectService projects)
        {
            context_ = context ??
                throw new ArgumentException(nameof(context));
        }

        public async Task<ApiResult<Owner>> CreateOwnerAsync(
            Model.Options.CreateOwnerOptions options)
        {
            if (options == null) {
                return new ApiResult<Owner>(
                    StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.FirstName) ||
              string.IsNullOrWhiteSpace(options.LastName)) {
                return new ApiResult<Owner>(
                   StatusCode.BadRequest, "Null FirstName Or LastName");
            }

            if (options.Age == 0 ||
                    options.Age < 18) {
                return new ApiResult<Owner>(
                    StatusCode.BadRequest, "Age is Invalid");
            }

            var exists = await SearchOwner(
                new SearchOwnerOptions()
                {
                    Email = options.Email,            
                }).SingleOrDefaultAsync();

            if (exists != null) {
                return new ApiResult<Owner>(
                   StatusCode.InternalServerError, "Project Creator is already exist");
            }

            var owner = new Owner()
            {
                FirstName = options.FirstName,
                LastName = options.LastName,
                Email = options.Email,
                Age = options.Age
            };

            await context_.AddAsync(owner);
            try {
                await context_.SaveChangesAsync();
            } catch (Exception ex) {
                
                return null;
            }

            return ApiResult<Owner>.CreateSuccess(owner);
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

        public async Task<ApiResult<Owner>> SearchOwnerByIdAsync(int ownerId)
        {
            if (ownerId <= 0) {
                return new ApiResult<Owner>(
                    StatusCode.BadRequest, "Invalid Id");
            }

            var owner = await context_
                .Set<Owner>()
                .SingleOrDefaultAsync(s => s.Id == ownerId);

            if (owner == null) {
                return new ApiResult<Owner>(
                    StatusCode.NotFound, "Backer doesn't exist");
            }
            return ApiResult<Owner>.CreateSuccess(owner);
        }

        public async Task<ApiResult<Owner>> UpdateOwnerAsync(int id, UpdateOwnerOptions options)
        {
            if (id <= 0) {
                return new ApiResult<Owner>(
                    StatusCode.BadRequest, "Invalid Id");
            }

            if (options == null) {
                return new ApiResult<Owner>(
                    StatusCode.BadRequest, "Invalid Options");
            }

            var owner = await SearchOwnerByIdAsync(id);

            if(!string.IsNullOrWhiteSpace(options.FirstName)) 
            {
                owner.Data.FirstName = options.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(options.LastName)) {
                owner.Data.LastName = options.LastName;
            }

            if (!string.IsNullOrWhiteSpace(options.Photo)) {
                owner.Data.Photo = options.Photo;
            }

            if (options.Age != 0) {
                owner.Data.Age = options.Age;
            }

            //context_.Update(owner);
            var success = false;
            try {
                success = await context_.SaveChangesAsync() > 0;
            } catch (Exception ex) {

            }
            return ApiResult<Owner>.CreateSuccess(owner.Data);
        }

        public async Task<bool> AddRewardAsync (int ownerId, Reward reward)
        {
            if (reward == null) {
                return false;
            }

            var owner = await SearchOwnerByIdAsync(ownerId);
            owner.Data.Rewards.Add(reward);
            return true; 
        }

       public async Task<ApiResult<ICollection<Project>>> GetMyProjectsAsync(int ownerId)
       {
            var owner = await SearchOwnerByIdAsync(ownerId);

            if (owner == null) {
                return new ApiResult<ICollection<Project>>(StatusCode.BadRequest, "Invalid Id");
            }
            return ApiResult<ICollection<Project>>.CreateSuccess(owner.Data.Projects);
    
       }

        public async Task<bool> IsOwnerAllowedToSeeAsync(int ownerId, Project project)
        {
            if (ownerId <= 0) {
                return false;
            }
            if (project == null) {
                return false;
            }
            var owner = await SearchOwnerByIdAsync(ownerId);
            if (owner == null) {
                return false;
            }

            if (project.OwnerId == ownerId) {
                return true;
            }
           
            //foreach(var p in owner.Data.Projects) {
            //    if (p.Id != projectId) {
            //        continue;
            //    }
            //    else {
            //        return true;
            //    }
            //}

            return false;
        }
    }
}
