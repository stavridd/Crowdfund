using System;
using System.Linq;

using Xunit;
using Autofac;

using Crowdfund.Core.Services;
using Crowdfund.Core.Model.Options;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Test {
    public partial class ProjectServiceTests : IClassFixture<CrowdfundFixture>
    {
        private readonly IProjectService psvc_;

        public ProjectServiceTests (CrowdfundFixture fixture)
        {
            psvc_ = fixture.Container.Resolve<IProjectService>();
        }


        [Fact]
        public async Task CreateProjectSuccess()
        {
            var option = new CreateProjectOptions()
            {
                Title = $"The First Project{DateTime.Now.Millisecond}",
                Description = $"This is the first test project" +
                                $"{DateTime.Now.Millisecond}",
                projectcategory = Core.Model.ProjectCategory.DesignAndTech
            };

            var project = await psvc_.CreateProjectAsync(1,option);

            Assert.NotNull(project);
        }

        [Fact]
        public async Task CreateProjectFail_SameTitle()
        {
            var option = new CreateProjectOptions()
            {
                Title = $"The First Project",
                Description = $"This is the first test project",
                projectcategory = Core.Model.ProjectCategory.DesignAndTech
            };

            var project = await psvc_.CreateProjectAsync(1, option);

            Assert.NotNull(project);
        }

        [Fact]
        public async Task SearchProjectSuccess()
        {
            var rand = DateTime.Now.Second;

            var option = new CreateProjectOptions()
            {
                Title = $"This is a Test Project {rand}",
                Description = "This is the first test project",
                projectcategory = Core.Model.ProjectCategory.DesignAndTech
            };

            var project =  await psvc_.CreateProjectAsync(2, option);

            var search = new SearchProjectOptions()
            {
                Title = $"This is a Test Project {rand}",
            };

            var pr =  psvc_.SearchProject(search);
            var retreiveProject =await pr.ToListAsync();

            Assert.NotNull(retreiveProject);
            //Assert.Contains($"This is a Test Project {rand}",
            //         $"This is a Test Project {rand}");
        }

        [Fact]
        public void SearchProjectByIdSuccess()
        {
            
            var pr =  psvc_.SearchProjectByCstegory( 
                 Core.Model.ProjectCategory.DesignAndTech);

            Assert.NotNull(pr);
            
        }

        [Fact]
        public async Task ChangeProjectStatusSuccessAsync()
        {

            var pr = await psvc_.ChangeProjectStatusAsync(3,
                Core.Model.ProjectStatus.Completed);
            
            Assert.True(pr);

            var result = await psvc_.SearchProjectByIdAsync(3);

            var stat = result.Data.Status.ToString();

            Assert.Equal("Completed", stat);   
        }

        [Fact]
        public async Task GetProjectIdSuccessAsync()
        {

            var title = "This is a Test Project 41";
            var Desc = "This is the first test project";

            var Id = await psvc_.GetProjectIdAsync(title, Desc);

            Assert.Equal(1, Id);
        }

        [Fact]
        public async Task BuyProject_Success()
        {
            var success = await psvc_.BuyProjectAsync(1, 1, 1);
            Assert.True(success);
        }

        [Fact]
        public async Task AddStatusUpdate_Success()
        {
            var update = $"This is a status Uppdate {DateTime.Now.Second}";

            var isPosted = await psvc_.AddStatusUpdateAsync(3, update);

            Assert.True(isPosted);

            var stats = await psvc_.GetStatusUpdateAsync(3);

            //Assert.Equal(isPosted, stats.Data);

        }

        [Fact]
        public async Task AddMulti_Success() {
            var url = "https://www.google.com/url?sa=i&url=https%3A%2F%2Funsplash.com%2Fs%2Fphotos%2Fphotos&psig=AOvVaw3qW_LFhZC3J3q3hDosZFTr&ust=1582463875266000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCLD1m7uf5ecCFQAAAAAdAAAAABAD";


            var multi = await psvc_.AddMultiAsync(2, url, Crowdfund.Core.Model.MultimediaCategory.Photo);

            //Assert.True(isPosted);

            // var stats = await psvc_.GetStatusUpdateAsync(3);

            //Assert.Equal(isPosted, stats.Data);

        }
    }
}
