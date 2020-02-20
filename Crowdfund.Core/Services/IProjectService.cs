using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Model;

namespace Crowdfund.Core.Services {
    public interface IProjectService 
    {
        Task<ApiResult<Project>> CreateProjectAsync( int ownerId,
            Model.Options.CreateProjectOptions options);

        IQueryable<Project> SearchProject(
            Model.Options.SearchProjectOptions options);

        IQueryable<Project> SearchProjectByCstegory(
            ProjectCategory category);

       Task <bool> ChangeProjectStatusAsync(int projectId,
            ProjectStatus Status);

        Task<ApiResult<Project>> SearchProjectByIdAsync(int projectId);

        // bool ProjectStatusUpdate(Project id);

        Task<bool> BuyProjectAsync(int projectId, int buyerId, int rewardId);
        
         Task<int> GetProjectIdAsync(string title, string Desc);
    }
}
