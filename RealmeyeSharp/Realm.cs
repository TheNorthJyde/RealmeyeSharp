using System;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;


namespace RealmeyeSharp
{
    public class Realm
    {
        public static void GetUserInfo(string IGN)
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
                User.Name = "Private or non-existent";
                User.Chars = "Private";
                User.Skins = "Private";
                User.Fame = "Private";
                User.Rank = "Private";
                User.AccFame = "Private";
                User.Guild = "Private";
                User.Created = "Private";
            }

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
    }
}
