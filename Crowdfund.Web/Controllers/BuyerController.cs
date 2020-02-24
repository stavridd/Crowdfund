using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Data;
using Crowdfund.Core.Model;
using Crowdfund.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund.Web.Controllers {
    public class BuyerController : Controller {


        private Core.Services.IBuyerService buyers_;
        private Core.Services.IProjectService projects_;
        private readonly CrowdfundDbContext context_;
        public BuyerController(Core.Services.IProjectService projects,
          Core.Services.IBuyerService buyers, CrowdfundDbContext context) {
            buyers_ = buyers;
            projects_ = projects;
            context_ = context;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(
        //      Models.CreateBuyerViewModel model) {

        //    var result = await buyers_.CreateBuyerAsync(
        //        model?.CreateOptions);

        //    if (result == null) {
        //        model.ErrorText = "Oops. Something went wrong";

        //        return View(model);
        //    }

        //    return Ok();
        //}


        [HttpPost]
        public async Task<IActionResult> CreateBuyer(
              [FromBody] Core.Model.Options.CreateBuyerOptions options) {

            var result = await buyers_.CreateBuyerAsync(options);
            return result.AsStatusResult();
        }



        [HttpGet]
        public async Task<IActionResult> BrowseFundedProjects() {

            var projectList = context_
                .Set<ProjectBuyer>()
                .Where(p => p.BuyerId == 1)
                .Take(100)              
                .ToList();

            var list = new List<Project>();

            foreach (var p in projectList) {

                var pj = await projects_.SearchProjectByIdAsync(p.ProjectId);

                list.Add(pj.Data);
            }

            var projects = new Models.ProjectsViewModel() {
                BuyerProjects = list
            };
            return View(projects);
        }










    }
}