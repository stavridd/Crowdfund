using System;
using System.Collections.Generic;
using System.Linq;
using Crowdfund.Core.Model;

namespace Crowdfund.Core.Services {
    public interface IProjectService 
    {
        Project CreateProject( int ownerId,
            Model.Options.CreateProjectOptions options);

        IQueryable<Project> SearchProject(
            Model.Options.SearchProjectOptions options);

        IQueryable<Project> SearchProjectByCstegory(
            ProjectCategory category);

        bool ChangeProjectStatus(int projectId,
            ProjectStatus Status);

        Project SearchProjectById(int projectId);

        // bool ProjectStatusUpdate(Project id);

        bool BuyProject(int projectId, int buyerId, int rewardId);
        
        public int GetProjectId(string title, string Desc);
    }
}
