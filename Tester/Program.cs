using RealmeyeSharp;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            
            User user = new User();
            Console.Write("In Game Name: ");
            var IGN = Console.ReadLine();
            Realm.GetUserSummary(IGN, user);
            GetUserPetStats(user);
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
        }
        public static bool GetUserPetStats(User user)
        {
            bool result = false;
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;
            if (user.Name != null && user.Name != "Private")
            {
                try
                {
                    WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/pets-of/" + user.Name));
                    var Table = Main.Html.CssSelect(".table-responsive").First();
                    var PetTable = Table.LastChild;

                    foreach (var row in PetTable.SelectNodes("tbody/tr"))
                    {
                        var node = row.SelectSingleNode("td[1]").FirstChild.Attributes["class"].Value;
                        
                        
                        user.PetName = row.SelectSingleNode("td[2]").InnerText;
                        user.Petstat1 = row.SelectSingleNode("td[6]").InnerText;
                        user.Petlvl1 = row.SelectSingleNode("td[7]").InnerText;
                        user.Petstat2 = row.SelectSingleNode("td[8]").InnerText;
                        user.Petlvl2 = row.SelectSingleNode("td[9]").InnerText;
                        user.Petstat3 = row.SelectSingleNode("td[10]").InnerText;
                        user.Petlvl3 = row.SelectSingleNode("td[11]").InnerText;
                        break;
                    }
                    result = true;
                }
                catch (Exception)
                {
                    user.PetName = "Private";
                    user.Petstat1 = "Private";
                    user.Petstat2 = "Private";
                    user.Petstat3 = "Private";
                    user.Petlvl1 = "0";
                    user.Petlvl2 = "0";
                    user.Petlvl3 = "0";
                }
            }
            else
            {
                Console.WriteLine("you either havent gotten summary or your user is private");
            }
            return result;
        }
    }
}
