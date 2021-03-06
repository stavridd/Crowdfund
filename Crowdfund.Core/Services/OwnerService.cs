﻿using System;
using System.Linq;
using Crowdfund.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Crowdfund.Core.Model.Options;
using Microsoft.EntityFrameworkCore;


namespace Crowdfund.Core.Services {
    public class OwnerService : IOwnerService 
    {
        private readonly Data.CrowdfundDbContext context_;
        private readonly ILoggerService logger_;

        public OwnerService(Data.CrowdfundDbContext context,
            ILoggerService logger)

        {
            context_ = context ?? throw new ArgumentException(nameof(context));
            logger_ = logger ?? throw new ArgumentException(nameof(logger));
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
            } catch {

                logger_.LogError(StatusCode.InternalServerError,
                    $"Error Save Project Creator: {owner.LastName}");

                return new ApiResult<Owner>
                     (StatusCode.InternalServerError,
                       "Error Save Project Creator");

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

            var success = false;
            try {
                success = await context_.SaveChangesAsync() > 0;
            } catch {

                logger_.LogError(StatusCode.InternalServerError,
                    $"Error Update Project Creator: {owner.Data.LastName}");

                return new ApiResult<Owner>
                     (StatusCode.InternalServerError,
                       "Error Update Project Creator");
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
                return new ApiResult<ICollection<Project>>(StatusCode.BadRequest,
                                "Invalid Id");
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
        
            return false;
        }
    }
}
