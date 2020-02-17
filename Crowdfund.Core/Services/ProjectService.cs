using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdfund.Core.Model;

namespace Crowdfund.Core.Services {
    public class ProjectService : IProjectService
    {
        private readonly Data.CrowdfundDbContext context_;
        private readonly IOwnerService owners_;
        private readonly IRewardService rewards_;

        public ProjectService(Data.CrowdfundDbContext context,
                  IOwnerService owners, IRewardService reward)
        {
            context_ = context ??
                throw new ArgumentException(nameof(context));
            owners_ = owners;
            rewards_ = reward;
        }

        public Project CreateProject(int ownerId,
            Model.Options.CreateProjectOptions options)
        {
            if (options == null) {
                return null;
            }

            if (string.IsNullOrWhiteSpace(options.Title) ||
              string.IsNullOrWhiteSpace(options.Description)) {
                return null;
            }

            if (options.projectcategory < 0 )
            {
                return null;
            }

            var owner = owners_.SearchOwnerById(ownerId);

            if (owner == null) {
                return null;
            }

            // only once every title && description
            var exist = GetProjectId(options.Title, options.Description);

            if (exist !=0 ) {
                return null;
            }

            var project = new Project()
            {
                Title = options.Title,
                Description = options.Description,
                projectcategory = options.projectcategory, 
                Owner = owner
            };



            owner.Projects.Add(project);

            context_.Add(project);
            try {
                context_.SaveChanges();
            } catch (Exception ex) {

                return null;
            }

            return project;
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

        public bool ChangeProjectStatus(int projectid, ProjectStatus Status)
        {
            if (projectid <= 0) {
                return false;
            }

            if (Status <= 0) {
                return false;
            }

            var project = SearchProjectById(projectid);

            if (project == null) {
                return false;
            }

            project.Status = Status;

            context_.Update(project);
            var success = false;
            try {
                success = context_.SaveChanges() > 0;
            } catch (Exception ex) {

            }
            return success;
        }

        public Project SearchProjectById (int projectId)
        {
            if (projectId <= 0) {
                return null;
            }

            return context_
                .Set<Project>()
                .SingleOrDefault(s => s.Id == projectId);
        }


        //not sure!!
        // Not tested!!
        public bool BuyProject(int projectId, int buyerId,
            int rewardId)
        {
            if (projectId == 0 ||
                buyerId == 0 ||
                 rewardId == 0) {
                return false;
            }

            var project = SearchProjectById(projectId);

            if (project == null) {
                return false;
            }

            project.Buyers.Add(
                new ProjectBuyer()
                {
                    ProjectId = project.Id,
                    BuyerId = buyerId
                });

            var reward = rewards_.SearchRewardById(rewardId);

            if (reward == null) {
                return false;
            }

            reward.Buyers.Add(
                new BuyerReward()
                {
                    RewardId = rewardId,
                    BuyerId = buyerId
                });
            
            return true;

        }

        public int GetProjectId(string title, string Desc)
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


            var pr = query.SingleOrDefault();
            if (pr == null) {
                return 0;
            } else {
                return pr.Id;
            }
        }
    }
}
