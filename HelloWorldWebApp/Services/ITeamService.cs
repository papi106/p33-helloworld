using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public interface ITeamService
    {
        void AddTeamMember(string name);

        public void RemoveMember(int memberIndex);

        TeamInfo GetTeamInfo();
    }
}