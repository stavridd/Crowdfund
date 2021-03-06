﻿using System;
using System.Linq;
using System.Text;
using Crowdfund.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Core.Services {
    public class ProjectService : IProjectService
    {
        private readonly Data.CrowdfundDbContext context_;
        private readonly IOwnerService owners_;
        private readonly IRewardService rewards_;
        private readonly ILoggerService logger_;

        public ProjectService(Data.CrowdfundDbContext context,
                  IRewardService reward, IOwnerService owner,
                  ILoggerService logger)
        {
            context_ = context ?? throw new ArgumentException(nameof(context));
            rewards_ = reward ?? throw new ArgumentException(nameof(reward));
            owners_ = owner ?? throw new ArgumentException(nameof(owner));
            logger_ = logger ?? throw new ArgumentException(nameof(logger));
        }

        public async Task <ApiResult<Project>> CreateProjectAsync(int ownerId,
            Model.Options.CreateProjectOptions options)
        {
            if (options == null) {
                return new ApiResult<Project>(
                     StatusCode.BadRequest, "Null options");
            }

            if (ownerId == 0) {
                return new ApiResult<Project>(
                     StatusCode.BadRequest, "Null Project Creator's Id");
            }

            if (string.IsNullOrWhiteSpace(options.Title) ||
              string.IsNullOrWhiteSpace(options.Description))
              {
                return new ApiResult<Project>(
                     StatusCode.BadRequest, "Null Title or Description");
            }

            if (options.projectcategory < 0 )
            {
                return new ApiResult<Project>(
                     StatusCode.BadRequest, " Project Category is Invlaid");
            }

            if (options.Goal <= 0) {
                return new ApiResult<Project>(
                     StatusCode.BadRequest, " Financial Goal Must Be Greater than 0");
            }

            var owner = await owners_.SearchOwnerByIdAsync(ownerId);

            if (owner == null) {
                return new ApiResult<Project>(
                    StatusCode.NotFound, "Project Creator Doesn't Exist");
            }

            // only once every title && description
            var exist = await GetProjectIdAsync(options.Title, options.Description);

            if (exist !=0 ) {
                return new ApiResult<Project>(
                     StatusCode.BadRequest, "Project Already Exists");
            }

            var project = new Project()
            {
                Title = options.Title,
                Description = options.Description,
                projectcategory = options.projectcategory,
                Goal = options.Goal,
                Owner = owner.Data
            };

           owner.Data.Projects.Add(project);

           await context_.AddAsync(project);
            try {
                await context_.SaveChangesAsync();
            } catch {

                logger_.LogError(StatusCode.InternalServerError,
                       $"Error Save Project: {project.Title}");

                return new ApiResult<Project>
                     (StatusCode.InternalServerError,
                       "Error Save Project");
            }

            return ApiResult<Project>.CreateSuccess(project);
        }

        public IQueryable<Project> SearchProject(
            Model.Options.SearchProjectOptions options)
        {
            if (options == null) {
                return null;
            }

            var query = context_
                .Set<Project>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Title)) {
                query = query.Where(c =>
                    c.Title.Contains(options.Title));
            }

            if (options.projectcategory != null) {
                query = query.Where(c =>
                    c.projectcategory == options.projectcategory);
            }

            return query.Take(500);
        }

        public IQueryable<Project> SearchProjectByCstegory(
            ProjectCategory category)
        {          
            var query = context_
                .Set<Project>()
                .AsQueryable();

            if (category !=0) {
                query = query.Where(c =>
                    c.projectcategory == category);
            }
          
            return query.Take(500);
        }

        public async Task<bool> ChangeProjectStatusAsync(int projectid, ProjectStatus Status)
        {
            if (projectid <= 0) {
                return false;
            }

            if (Status <= 0) {
                return false;
            }

            var project = await SearchProjectByIdAsync(projectid);

            if (project == null) {
                return false;
            }

            project.Data.Status = Status;
           

            var success = false;
            try {
                success = await context_.SaveChangesAsync() > 0;
            } catch {
                logger_.LogError(StatusCode.InternalServerError,
                     $"Error Save Project: {project.Data.Title}");

                return false;
            }
            return success;
        }

        public async Task<ApiResult<Project>> SearchProjectByIdAsync(int projectId)
        {
            if (projectId <= 0) {
                return new ApiResult<Project>(
                     StatusCode.BadRequest, " Project Was Not Found");
            }

            var project =  await context_
                .Set<Project>()
                .SingleOrDefaultAsync(s => s.Id == projectId);

            return ApiResult<Project>.CreateSuccess(project);
        }

        public async Task<bool> BuyProjectAsync(int projectId, int buyerId,
            int rewardId)
        {
            if (projectId == 0 ||
                buyerId == 0 ||
                 rewardId == 0) {
                return false;
            }

            var project = await SearchProjectByIdAsync(projectId);

            if (project == null) {
                return false;
            }

            if (project.Data.Status != ProjectStatus.Active) {
                return false;
            }
            project.Data.Buyers.Add(
                new ProjectBuyer()
                {
                    ProjectId = project.Data.Id,
                    BuyerId = buyerId
                });

            var reward = await rewards_.SearchRewardByIdAsync(rewardId);

            if (reward == null) {
                return false;
            }

            reward.Data.Buyers.Add(
                new BuyerReward()
                {
                    RewardId = rewardId,
                    BuyerId = buyerId
                });

            project.Data.Contributions = project.Data.Contributions + reward.Data.Value;

            if(project.Data.Contributions > project.Data.Goal) {
                project.Data.Status = ProjectStatus.Completed;
            }

            try {
                await context_.SaveChangesAsync();
            } catch {

                logger_.LogError(StatusCode.InternalServerError,
                     $"Error Search Project: {project.Data.Title}");

                
                return false;
            }

            return true;
        }

        public async Task<int> GetProjectIdAsync(string title, string Desc)
        {
            if (string.IsNullOrWhiteSpace(title)) {
                return 0;
            }

            if (string.IsNullOrWhiteSpace(Desc)) {
                return 0;
            }

            var query = context_
                .Set<Project>()
                .AsQueryable();

            query = query.Where(c =>
                    c.Title == title);

            query = query.Where(c =>
                    c.Description == Desc);


            var pr = await query.SingleOrDefaultAsync();
            if (pr == null) {
                return 0;
            } else {
                return pr.Id;
            }
        }

        public async Task<bool> AddStatusUpdateAsync(int projectId, string update)
        {
            if (string.IsNullOrWhiteSpace(update)) {
                return false;
            }

            if (projectId <=0) {
                return false;
            }

            var project = await SearchProjectByIdAsync(projectId);

            if (project.Data == null) {
                return false;
            }

            var statusUpdate = new StatusUpdates()
            {
                Id = projectId,
                StatusUpdate = update,
                DatePost = DateTimeOffset.Now
            };

            await context_.AddAsync(statusUpdate);

            var success = false;
            try {
                success = await context_.SaveChangesAsync() > 0;
            } catch{
                logger_.LogError(StatusCode.InternalServerError,
                   $"Error Adding status update of a project: {project.Data.Title}");

                return false;
            }
            return success;

        }

        public async Task<ApiResult<List<StatusUpdates>>> GetStatusUpdateAsync(int projectId)
        {
            if (projectId <= 0) {
                return new ApiResult<List<StatusUpdates>> (
                     StatusCode.BadRequest, "Null project id");
            }

            var project = await SearchProjectByIdAsync(projectId);

            if (project.Data == null) {
                return new ApiResult<List<StatusUpdates>>(
                     StatusCode.NotFound, "No such project");
            }

            var query = context_
                .Set<StatusUpdates>()
                .AsQueryable();

            var statusUp = await query
                .Where(u => u.Id == projectId)
                .ToListAsync();

            return ApiResult<List<StatusUpdates>>.CreateSuccess(statusUp);

        }

        public async Task<ApiResult<List<Multimedia>>> GetProjectPhotoAsync(int projectId) {
            if (projectId <= 0) {
                return new ApiResult<List<Multimedia>>(
                     StatusCode.BadRequest, "Null project id");
            }

            var project = await SearchProjectByIdAsync(projectId);

            if (project.Data == null) {
                return new ApiResult<List<Multimedia>>(
                     StatusCode.NotFound, "No such project");
            }

            var query = context_
                .Set<Multimedia>()
                .AsQueryable();

            var photo = await query
                .Where(m => m.Project == project.Data && m.MultimediaCategory == MultimediaCategory.Photo)
                .ToListAsync();

            return ApiResult<List<Multimedia>>.CreateSuccess(photo);
        }

        public async Task<ApiResult<List<Multimedia>>> GetProjectVideoAsync(int projectId) {
            if (projectId <= 0) {
                return new ApiResult<List<Multimedia>>(
                     StatusCode.BadRequest, "Null project id");
            }

            var project = await SearchProjectByIdAsync(projectId);

            if (project.Data == null) {
                return new ApiResult<List<Multimedia>>(
                     StatusCode.NotFound, "No such project");
            }

            var query = context_
                .Set<Multimedia>()
                .AsQueryable();

            var statusUp = await query
                .Where(m => m.Project == project.Data &&
                m.MultimediaCategory == MultimediaCategory.Video)
                .ToListAsync();

            return ApiResult<List<Multimedia>>.CreateSuccess(statusUp);
        }

        public async Task<ApiResult<Multimedia>> AddMultiAsync(int projectId, string url,
            MultimediaCategory category) {
            if (projectId <= 0) {
                return new ApiResult<Multimedia>(
                     StatusCode.BadRequest, "Null project id");
            }

            if (string.IsNullOrWhiteSpace(url)) {
                return new ApiResult<Multimedia>(
                     StatusCode.BadRequest, "Null Url");
            }

            if (category <= 0) {
                return new ApiResult<Multimedia>(
                     StatusCode.BadRequest, "Null category");
            }

            var project = await SearchProjectByIdAsync(projectId);

            if (project.Data == null) {
                return new ApiResult<Multimedia>(
                     StatusCode.NotFound, "No such project");
            }


            var multi = new Multimedia() {
                ProjectId = project.Data.Id,
                Url = url,
                MultimediaCategory = category,
                Project = project.Data
            };


            await context_.AddAsync(multi);
            try {
                await context_.SaveChangesAsync();
            } catch {

                logger_.LogError(StatusCode.InternalServerError,
                    $"Error Save Multimedia for project: {project.Data.Title}");

                return new ApiResult<Multimedia>
                     (StatusCode.InternalServerError,
                       $"Error Save Multimedia for project: {project.Data.Title}");

            }

            return ApiResult<Multimedia>.CreateSuccess(multi);
        }
    }
}
