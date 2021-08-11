using System.Collections.Generic;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public class TeamService : ITeamService
    {
        private readonly TeamInfo teamInfo;

        public TeamService()
        {
            this.teamInfo = new TeamInfo
            {
                Name = "Team 3",
                TeamMembers = new List<TeamMember>(),
            };

            string[] teamMembersData = new string[]
           {
                "Sechei Radu",
                "Tanase Teona",
                "Duma Dragos",
                "Campean Leon",
                "Naghi Claudia",
                "Marian George",
           };

            foreach (string name in teamMembersData)
            {
                AddTeamMember(name);
            }
        }

        public TeamInfo GetTeamInfo()
        {
            return teamInfo;
        }

        public void RemoveMember(int memberId)
        {
            TeamMember member = GetMemberById(memberId);
            teamInfo.TeamMembers.Remove(member);
        }

        public int AddTeamMember(string name)
        {
            int count = teamInfo.TeamMembers.Count;
            teamInfo.TeamMembers.Add(new TeamMember(name));

            int newId = teamInfo.TeamMembers[count].Id;
            return newId;
        }

        public void UpdateMemberName(int memberId, string name)
        {
            TeamMember member = GetMemberById(memberId);
            member.Name = name;
        }

        public TeamMember GetMemberById(int memberId)
        {
            return teamInfo.TeamMembers.Find(element => element.Id == memberId);
        }
    }
}
