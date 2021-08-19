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
            //ITimeService timeService = new TimeService();
            //TeamMember teamMember = new TeamMember("member1", timeService);

            //string json = JsonSerializer.Serialize(teamMember);
            //Console.WriteLine(json);

            //File.WriteAllText("TeamMember.json",json);

            //Task<string> jsonTask = File.ReadAllTextAsync("TeamMember.json");
            //jsonTask.Wait();
            //string jsonFile = jsonTask.Result;

            //TeamMember deserializerMember = JsonSerializer.Deserialize<TeamMember>(jsonFile);

            Console.Write("What would you like?");
            string input = Console.ReadLine();
            Func<string, string, string, string, Coffee> receipe = input == "FlatWhite" ? FlatWhite : Espresso;

            Coffee coffe = MakeCoffe("Grains", "Milk", "Water", "Sugar", receipe);
            if (coffe != null)
            {
                Console.WriteLine($"Here is your coffee: {coffe}.");
            }
        }

        static public Coffee MakeCoffe(string grains, string milk, string water, string sugar, Func<string, string, string, string, Coffee> recipe)
        {
            try
            {
                Console.WriteLine("Start preparing the coffe");
                var coffe = recipe(grains, milk, water, sugar);
                return coffe;
            }
            catch (RecipeUnavailableException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Your order could not be completed.");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong!");
                Console.WriteLine(e.Message);
                Console.WriteLine("Your order could not be completed.");
                return null;
            }
            finally
            {
                Console.WriteLine("Finished");
            }
        }

        static public Coffee Espresso(string grains, string milk, string water, string sugar)
        {
            throw new RecipeUnavailableException("Water is stale.");
        }
        
        static public Coffee FlatWhite(string grains, string milk, string water, string sugar)
        {
            return new Coffee("FlatWhite");
        }
    }
}
