using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using System;
using System.IO;
using System.Text.Json;

namespace CSharpFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            ITimeService timeService = new TimeService();
            TeamMember teamMember = new TeamMember("member1", timeService);

            string json = JsonSerializer.Serialize(teamMember);
            Console.WriteLine(json);

            File.WriteAllText("TeamMember.json",json);

            string jsonFile = File.ReadAllText("TeamMember.json");
            TeamMember deserializerMember = JsonSerializer.Deserialize<TeamMember>(jsonFile);
            
        }
    }
}
