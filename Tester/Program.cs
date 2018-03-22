using RealmeyeSharp;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            Console.Write("In Game Name: ");
                
            var IGN = Console.ReadLine();
            ObservableCollection<Class> classes = Realm.GetUserClasses(IGN);
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
            int i = 0;
            foreach (var c in classes)
            {
                Console.WriteLine("Class: " + i + "\nName: " + c.ClassName + " Lvl: " + c.Lvl + "\nClass Quest Completed: " + c.CQC + " Fame: " + c.Fame +
                    "\nEquipment 1: " + c.Eq1 + " 2: " + c.Eq2 + " 3: " + c.Eq3 + " 4: " + c.Eq4 + "\nBackpack: " + c.Backpack + " Stats: " + c.Stats);
                i++;
            }
            Console.ReadKey();
        }
        
    }
}
