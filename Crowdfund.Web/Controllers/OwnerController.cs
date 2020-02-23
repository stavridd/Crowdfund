using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Crowdfund.Web.Extensions;
using Crowdfund.Core.Data;

using Microsoft.EntityFrameworkCore;


namespace Crowdfund.Web.Controllers {
    public class OwnerController : Controller {
        private Core.Services.IOwnerService owners_;

        public OwnerController(
          Core.Services.IOwnerService owners)
        {
            owners_ = owners;
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

        //[HttpPost]
        //public async Task<IActionResult> Create(
        //   Models.CreateOwnerViewModel model) {

        //    var result = await owners_.CreateOwnerAsync(
        //        model?.CreateOptions);

        //    if (result == null) {
        //        model.ErrorText = "Oops. Something went wrong";

        //        return View(model);
        //    }

        //    return Ok();
        //}


        [HttpPost]
        public async Task<IActionResult> CreateOwner(
           [FromBody] Core.Model.Options.CreateOwnerOptions options) {

            var result = await owners_.CreateOwnerAsync(options);

            return result.AsStatusResult();
        }
    }
}





