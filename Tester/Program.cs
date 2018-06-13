using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Linq;
using RealmeyeSharp;

namespace Tester
{
    class Program
    {
        
        static void Main(string[] args)
        {
            #region realm
            /*
            User user = new User();
            Console.Write("In Game Name: ");
            var IGN = Console.ReadLine();
            Realm.GetUserSummary(IGN, user);
            GetUserPetStats(user);
            Realm.GetUserDescription(user);
            Realm.GetUserClasses(user);
            int fame = Convert.ToInt32(user.Fame.Substring(0, user.Fame.IndexOf(" ")));
            Console.WriteLine("Name: " + user.Name + 
                "\nCharacters: " + user.Chars + 
                "\nSkins: " + user.Skins + 
                "\nFame: " + user.Fame + 
                "\nRank: " + user.Rank + 
                "\nAccount fame: " + user.AccFame + 
                "\nGuild: " + user.Guild + 
                "\nCreated: " + user.Created + 
                "\nPet name: " + user.PetName + 
                "\nPet stats: " + user.Petstat1 + " " + user.Petlvl1 + " " + user.Petstat2 + " " + user.Petlvl2 + " " + user.Petstat3 + " " + user.Petlvl3 + 
                "\nDesc1: " + user.Desc1 + "\nDesc2: " + user.Desc2 + "\nDesc3: " + user.Desc3);
            int i = 0;
            foreach (var c in user.Classes)
            {
                Console.WriteLine("Class: " + i + "\nName: " + c.ClassName + " Lvl: " + c.Lvl + "\nClass Quest Completed: " + c.CQC + " Fame: " + c.Fame +
                    "\nEquipments Weapon: " + c.Eq1 + "\nAbility: " + c.Eq2 + "\nArmour: " + c.Eq3 + "\nRing: " + c.Eq4 + "\nBackpack: " + c.Backpack + "\nStats: " + c.Stats);
                i++;
            }

            //Example GetAllUserInfo(IGN)
            Console.Write("\nWrite ur ign again to test this function \nIGN: ");
            Realm.GetAllUserInfo(Console.ReadLine(), user);

            Console.WriteLine("Name: " + user.Name +
                "\nCharacters: " + user.Chars +
                "\nSkins: " + user.Skins +
                "\nFame: " + user.Fame +
                "\nRank: " + user.Rank +
                "\nAccount fame: " + user.AccFame +
                "\nGuild: " + user.Guild +
                "\nCreated: " + user.Created +
                "\nPet name: " + user.PetName +
                "\nPet stats: " + user.Petstat1 + " " + user.Petlvl1 + " " + user.Petstat2 + " " + user.Petlvl2 + " " + user.Petstat3 + " " + user.Petlvl3 +
                "\nDesc1: " + user.Desc1 + "\nDesc2: " + user.Desc2 + "\nDesc3: " + user.Desc3);

            i = 0;

            foreach (var c in user.Classes)
            {
                Console.WriteLine("Class: " + i + "\nName: " + c.ClassName + " Lvl: " + c.Lvl + "\nClass Quest Completed: " + c.CQC + " Fame: " + c.Fame +
                    "\nEquipments Weapon: " + c.Eq1 + "\nAbility: " + c.Eq2 + "\nArmour: " + c.Eq3 + "\nRing: " + c.Eq4 + "\nBackpack: " + c.Backpack + "\nStats: " + c.Stats);
                i++;
            }
            Console.ReadKey();

            // Starts a new instance of the program itself
            System.Diagnostics.Process.Start("Tester.exe");

            // Closes the current process
            Environment.Exit(0);
            
        */
            #endregion
            

            string server = FindKey("lh");

            int price = Convert.ToInt32(server.Split(' ').Last());
            server = server.Substring(0, server.IndexOf(" "));
            Console.WriteLine("server: " + server + " Price: " + price);
            
            
            Console.ReadKey();
        }
        
    }
}
