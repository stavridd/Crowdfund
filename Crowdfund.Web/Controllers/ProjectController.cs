using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Data;
using Crowdfund.Core.Model;
using Crowdfund.Core.Services;
using Crowdfund.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Web.Controllers
{
    public class ProjectController : Controller {

        private Core.Services.IProjectService projects_;
        private Core.Services.IRewardService rewards_;

       
        public ProjectController(IRewardService rewards,
          Core.Services.IProjectService projects) {
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

        //[HttpPost]
        //public async Task <IActionResult> Create(
        //   Models.CreateProjectViewModel model) {

        //    var result = await projects_.CreateProjectAsync(model.OwnerId,
        //        model?.CreateOptions);

        //    if (result == null) {
        //        model.ErrorText = "Oops. Something went wrong";

        //        return View(model);
        //    }

        //    return Ok();
        //}

        [HttpGet("project/{id}")]
        public async Task<IActionResult> Detail(int id) {

            var project = await projects_.SearchProjectByIdAsync(id);
            var details = await projects_.GetStatusUpdateAsync(id);
            var multi = await projects_.GetProjectPhotoAsync(id);


            var d1 = project.Data.Contributions;
            var d2 = project.Data.Goal;
            //var pr = Decimal.Divide(project.Data.Contributions, project.Data.Goal);
            //var pr = ((int)project.Data.Contributions /
            //                   (int)project.Data.Goal);

            var tot = 0.7M;

            var model = new Models.ProjectDetailView() {
                Title = project.Data.Title,
                Description = project.Data.Description,
                Goal = project.Data.Goal,
                Updates = details.Data,
                Multis = multi.Data,
                Contributions = project.Data.Contributions,
                progress = tot*100,
                ProjectCategory = project.Data.projectcategory,
                Rewards = await rewards_.SearchRewardByProjectIdAsync(project.Data.Id)

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(
           [FromBody] Models.CreateProjectViewModel options) {

            var result = await projects_.CreateProjectAsync(2,options.CreateOptions);

            var searchOp = new Core.Model.Options.SearchProjectOptions() {
                Title = options.CreateOptions.Title
            };
            var project = await projects_.SearchProject(searchOp)
                .SingleOrDefaultAsync();

            var photo = await projects_.AddMultiAsync(project.Id, options.CreateOptions.Multis,
                MultimediaCategory.Photo);

            foreach (var r in options.reward) {

                var rewardOp = new Core.Model.Options.CreateRewardOptions() {

                    Title = r.Title,
                    Value = r.Value

                };

                var reward = await rewards_.CreateRewardAsync(1, 1, rewardOp );
            }

            return result.AsStatusResult();
        }


        //[HttpGet]
        //public IActionResult Buy() {
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> BuyProject(
        // int rewardId) {

        //    var result = await projects_.BuyProjectAsync(1, 1, rewardId);

        //    return Ok();
        //    //return result.AsStatusResult();
        //}



    }
}