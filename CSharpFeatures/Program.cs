using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

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

            Task<string> jsonTask = File.ReadAllTextAsync("TeamMember.json");
            jsonTask.Wait();
            string jsonFile = jsonTask.Result;

            TeamMember deserializerMember = JsonSerializer.Deserialize<TeamMember>(jsonFile);

            Console.WriteLine(deserializerMember);
        }
    }
}
