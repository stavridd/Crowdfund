﻿using System.Linq;
using Crowdfund.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Crowdfund.Core.Services {
    public interface IOwnerService 
    {

        Task<ApiResult<Owner>> CreateOwnerAsync(
            Model.Options.CreateOwnerOptions options);

        IQueryable<Model.Owner> SearchOwner(
            Model.Options.SearchOwnerOptions options);

        Task<ApiResult<Owner>> SearchOwnerByIdAsync(int ownerId);

        Task<ApiResult<Owner>> UpdateOwnerAsync(int id,
            Model.Options.UpdateOwnerOptions options);

        Task<bool> AddRewardAsync(int ownerId, Reward reward);

        Task<ApiResult<ICollection<Project>>> GetMyProjectsAsync(int ownerId);

        Task<bool> IsOwnerAllowedToSeeAsync(int ownerId, Project projectId);
    }
}
