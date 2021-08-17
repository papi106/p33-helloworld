using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public class DbTeamService : ITeamService
    {
        private readonly ApplicationDbContext context;

        public DbTeamService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int AddTeamMember(string name)
        {
            TeamMember newMember = new TeamMember() { Name = name };
            context.Add(newMember);
            context.SaveChanges();

            return newMember.Id;
        }

        public TeamMember GetMemberById(int memberId)
        {
            throw new NotImplementedException();
        }

        public TeamInfo GetTeamInfo()
        {
            var result = context.TeamMembers.ToList();
            return new TeamInfo(result);
        }

        public void RemoveMember(int memberId)
        {
            var teamMember = context.TeamMembers.Find(memberId);
            context.TeamMembers.Remove(teamMember);
            context.SaveChanges();
        }

        public void UpdateMemberName(int memberId, string name)
        {
            throw new NotImplementedException();
        }
    }
}
