using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund.Web.Controllers {
    public class BuyerController : Controller {


        private Core.Services.IBuyerService buyers_;
        public BuyerController(
          Core.Services.IBuyerService buyers) {
            buyers_ = buyers;
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

    }
}