using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
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
        [Fact]
        public void GettingAge()
        {
            //Assume
            TeamMember newMember = new TeamMember("UnitTests");
            newMember.Birthday = new DateTime(1990, 9, 30);

            //Act
            int age = newMember.GetAge();

            //Assert
            Assert.Equal(30, age);
        }
    }
}
