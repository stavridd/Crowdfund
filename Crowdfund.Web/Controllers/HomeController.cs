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

namespace Crowdfund.Web.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly CrowdfundDbContext context_;

        public HomeController(ILogger<HomeController> logger,
            Crowdfund.Core.Data.CrowdfundDbContext context)
        {
            _logger = logger;
            context_ = context;
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
