using System;
using System.Linq;
using Crowdfund.Core.Data;
using Crowdfund.Web.Models;
using Crowdfund.Core.Model;
using System.Threading.Tasks;
using Crowdfund.Core.Services;
using Crowdfund.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Web.Controllers
{
    public class ProjectController : Controller
    {
        private Core.Services.IProjectService projects_;
        private Core.Services.IRewardService rewards_;

        public ProjectController(IRewardService rewards,
          Core.Services.IProjectService projects)
        {
            projects_ = projects;
            rewards_ = rewards;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet("project/{id}")]
        public async Task<IActionResult> Detail(int id)
        {

            var project = await projects_.SearchProjectByIdAsync(id);
            var details = await projects_.GetStatusUpdateAsync(id);
            var multi = await projects_.GetProjectPhotoAsync(id);
            var d1 = project.Data.Contributions;
            var d2 = project.Data.Goal;
            var tot = d1 / d2;

            var model = new Models.ProjectDetailView()
            {
                Title = project.Data.Title,
                Description = project.Data.Description,
                Goal = project.Data.Goal,
                Updates = details.Data,
                Multis = multi.Data,
                Contributions = project.Data.Contributions,
                Progress = tot * 100,
                ProjectCategory = project.Data.projectcategory,
                Rewards = await rewards_.SearchRewardByProjectIdAsync(project.Data.Id),
                Id = project.Data.Id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(
           [FromBody] Models.CreateProjectViewModel options)
        {
            var result = await projects_.CreateProjectAsync(2, options.CreateOptions);
            var searchOp = new Core.Model.Options.SearchProjectOptions()
            {
                Title = options.CreateOptions.Title
            };
            var project = await projects_.SearchProject(searchOp)
                .SingleOrDefaultAsync();
            var photo = await projects_.AddMultiAsync(project.Id, options.CreateOptions.Multis,
                MultimediaCategory.Photo);

            foreach (var r in options.Reward)
            {
                var rewardOp = new Core.Model.Options.CreateRewardOptions()
                {
                    Title = r.Title,
                    Description = r.Description,
                    Value = r.Value
                };

                var reward = await rewards_.CreateRewardAsync(1, project.Id, rewardOp);
            }
            return result.AsStatusResult();
        }

        [HttpPost]
        public async Task<IActionResult> BuyProject(
         [FromBody] BuyProjectOptions option)
        {
            var result = await projects_.BuyProjectAsync(option.ProjectId, 1, option.RewardId);
            var reward = await rewards_.SearchRewardByIdAsync(option.RewardId);
            return Json(reward);
        }
    }
}