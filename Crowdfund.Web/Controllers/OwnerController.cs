using System;
using System.Linq;
using Crowdfund.Core.Data;
using Crowdfund.Core.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Crowdfund.Web.Extensions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Crowdfund.Web.Controllers
{
    public class OwnerController : Controller
    {
        private Core.Services.IOwnerService owners_;
        private readonly CrowdfundDbContext context_;

        public OwnerController(CrowdfundDbContext context,
          Core.Services.IOwnerService owners)
        {
            owners_ = owners;
            context_ = context;
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

        [HttpPost]
        public async Task<IActionResult> CreateOwner(
           [FromBody] Core.Model.Options.CreateOwnerOptions options)
        {
            var result = await owners_.CreateOwnerAsync(options);
            return result.AsStatusResult();
        }

        [HttpGet]
        public IActionResult BrowseFundedProjects()
        {
            var projectList = context_
                .Set<Project>()
                .Where(p => p.OwnerId == 1)
                .Take(100)
                .OrderByDescending(a => a.Contributions)
                .ToList();

            var projects = new Models.ProjectsViewModel()
            {
                OwnerProjects = projectList
            };
            return View(projects);
        }
    }
}





