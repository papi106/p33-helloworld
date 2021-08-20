using HelloWorldWebApp.Services;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace HelloWorldWebApp.Tests
{
    public class TeamServiceTests
    {
        //[Fact]
        public void AddTeamMemberToTheTeam()
        {
            //Assume
            var teamServiceMock = new Mock<ITeamService>();
            var teamService = teamServiceMock.Object;
            //Act
            int initialCount = teamService.GetTeamInfo().TeamMembers.Count;
            teamService.AddTeamMember("George");

            //Assert
            Assert.Equal(initialCount + 1, teamService.GetTeamInfo().TeamMembers.Count);
        }

        //[Fact]
        public void RemoveMemberFromTheTeam()
        {
            // Assume
            var teamServiceMock = new Mock<ITeamService>();
            var teamService = teamServiceMock.Object;

            int initialCount = teamService.GetTeamInfo().TeamMembers.Count;
            var id = teamService.GetTeamInfo().TeamMembers[0].Id;

            // Act
            teamService.RemoveMember(id);

            // Assert
            Assert.Equal(initialCount - 1, teamService.GetTeamInfo().TeamMembers.Count);
        }

        //[Fact]
        public void UpdateMemberName()
        {
            // Assume
            var teamServiceMock = new Mock<ITeamService>();
            var teamService = teamServiceMock.Object;

            var id = teamService.GetTeamInfo().TeamMembers[0].Id;

            // Act
            teamService.UpdateMemberName(id, "UnitTest");

            // Assert
            var member = teamService.GetMemberById(id);
            Assert.Equal("UnitTest", member.Name);
        }

        //[Fact]
        public void CheckIdProblem()
        {
            // Assume
            var messageHub = new Mock<IHubContext<MessageHub>>();
            var mockClients = new Mock<IHubClients>();
            ITeamService teamService = new TeamService(messageHub.Object);

            var id = teamService.GetTeamInfo().TeamMembers[0].Id;

            // Act
            teamService.RemoveMember(id);
            int memberId = teamService.AddTeamMember("Test");
            teamService.RemoveMember(memberId);

            // Assert
            int lastIndex = teamService.GetTeamInfo().TeamMembers.Count;
            Assert.NotEqual("Test", teamService.GetTeamInfo().TeamMembers[lastIndex - 1].Name);
        }
    }
}
