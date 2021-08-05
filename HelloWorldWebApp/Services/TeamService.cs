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

            int i = 0;
            foreach (string name in teamMembersData)
            {
                teamInfo.TeamMembers.Add(new TeamMember(i++, name));
            }
        }

        public TeamInfo GetTeamInfo()
        {
            return teamInfo;
        }

        public void RemoveMember(int memberIndex)
        {
            int listIndex = teamInfo.TeamMembers.FindIndex(element => element.Id == memberIndex);

            teamInfo.TeamMembers.RemoveAt(listIndex);
        }

        public int AddTeamMember(string name)
        {
            int newId = teamInfo.TeamMembers.Count;
            teamInfo.TeamMembers.Add(new TeamMember(newId, name));

            return newId;
        }
    }
}
