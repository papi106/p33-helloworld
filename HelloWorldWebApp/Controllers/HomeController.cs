﻿// <copyright file="HomeController.cs" company="Principal 33">
// Copyright (c) Principal 33. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Diagnostics;
using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloWorldWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ITeamService teamService;
        private readonly ITimeService timeService;

        public HomeController(ILogger<HomeController> logger, ITeamService teamService, ITimeService timeService)
        {
            this.logger = logger;
            this.teamService = teamService;
            this.timeService = timeService;
        }

        [Authorize]
        [HttpPost]
        public int AddTeamMember(string teamMember)
        {
            return teamService.AddTeamMember(teamMember);
        }

        [Authorize]
        [HttpDelete]
        public void RemoveMember(int memberIndex)
        {
            teamService.RemoveMember(memberIndex);
        }

        [Authorize]
        [HttpPost]
        public void UpdateMemberName(int memberId, string name)
        {
            teamService.UpdateMemberName(memberId, name);
        }

        [HttpGet]
        public int GetCount()
        {
            return teamService.GetTeamInfo().TeamMembers.Count;
        }

        public IActionResult Index()
        {
            return View(teamService.GetTeamInfo());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
        
        public IActionResult Chat()
        {
            return View();
        }
    }
}
