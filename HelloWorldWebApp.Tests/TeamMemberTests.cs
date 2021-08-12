using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HelloWorldWebApp.Tests
{
    public class TeamMemberTests
    {
        private Mock<ITimeService> mock;

        private void InitializeMockService()
        {
            mock = new Mock<ITimeService>();

            mock.Setup( _ => _.GetCurrentDate()).Returns(new DateTime(2021, 8, 11));
        }

        [Fact]
        public void GettingAge()
        {
            //Assume
            InitializeMockService();
            var timeService = mock.Object;
            TeamMember newMember = new TeamMember("UnitTests", timeService);
            newMember.Birthday = new DateTime(1990, 9, 30);

            //Act
            int age = newMember.GetAge();

            //Assert
            Assert.Equal(30, age);
            mock.Verify(_ => _.GetCurrentDate(), Times.Once());
        }
    }
}
