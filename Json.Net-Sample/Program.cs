using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Json.Net_Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //JsonSerializerSettings settingsTypeNameHandlingAll = new JsonSerializerSettings
            //{
            //    TypeNameHandling = TypeNameHandling.All
            //};
            
            //***** TypeNameHandling.None vs TypeNameHandling.All *****
            //Person person = new Person() { Name = "Tom", Age = 18 };
            //Console.WriteLine("TypeNameHandling = default(None):");
            //Console.WriteLine($"{JsonConvert.SerializeObject(person)}");
            //Console.WriteLine();
            //Console.WriteLine("TypeNameHandling = All:");
            //Console.WriteLine($"{JsonConvert.SerializeObject(person, settingsTypeNameHandlingAll)}");

            //***** What can TypeNameHandling.All do? *****
            // It can serialize and deserialize objects with type information.
            //List<Animal> animals = new List<Animal>
            //{
            //    new Dog { Name = "Buddy", Breed = "Golden Retriever" },
            //    new Cat { Name = "Whiskers", IsIndoor = true }
            //};
            //Console.WriteLine(JsonConvert.SerializeObject(person));
            //Console.WriteLine(JsonConvert.SerializeObject(animals, settingsTypeNameHandlingAll));


            var evilObject = JsonConvert.DeserializeObject(File.ReadAllText("Json.Net-Sample.json"), new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            });

            Console.ReadLine();
        }
    }

}
