using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Services;
using Crowdfund.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Web.Controllers
{
    public class RewardController : Controller
    {
        private Core.Services.IProjectService projects_;
        private Core.Services.IRewardService rewards_;
        public RewardController(Core.Services.IProjectService projects,
            Core.Services.IRewardService rewards) {
            projects_ = projects;
            rewards_ = rewards;
        }


        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> CreateReward(
           [FromBody] Core.Model.Options.CreateRewardOptions options) {


            var rewardOptoins = new Core.Model.Options.CreateRewardOptions() {
                Title = options.Title,
                Description = options.Description,
                Value = options.Value
            };

            var searchOp = new Core.Model.Options.SearchProjectOptions() {
                Title = options.ProjectTitle
            };
            var project = projects_.SearchProject(searchOp).First();

            

            var result = await rewards_.CreateRewardAsync(1, project.Id, rewardOptoins);

            return result.AsStatusResult();
        }






















    }
}