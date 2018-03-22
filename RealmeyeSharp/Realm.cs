using System;
using System.Collections.ObjectModel;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;


namespace RealmeyeSharp
{
    public class Realm
    {
        /// <summary>
        /// Will get you User: name, char amount, skin count, rank, fame, accdame, guild, created
        /// </summary>
        /// <param name="IGN"></param>
        /// <returns></returns>
        public static User GetUserSummary(string IGN)
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;
            User user = new User();
            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/player/" + IGN));
                HtmlNode Username = Main.Html.CssSelect(".entity-name").First();
                user.Name = Username.InnerText;

                var Table = Main.Html.CssSelect(".summary").First();

                foreach (var row in Table.SelectNodes("tr"))
                {
                    foreach (var cell in row.SelectNodes("td[1]"))
                    {
                        if (cell.InnerText == "Characters")
                        {
                            user.Chars =  Convert.ToInt32(cell.NextSibling.InnerText);
                        }
                        else if (cell.InnerText == "Skins")
                        {
                            user.Skins = Convert.ToInt32(cell.NextSibling.InnerText);
                        }
                        else if (cell.InnerText == "Fame")
                        {
                            user.Fame = Convert.ToInt32(cell.NextSibling.InnerText);
                        }
                        else if (cell.InnerText == "Rank")
                        {
                            user.Rank = Convert.ToInt32(cell.NextSibling.InnerText);
                        }
                        else if (cell.InnerText == "Account fame")
                        {
                            user.AccFame = Convert.ToInt32(cell.NextSibling.InnerText);
                        }
                        else if (cell.InnerText == "Guild")
                        {
                            user.Guild = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Created")
                        {
                            user.Created = cell.NextSibling.InnerText;
                        }
                    }
                }
            }
            catch (Exception)
            {
                user.Name = "Private";
                
            }
            return user;
        }

        /// <summary>
        /// will get you User: petname, pet stats, pet lvls
        /// </summary>
        /// <param name="user"></param>
        public static void GetUserPetStats(User user)
        {
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
                        user.PetName = row.SelectSingleNode("td[2]").InnerText;
                        user.Petstat1 = row.SelectSingleNode("td[6]").InnerText;
                        user.Petlvl1 = Convert.ToInt32(row.SelectSingleNode("td[7]").InnerText);
                        user.Petstat2 = row.SelectSingleNode("td[8]").InnerText;
                        user.Petlvl2 = Convert.ToInt32(row.SelectSingleNode("td[9]").InnerText);
                        user.Petstat3 = row.SelectSingleNode("td[10]").InnerText;
                        user.Petlvl3 = Convert.ToInt32(row.SelectSingleNode("td[11]").InnerText);
                        break;
                    }
                }
                catch (Exception)
                {
                    user.PetName = "Private";
                    user.Petstat1 = "Private";
                    user.Petstat2 = "Private";
                    user.Petstat3 = "Private";
                    user.Petlvl1 = 0;
                    user.Petlvl2 = 0;
                    user.Petlvl3 = 0;
                }
            }
            else
            {
                Console.WriteLine("you either havent gotten summary or your user is private");
            }

        }

        /// <summary>
        /// will get you User: description
        /// </summary>
        /// <param name="user"></param>
        public static void GetUserDescription(User user)
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;

            if (user.Name != null && user.Name != "Private")
            {
                try
                {
                    WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/player/" + user.Name));
                    var Table = Main.Html.CssSelect("#d").First();
                    user.Desc1 = Table.FirstChild.InnerText;
                    user.Desc2 = Table.FirstChild.NextSibling.InnerText;
                    user.Desc3 = Table.FirstChild.NextSibling.NextSibling.InnerText;
                }
                catch (Exception)
                {
                    user.Desc1 = "Private";
                    user.Desc2 = "Private";
                    user.Desc3 = "Private";
                }
            }
            else
            {
                Console.WriteLine("you either havent gotten summary or your user is private");
            }

        }

        /// <summary>
        /// will get you all User classes
        /// </summary>
        /// <param name="user"></param>
        public static void GetUserClasses(User user)
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;

            ObservableCollection<Class> classes = new ObservableCollection<Class>();
            if (user.Name != null && user.Name != "Private")
            {
                try
                {
                    WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/player/" + user.Name));
                    var Table = Main.Html.CssSelect(".table-responsive").First().LastChild;

                    foreach (var row in Table.SelectNodes("tbody/tr"))
                    {
                        bool t = false;
                        string eq1;
                        string eq2;
                        string eq3;
                        string eq4;

                        if (row.SelectSingleNode("td[9]").ChildNodes.Count == 5)
                        {
                            t = true;
                        }
                        //eq1
                        if (row.SelectSingleNode("td[9]").FirstChild.FirstChild.ChildNodes.Count == 1)
                        {
                            eq1 = row.SelectSingleNode("td[9]").FirstChild.FirstChild.FirstChild.Attributes[1].Value;
                        }
                        else
                        {
                            eq1 = row.SelectSingleNode("td[9]").FirstChild.FirstChild.FirstChild.Attributes[1].Value;
                        }
                        //eq2
                        if (row.SelectSingleNode("td[9]").FirstChild.NextSibling.FirstChild.ChildNodes.Count == 1)
                        {
                            eq2 = row.SelectSingleNode("td[9]").FirstChild.NextSibling.FirstChild.FirstChild.Attributes[1].Value;
                        }
                        else
                        {
                            eq2 = row.SelectSingleNode("td[9]").FirstChild.NextSibling.FirstChild.Attributes[1].Value;
                        }
                        //eq3
                        if (row.SelectSingleNode("td[9]").FirstChild.NextSibling.NextSibling.FirstChild.ChildNodes.Count == 1)
                        {
                            eq3 = row.SelectSingleNode("td[9]").FirstChild.NextSibling.NextSibling.FirstChild.FirstChild.Attributes[1].Value;
                        }
                        else
                        {
                            eq3 = row.SelectSingleNode("td[9]").FirstChild.NextSibling.NextSibling.FirstChild.Attributes[1].Value;
                        }
                        //eq4
                        if (row.SelectSingleNode("td[9]").FirstChild.NextSibling.NextSibling.NextSibling.FirstChild.ChildNodes.Count == 1)
                        {
                            eq4 = row.SelectSingleNode("td[9]").FirstChild.NextSibling.NextSibling.NextSibling.FirstChild.FirstChild.Attributes[1].Value;
                        }
                        else
                        {
                            eq4 = row.SelectSingleNode("td[9]").FirstChild.NextSibling.NextSibling.NextSibling.FirstChild.Attributes[1].Value;
                        }

                        user.Classes.Add(new Class(row.SelectSingleNode("td[3]").InnerText, Convert.ToInt32(row.SelectSingleNode("td[4]").InnerText),
                            row.SelectSingleNode("td[5]").InnerText, Convert.ToInt32(row.SelectSingleNode("td[6]").InnerText), eq1, eq2, eq3, eq4, t,
                            row.SelectSingleNode("td[10]").InnerText));
                    }
                }
                catch (Exception)
                {

                    classes.Add(new Class("Private", 0, "0/5", 0, "Empty slot", "Empty slot", "Empty slot", "Empty slot", false, "0/8"));
                }
                
            }
            else
            {
                Console.WriteLine("you either havent gotten summary or your user is private");
            }
        }
    }
}
