using HelloWorldWebApp.Services;
using System;
using Xunit;

namespace HelloWorldWebApp.Tests
{
    public class TeamServiceTest
    {
        [Fact]
        public void AddTeamMemberToTheTeam()
        {
            //Assume
            ITeamService teamService = new TeamService();

            //Act
            teamService.AddTeamMember("George");

            //Assert
            Assert.Equal(7, teamService.GetTeamInfo().TeamMembers.Count);
        }

        [Fact]
        public void RemoveMemberFromTheTeam()
        {
            // Assume
            ITeamService teamService = new TeamService();

            // Act
            int initialCount = teamService.GetTeamInfo().TeamMembers.Count;
            var id = teamService.GetTeamInfo().TeamMembers[0].Id;
            teamService.RemoveMember(id);

            // Assert
            Assert.Equal(initialCount - 1, teamService.GetTeamInfo().TeamMembers.Count);
        }

        [Fact]
        public void UpdateMemberName()
        {
            // Assume
            ITeamService teamService = new TeamService();

            // Act
            teamService.UpdateMemberName(1, "UnitTest");

            // Assert
            var member = teamService.GetMemberById(1);
            Assert.Equal("UnitTest", member.Name);
        }
    }
}
