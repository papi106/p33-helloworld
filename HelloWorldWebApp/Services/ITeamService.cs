using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public interface ITeamService
    {
        int AddTeamMember(string name);

        public void RemoveMember(int memberIndex);

        TeamInfo GetTeamInfo();
    }
}