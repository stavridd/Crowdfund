using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund.Web.Controllers
{
    public class ProjectController : Controller {

        private Core.Services.IProjectService projects_;
        public ProjectController(
          Core.Services.IProjectService projects) {
            projects_ = projects;
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


        [HttpPost]
        public async Task<IActionResult> CreateProject(int id,
           [FromBody] Core.Model.Options.CreateProjectOptions options) {

            var result = await projects_.CreateProjectAsync(id,options);

            return result.AsStatusResult();
        }
    }
}