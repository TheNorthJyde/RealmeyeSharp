using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;


namespace RealmeyeSharp
{
    public class Realm
    {
        public static bool result = false;
        /// <summary>
        /// Will get you User: name, char amount, skin count, rank, fame, accdame, guild, created
        /// </summary>
        /// <param name="IGN"></param>
        /// <returns></returns>
        public static bool GetUserSummary(string IGN, User user)
        {
            result = false;
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;
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
                            user.Chars =  cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Skins")
                        {
                            user.Skins = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Fame")
                        {
                            user.Fame = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Rank")
                        {
                            user.Rank = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Account fame")
                        {
                            user.AccFame = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Guild")
                        {
                            user.Guild = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Guild Rank")
                        {
                            user.GuildRank = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Created")
                        {
                            user.Created = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "First seen")
                        {
                            user.Created = cell.NextSibling.InnerText;
                        }
                    }
                }
                result = true;
            }
            catch (Exception)
            {
                user.Name = "Private";
                
            }
            return result;
        }

        /// <summary>
        /// will get you User: petname, pet stats, pet lvls
        /// </summary>
        /// <param name="user"></param>
        public static bool GetUserPetStats(User user)
        {
            result = false;
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

        /// <summary>
        /// will get you User: description
        /// </summary>
        /// <param name="user"></param>
        public static bool GetUserDescription(User user)
        {
            result = false;
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
                    result = true;
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

            return result;
        }

        /// <summary>
        /// will get you all User classes
        /// </summary>
        /// <param name="user"></param>
        public static bool GetUserClasses(User user)
        {
            result = false;
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
                            eq1 = row.SelectSingleNode("td[9]").FirstChild.FirstChild.Attributes[1].Value;
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
                    result = true;
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
            return result;
        }

        /// <summary>
        /// gets all user info 2 sec faster
        /// </summary>
        /// <param name="IGN"></param>
        /// <returns></returns>
        public static bool GetAllUserInfo(string IGN, User user)
        {
            result = false;
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;
            //main link
            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/player/" + IGN));
                HtmlNode Username = Main.Html.CssSelect(".entity-name").First();
                user.Name = Username.InnerText;
                var Table = Main.Html.CssSelect(".summary").First();
                //gets user summary
                try
                {
                    foreach (var row in Table.SelectNodes("tr"))
                    {
                        foreach (var cell in row.SelectNodes("td[1]"))
                        {
                            if (cell.InnerText == "Characters")
                            {
                                user.Chars = cell.NextSibling.InnerText;
                            }
                            else if (cell.InnerText == "Skins")
                            {
                                user.Skins = cell.NextSibling.InnerText;
                            }
                            else if (cell.InnerText == "Fame")
                            {
                                user.Fame = cell.NextSibling.InnerText;
                            }
                            else if (cell.InnerText == "Rank")
                            {
                                user.Rank = cell.NextSibling.InnerText;
                            }
                            else if (cell.InnerText == "Account fame")
                            {
                                user.AccFame = cell.NextSibling.InnerText;
                            }
                            else if (cell.InnerText == "Guild")
                            {
                                user.Guild = cell.NextSibling.InnerText;
                            }
                            else if (cell.InnerText == "Guild Rank")
                            {
                                user.GuildRank = cell.NextSibling.InnerText;
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

                }
                result = true;
                //gets user disc
                try
                {
                    Table = Main.Html.CssSelect("#d").First();
                    user.Desc1 = Table.FirstChild.InnerText;
                    user.Desc2 = Table.FirstChild.NextSibling.InnerText;
                    user.Desc3 = Table.FirstChild.NextSibling.NextSibling.InnerText;
                }
                catch (Exception)
                {

                }
                //gets user classes
                try
                {
                    
                    Table = Main.Html.CssSelect(".table-responsive").First().LastChild;
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
                            eq1 = row.SelectSingleNode("td[9]").FirstChild.FirstChild.Attributes[1].Value;
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

                }
                
                
            }
            catch (Exception)
            {
                user.Name = "Private";
                user.Desc1 = "Private";
                user.Desc2 = "Private";
                user.Desc3 = "Private";
            }

            //pet link
            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/pets-of/" + user.Name));
                var Table = Main.Html.CssSelect(".table-responsive").First();
                var PetTable = Table.LastChild;

                foreach (var row in PetTable.SelectNodes("tbody/tr"))
                {
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

            return result;
        }
        
        /// <summary>
        /// Will get you Guild: name, char amount, fame, most active on and description.
        /// </summary>
        /// <param name="guildName"></param>
        /// <param name="guild"></param>
        /// <returns></returns>
        public static bool GetGuildSummary(string guildName, Guild guild)
        {
            guildName = guildName.Replace(" ", "%20");
            result = false;
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;
            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/guild/" + guildName));
                HtmlNode Username = Main.Html.CssSelect(".entity-name").First();
                guild.Name = Username.InnerText;
                try
                {
                    var Table1 = Main.Html.CssSelect("#d").First();
                    guild.Desc1 = Table1.FirstChild.InnerText;
                    guild.Desc2 = Table1.FirstChild.NextSibling.InnerText;
                    guild.Desc3 = Table1.FirstChild.NextSibling.NextSibling.InnerText;
                }
                catch
                {
                    guild.Desc1 = "Private";
                    guild.Desc2 = "Private";
                    guild.Desc3 = "Private";
                }

                var Table2 = Main.Html.CssSelect(".summary").First();

                foreach (var row in Table2.SelectNodes("tr"))
                {
                    foreach (var cell in row.SelectNodes("td[1]"))
                    {
                        if (cell.InnerText == "Members")
                        {
                            guild.MemberCount = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Characters")
                        {
                            guild.Chars = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Fame")
                        {
                            guild.Fame = cell.NextSibling.InnerText;
                        }
                        else if (cell.InnerText == "Most active on")
                        {
                            guild.MostActiveOn = cell.NextSibling.InnerText;
                        }
                    }
                }
                result = true;
            }
            catch (Exception)
            {
                guild.Name = "Private";
            }
            return result;
        }
        
        /// <summary>
        /// will get you all Guild members
        /// </summary>
        /// <param name="guild"></param>
        /// <returns></returns>
        public static bool GetGuildMembers(Guild guild)
        {
            string guildName = guild.Name;
            int offset = 0;
            if (guildName.Contains(' '))
            {
                guildName = guildName.Replace(" ", "%20");
                offset = 1;
            }
            result = false;
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;

            ObservableCollection<Member> member = new ObservableCollection<Member>();
            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/guild/" + guildName));
                Console.WriteLine("https://www.realmeye.com/guild/" + guildName);
                var Table = Main.Html.CssSelect(".table-responsive").First().LastChild;

                foreach (var row in Table.SelectNodes("tbody/tr"))
                {
                    guild.Members.Add(new Member(
                        row.SelectSingleNode($"td[{1 + offset}]").InnerText,
                        row.SelectSingleNode($"td[{2 + offset}]").InnerText,
                        int.Parse(row.SelectSingleNode($"td[{3 + offset}]").InnerText),
                        int.Parse(row.SelectSingleNode($"td[{5 + offset}]").InnerText),
                        int.Parse(row.SelectSingleNode($"td[{6 + offset}]").InnerText)
                        ));
                }
                result = true;
            }
            catch (Exception)
            {
                guild.Name = "Private";
            }
            return result;
        }
        
        /// <summary>
        /// will get you all mystery boxes
        /// </summary>
        /// <param name="mysteryBoxes"></param>
        /// <returns></returns>
        public static bool GetAllMysteryBoxes(List<MysteryBox> mysteryBoxes)
        {
            ScrapingBrowser browser = new ScrapingBrowser
            {
                AllowAutoRedirect = true,
                AllowMetaRedirect = true
            };
            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/items/mystery-boxes"));
                var Boxes = Main.Html.CssSelect(".col-md-12").First();
                foreach (var Box in Boxes.CssSelect(".well"))
                {
                    MysteryBox box = new MysteryBox
                    {
                        Name = Box.SelectSingleNode("h3").InnerHtml.Split('"')[0].Split('<')[0].Replace("&apos;", "'"),
                        Price = Box.SelectSingleNode("h3/span").InnerText,
                        EndsAt = Box.SelectSingleNode("small/span").InnerText
                    };
                    foreach (var Prize in Box.CssSelect(".prize"))
                    {
                        MPrize prize = new MPrize();
                        if (Box.CssSelect(".prize.jackpot") != null)
                        {
                            prize.Jackpot = true;
                        }
                        else
                        {
                            prize.Jackpot = false;
                        }
                        foreach (var Item in Prize.CssSelect(".item"))
                        {
                            MItem item = new MItem
                            {
                                Name = Item.ParentNode.InnerHtml.Split('"')[3],
                                Quantity = "1"
                            };
                            if (Prize.CssSelect(".item-quantity-static") != null)
                            {
                                foreach (var ItemQ in Prize.CssSelect(".item-quantity-static"))
                                {
                                    item.Quantity = ItemQ.InnerText;
                                }
                            }
                            prize.Items.Add(item);
                        }
                        box.Prizes.Add(prize);
                    }
                    mysteryBoxes.Add(box);
                }
                result = true;
            }
            catch (Exception e)
            {
                
            }
            return result;
        }
        
    }
}
