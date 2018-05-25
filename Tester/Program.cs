using RealmeyeSharp;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            
            User user = new User();
            Console.Write("In Game Name: ");
            var IGN = Console.ReadLine();
            user = Realm.GetUserSummary(IGN);
            Realm.GetUserPetStats(user);
            Realm.GetUserDescription(user);
            Realm.GetUserClasses(user);

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
            Console.ReadKey();
        }
        
    }
}
