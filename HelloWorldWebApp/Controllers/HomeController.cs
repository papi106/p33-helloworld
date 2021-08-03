using HelloWorldWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TeamInfo teamInfo;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            teamInfo = new TeamInfo();
            teamInfo.Name = "Team 3";
            teamInfo.TeamMembers = new List<string>();
            teamInfo.TeamMembers.Add("Sechei Radu");
            teamInfo.TeamMembers.Add("Tanase Teona");
            teamInfo.TeamMembers.Add("Duma Dragos");
            teamInfo.TeamMembers.Add("Campean Leon");
            teamInfo.TeamMembers.Add("Naghi Claudia");
            teamInfo.TeamMembers.Add("Marian George");
        }

        public IActionResult Index()
        {
            return View(teamInfo);
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
