// <copyright file="HomeController.cs" company="Principal 33">
// Copyright (c) Principal 33. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Diagnostics;
using HelloWorldWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloWorldWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly TeamInfo teamInfo;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;

            this.teamInfo = new TeamInfo
            {
                Name = "Team 3",
                TeamMembers = new List<string>(new string[]
                {
                    "Sechei Radu",
                    "Tanase Teona",
                    "Duma Dragos",
                    "Campean Leon",
                    "Naghi Claudia",
                    "Marian George",
                }),
            };
        }

        [HttpPost]
        public void AddTeamMember(string teamMember)
        {
            this.teamInfo.TeamMembers.Add(teamMember);
        }

        [HttpGet]
        public int GetCount()
        {
            return this.teamInfo.TeamMembers.Count;
        }

        public IActionResult Index()
        {
            return this.View(this.teamInfo);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
