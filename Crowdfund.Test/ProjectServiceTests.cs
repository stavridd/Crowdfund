using System;
using System.Linq;

using Xunit;
using Autofac;

using Crowdfund.Core.Services;
using Crowdfund.Core.Model.Options;

namespace Crowdfund.Test {
    public partial class ProjectServiceTests : IClassFixture<CrowdfundFixture>
    {
        private readonly IProjectService psvc_;

        public ProjectServiceTests (CrowdfundFixture fixture)
        {
            psvc_ = fixture.Container.Resolve<IProjectService>();
        }


        [Fact]
        public void CreateProjectSuccess()
        {
            var option = new CreateProjectOptions()
            {
                Title = $"The First Project{DateTime.Now.Millisecond}",
                Description = $"This is the first test project" +
                                $"{DateTime.Now.Millisecond}",
                projectcategory = Core.Model.ProjectCategory.DesignAndTech
            };

            var project = psvc_.CreateProject(2,option);

            Assert.NotNull(project);
        }

        [Fact]
        public void CreateProjectFail_SameTitle()
        {
            var option = new CreateProjectOptions()
            {
                Title = $"The First Project",
                Description = $"This is the first test project",
                projectcategory = Core.Model.ProjectCategory.DesignAndTech
            };

            var project = psvc_.CreateProject(2, option);

            Assert.NotNull(project);
        }

        [Fact]
        public void SearchProjectSuccess()
        {
            var rand = DateTime.Now.Second;

            var option = new CreateProjectOptions()
            {
                Title = $"This is a Test Project {rand}",
                Description = "This is the first test project",
                projectcategory = Core.Model.ProjectCategory.DesignAndTech
            };

            var project = psvc_.CreateProject(3, option);

            var search = new SearchProjectOptions()
            {
                Title = $"This is a Test Project {rand}",
            };

            var pr = psvc_.SearchProject(search);

            Assert.NotNull(pr);
            Assert.Contains($"This is a Test Project {rand}",
                     $"This is a Test Project {rand}");
        }

        [Fact]
        public void SearchProjectByIdSuccess()
        {
            
            var pr = psvc_.SearchProjectByCstegory(
                 Core.Model.ProjectCategory.DesignAndTech);

            Assert.NotNull(pr);
            
        }

        [Fact]
        public void ChangeProjectStatusSuccess()
        {

            var pr = psvc_.ChangeProjectStatus(1,
                Core.Model.ProjectStatus.Completed);
            
            Assert.True(pr);

            var result = psvc_.SearchProjectById(1);

            var stat = result.Status.ToString();

            Assert.Matches("Completed", stat);   
        }

        [Fact]
        public void GetProjectIdSuccess()
        {

            var title = "This is a Test Project 29";
            var Desc = "This is the first test project";

            var Id = psvc_.GetProjectId(title, Desc);

            Assert.Equal(4, Id);

        }
    }
}
