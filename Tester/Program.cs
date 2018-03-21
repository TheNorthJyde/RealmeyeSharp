using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealmeyeSharp;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("In Game Name: ");
                var IGN = Console.ReadLine();
                Realm.GetUserSummary(IGN);
                Realm.GetUserPetStats(IGN);
                Realm.GetUserDescription(IGN);
                Console.WriteLine("Name: " + User.Name + 
                    "\nCharacters: " + User.Chars + 
                    "\nSkins: " + User.Skins + 
                    "\nFame: " + User.Fame + 
                    "\nRank: " + User.Rank + 
                    "\nAccount fame: " + User.AccFame + 
                    "\nGuild: " + User.Guild + 
                    "\nCreated: " + User.Created + 
                    "\nPet name: " + User.PetName + 
                    "\nPet stats: " + User.Petstat1 + " " + User.Petlvl1 + " " + User.Petstat2 + " " + User.Petlvl2 + " " + User.Petstat3 + " " + User.Petlvl3 + 
                    "\nDesc1: " + User.Desc1 + "\nDesc2: " + User.Desc2 + "\nDesc3: " + User.Desc3);
            }


        }
        
    }
}
