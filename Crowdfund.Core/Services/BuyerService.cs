using System;
using System.Linq;
using Crowdfund.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Crowdfund.Core.Model.Options;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Core.Services
{
    public class BuyerService : IBuyerService 
    {
        
        private readonly ILoggerService logger_;
        private readonly IProjectService projects_;
        private readonly Data.CrowdfundDbContext context_;

        public BuyerService(Data.CrowdfundDbContext context,
            IProjectService projects, ILoggerService logger)
        {
            context_ = context ?? throw new ArgumentException(nameof(context));
            projects_ = projects ?? throw new ArgumentException(nameof(projects));
            logger_ = logger ?? throw new ArgumentException(nameof(logger));
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
            } catch {

                logger_.LogError(StatusCode.InternalServerError,
                    $"Error Save Backer: {buyer.LastName}");

                return new ApiResult<Buyer>
                     (StatusCode.InternalServerError,
                       "Error Save Backer");

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

        public async Task<ApiResult<Buyer>> UpdateBuyerAsync(int id, 
                        UpdateBuyerOptions options)
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
            } catch {

                logger_.LogError(StatusCode.InternalServerError,
                    $"Error Update Backer: {buyer.Data.LastName}");

                return new ApiResult<Buyer>
                     (StatusCode.InternalServerError,
                       "Error Update Backer");

            }

            return ApiResult<Buyer>.CreateSuccess(buyer.Data);
        }

        public async Task<ApiResult<ICollection<Project>>> GetMyProjectsAsync(
                    int buyerId)
        {
            var buyer = await SearchBuyerByIdAsync(buyerId);

            if (buyer == null) {
                return new ApiResult<ICollection<Project>>(
                    StatusCode.BadRequest, "Invalid Id");
            }

            var buyerProjects = buyer.Data.Projects;

            ICollection<Project> projectList;
            projectList = new List<Project>();

            foreach(var p in buyerProjects) {

                var project = await projects_.SearchProjectByIdAsync(p.ProjectId);
                 projectList.Add(project.Data);
            }

            if (projectList.Count == 0) {
                return new ApiResult<ICollection<Project>>(
                    StatusCode.BadRequest, "No projects are available");
            }

            return ApiResult<ICollection<Project>>.CreateSuccess(projectList);
        }


        public async Task<ApiResult<ICollection<Project>>> GetMyCompletedProjectsAsync(
                    int buyerId)
        {
            var buyer = await SearchBuyerByIdAsync(buyerId);

            if (buyer == null) {
                return new ApiResult<ICollection<Project>>(
                    StatusCode.BadRequest, "Invalid Id");
            }

            var buyerProjects = buyer.Data.Projects;

            ICollection<Project> projectList;
            projectList = new List<Project>();

            foreach (var p in buyerProjects) {

                var project = await projects_.SearchProjectByIdAsync(p.ProjectId);

                if (project.Data.Status == ProjectStatus.Completed) {
                    projectList.Add(project.Data);
                }
            }

            if (projectList.Count == 0) {
                return new ApiResult<ICollection<Project>>(
                    StatusCode.BadRequest, "No projects are available");
            }
            return ApiResult<ICollection<Project>>.CreateSuccess(projectList);

        }

        public async Task<bool> IsBuyerAllowedToSee(int buyerId, int projectId)
        {
            if (buyerId <= 0) {
                return false;
            }
            if (projectId <= 0) {
                return false;
            }
            var buyer = await SearchBuyerByIdAsync(buyerId);
            if (buyer == null) {
                return false;
            }
            var project = await projects_.SearchProjectByIdAsync(projectId);
            if (project == null) {
                return false;
            }

            var exists = await context_
                .Set<ProjectBuyer>()
                .SingleOrDefaultAsync(
                s => s.BuyerId == buyerId &&
                s.ProjectId == projectId);

            if (exists == null) {
                return false;
            }
            return true;
        }
    }
}
