using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowdfund.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Core.Services {
    public class ProjectService : IProjectService
    {
        private readonly Data.CrowdfundDbContext context_;
        private readonly IOwnerService owners_;
        private readonly IRewardService rewards_;
        private readonly IOwnerService owner;

        public ProjectService(Data.CrowdfundDbContext context,
                  IRewardService reward, IOwnerService owner)
        {
            context_ = context ??
                throw new ArgumentException(nameof(context));
            rewards_ = reward;
            owners_ = owner;
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
              string.IsNullOrWhiteSpace(options.Description)) {
                return new ApiResult<Project>(
                     StatusCode.BadRequest, "Null Title or Description");
            }

            if (options.projectcategory < 0 )
            {
                return new ApiResult<Project>(
                     StatusCode.BadRequest, " Project Category is Invlaid");
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
                Owner = owner.Data
            };



           owner.Data.Projects.Add(project);

           await context_.AddAsync(project);
            try {
                await context_.SaveChangesAsync();
            } catch (Exception ex) {

                return new ApiResult<Project>(
                      StatusCode.InternalServerError, "Project Was Not Added");
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
                    c.Title == options.Title);
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
            } catch (Exception ex) {

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

            //Must change Below

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
            } catch (Exception ex) {

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


    }
}
