using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Monopoly_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Gameboard.Init();
            ActionManager.Init();
            TurnManager.Init();

            Gameboard g = Gameboard.instance;
            ActionManager a = ActionManager.instance;
            TurnManager t = TurnManager.Instance;

            do
            {
                TurnManager.Instance.NextTurn();
            } while (!TurnManager.GameEnded());

            Console.ReadKey();
        }

        private static void SaveGame(Gameboard g)
        {
            int count = 0;
            while (File.Exists("savedGame" + count + ".monopolySave")) 
            {
                count++;
            }

            string toSave = JsonConvert.SerializeObject(g.Cells, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            File.WriteAllText("savedGame"+count+".monopolySave", toSave);
        }

        public static Cell[] LoadGame(string filename = "savedGame0.monopolySave")
        {
            if (File.Exists(filename))
            {
                Console.WriteLine("Game Loaded !");
                return JsonConvert.DeserializeObject<Cell[]>(File.ReadAllText(filename), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            return null;
        }
    }
}
