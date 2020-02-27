using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Crowdfund.Web.Models;
using Autofac;
using Crowdfund.Core.Data;
using Crowdfund.Core.Model;
using Crowdfund.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Web.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly CrowdfundDbContext context_;
        private readonly IProjectService projects_;

        public HomeController(ILogger<HomeController> logger,
            Crowdfund.Core.Data.CrowdfundDbContext context,
            IProjectService projects)
        {
            _logger = logger;
            context_ = context;
            projects_ = projects;
        }

        public IActionResult Index()
        {
            var projectList = context_
                .Set<Project>()
                .Take(100)
                .OrderByDescending(a => a.Contributions)
                .ToList();

            var projects = new Models.ProjectsViewModel()
            {
                Projects = projectList
            };
            return View(projects);
        }

        public async Task<IActionResult> SearchProjects(
    string title) {
            if (string.IsNullOrWhiteSpace(title)) {
                return BadRequest("Title is required");
            }
            var projectList = await projects_.SearchProject(
                new Core.Model.Options.SearchProjectOptions() {
                    Title = title
                })
                .Select(c => new { c.Title })
                .Take(100)
                .ToListAsync();
            return Json(projectList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Creator() {
            return View();
        }

        public IActionResult Backer() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
