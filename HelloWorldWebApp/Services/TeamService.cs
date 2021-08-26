using System.Collections.Generic;
using HelloWorldWebApp.Models;
using Microsoft.AspNetCore.SignalR;

namespace HelloWorldWebApp.Services
{
    public class TeamService : ITeamService
    {
        private readonly TeamInfo teamInfo;
        private readonly IBroadcastService broadcastService;

        public TeamService(IBroadcastService broadcastService)
        {
            this.broadcastService = broadcastService;

            this.teamInfo = new TeamInfo
            {
                Name = "Team 1",
                TeamMembers = new List<TeamMember>(),
            };

            string[] teamMembersData = new string[]
           {
                "Ema",
                "Sorina",
                "Fineas",
                "Patrick",
                "Radu",
                "Tudor",
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
            broadcastService.TeamMemberDeleted(memberId);
        }

        public int AddTeamMember(string name)
        {
            TeamMember newMember = new TeamMember() { Name = name };
            teamInfo.TeamMembers.Add(newMember);

            broadcastService.NewTeamMemberAdded(newMember.Name, newMember.Id);

            return newMember.Id;
        }

        public void UpdateMemberName(int memberId, string name)
        {
            TeamMember member = GetMemberById(memberId);
            member.Name = name;

            broadcastService.UpdateTeamMember(name, memberId);
        }

        public TeamMember GetMemberById(int memberId)
        {
            return teamInfo.TeamMembers.Find(element => element.Id == memberId);
        }
    }
}
