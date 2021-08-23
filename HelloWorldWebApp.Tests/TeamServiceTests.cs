using HelloWorldWebApp.Services;
//using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Xunit;

namespace HelloWorldWebApp.Tests
{

    public class TeamServiceTests
    {
        private Mock<IHubContext<MessageHub>> messageHubMock = null;
        private Mock<IClientProxy> hubAllClientsMock;

        private void InitializeMessageHubMock()
        {
            // https://www.codeproject.com/Articles/1266538/Testing-SignalR-Hubs-in-ASP-NET-Core-2-1
            hubAllClientsMock = new Mock<IClientProxy>();
            Mock<IHubClients> hubClients = new Mock<IHubClients>();
            hubClients.Setup(_ => _.All).Returns(hubAllClientsMock.Object);
            messageHubMock = new Mock<IHubContext<MessageHub>>();



            messageHubMock.SetupGet(_ => _.Clients).Returns(hubClients.Object);
        }

        private Mock<IHubContext<MessageHub>> GetMockedMessageHub()
        {
            if (messageHubMock == null)
            {
                InitializeMessageHubMock();
            }

            return messageHubMock;
        }

        [Fact]
        public void CheckAsync()
        {
            //Assume
            var messageHub = GetMockedMessageHub().Object;
            hubAllClientsMock.Setup(_ => _.SendAsync("unitTest", "test", 1, It.IsAny<CancellationToken>()));

            //Act
            messageHub.Clients.All.SendAsync("unitTest", "test", 1);

            //Assert
            hubAllClientsMock.Verify(hubAllClients => hubAllClients.SendAsync("unitTest", "test", 1, It.IsAny<CancellationToken>()), Times.Once, "I expct SendAsync to be called once.");
        }

        [Fact]
        public void AddTeamMemberToTheTeam()
        {
            //Assume
            var teamService = new TeamService(GetMockedMessageHub().Object);

            //Act
            int initialCount = teamService.GetTeamInfo().TeamMembers.Count;
            teamService.AddTeamMember("George");

            //Assert
            Assert.Equal(initialCount + 1, teamService.GetTeamInfo().TeamMembers.Count);
        }

        [Fact]
        public void RemoveMemberFromTheTeam()
        {
            // Assume
            var teamService = new TeamService(GetMockedMessageHub().Object);
            int initialCount = teamService.GetTeamInfo().TeamMembers.Count;
            var id = teamService.GetTeamInfo().TeamMembers[0].Id;

            // Act
            teamService.RemoveMember(id);

            // Assert
            Assert.Equal(initialCount - 1, teamService.GetTeamInfo().TeamMembers.Count);
        }

        [Fact]
        public void UpdateMemberName()
        {
            // Assume
            var teamService = new TeamService(GetMockedMessageHub().Object);
            var id = teamService.GetTeamInfo().TeamMembers[0].Id;

            // Act
            teamService.UpdateMemberName(id, "UnitTest");

            // Assert
            var member = teamService.GetMemberById(id);
            Assert.Equal("UnitTest", member.Name);
        }

        [Fact]
        public void CheckIdProblem()
        {
            // Assume
            var teamService = new TeamService(GetMockedMessageHub().Object);
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
