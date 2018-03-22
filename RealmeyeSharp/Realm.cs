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
        public static void GetUserSummary(string IGN)
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;

            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/player/" + IGN));
                HtmlNode Username = Main.Html.CssSelect(".entity-name").First();
                User.Name = Username.InnerText;

                var Table = Main.Html.CssSelect(".summary").First();

                foreach (var row in Table.SelectNodes("tr"))
                {
                    foreach (var cell in row.SelectNodes("td[1]"))
                    {
                        if (cell.InnerText == "Characters")
                        {
                            User.Chars = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Skins")
                        {
                            User.Skins = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Fame")
                        {
                            User.Fame = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Rank")
                        {
                            User.Rank = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Account fame")
                        {
                            User.AccFame = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Guild")
                        {
                            User.Guild = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Created")
                        {
                            User.Created = cell.NextSibling.InnerText;
                        }
                    }
                }
            }
            catch (Exception)
            {
                User.Name = "Private";
                User.Chars = "Private";
                User.Skins = "Private";
                User.Fame = "Private";
                User.Rank = "Private";
                User.AccFame = "Private";
                User.Guild = "Private";
                User.Created = "Private";
            }
        }

        public static void GetUserPetStats(string IGN)
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;

            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/pets-of/" + IGN));
                var Table = Main.Html.CssSelect(".table-responsive").First();
                var PetTable = Table.LastChild;

                foreach (var row in PetTable.SelectNodes("tbody/tr"))
                {
                    User.PetName = row.SelectSingleNode("td[2]").InnerText;
                    User.Petstat1 = row.SelectSingleNode("td[6]").InnerText;
                    User.Petlvl1 = row.SelectSingleNode("td[7]").InnerText;
                    User.Petstat2 = row.SelectSingleNode("td[8]").InnerText;
                    User.Petlvl2 = row.SelectSingleNode("td[9]").InnerText;
                    User.Petstat3 = row.SelectSingleNode("td[10]").InnerText;
                    User.Petlvl3 = row.SelectSingleNode("td[11]").InnerText;
                    break;
                }
            }
            catch (Exception)
            {
                User.PetName = "Private";
                User.Petstat1 = "Private";
                User.Petstat2 = "Private";
                User.Petstat3 = "Private";
                User.Petlvl1 = "Private";
                User.Petlvl2 = "Private";
                User.Petlvl3 = "Private";
            }
        }

        public static void GetUserDescription(string IGN)
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;

            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/player/" + IGN));
                var Table = Main.Html.CssSelect("#d").First();
                User.Desc1 = Table.FirstChild.InnerText;
                User.Desc2 = Table.FirstChild.NextSibling.InnerText;
                User.Desc3 = Table.FirstChild.NextSibling.NextSibling.InnerText;
            }
            catch (Exception)
            {
                User.Desc1 = "Private";
                User.Desc2 = "Private";
                User.Desc3 = "Private";
            }
        }

        public static ObservableCollection<Class> GetUserClasses(string IGN)
        {
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;

            ObservableCollection<Class> classes = new ObservableCollection<Class>();

            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/player/" + IGN));
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

                    classes.Add(new Class(row.SelectSingleNode("td[3]").InnerText, Convert.ToInt32(row.SelectSingleNode("td[4]").InnerText),
                        row.SelectSingleNode("td[5]").InnerText, Convert.ToInt32(row.SelectSingleNode("td[6]").InnerText), eq1, eq2, eq3, eq4, t,
                        row.SelectSingleNode("td[10]").InnerText));
                }
            }
            catch (Exception)
            {

                classes.Add(new Class("Private", 0, "0/5", 0, "Empty slot", "Empty slot", "Empty slot", "Empty slot", false, "0/8"));
            }
            
            return classes;
        }
    }
}
