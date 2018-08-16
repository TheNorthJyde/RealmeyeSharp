using System;
using System.Collections.ObjectModel;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace RealmeyeSharp
{
    public class Find
    {
        public static bool result = false;
        /// <summary>
        /// will find keys
        /// </summary>
        /// <param name="IGN"></param>
        /// <returns></returns>
        public static string FindKey(string keyName)
        {
            string result = "";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;
            try
            {
                Key key = new Key();
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/items/keys"));

                var Table = Main.Html.CssSelect(".table-responsive").First().LastChild;
                Table = Table.SelectSingleNode("tbody/tr[" + key.keyList[keyName.ToLower()] + "]");
                result = Table.SelectSingleNode("td[3]").InnerText;
                result += " " + Table.SelectSingleNode("td[4]").InnerText;
            }
            catch (Exception)
            {
                result = "could not find key";

            }
            return result;
        }

        public static string FindBackpack()
        {
            string result = "";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;
            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/items/misc"));

                var Table = Main.Html.CssSelect(".table-responsive").First().LastChild;
                Table = Table.SelectSingleNode("tbody/tr[2]");
                result = Table.SelectSingleNode("td[3]").InnerText;
                result += " " + Table.SelectSingleNode("td[4]").InnerText;
            }
            catch (Exception)
            {
                result = "could not find backpack";

            }
            return result;
        }
      
        public static string FindClover()
        {
            string result = "";
            ScrapingBrowser browser = new ScrapingBrowser();
            browser.AllowAutoRedirect = true;
            browser.AllowMetaRedirect = true;
            try
            {
                WebPage Main = browser.NavigateToPage(new Uri("https://www.realmeye.com/items/misc"));

                var Table = Main.Html.CssSelect(".table-responsive").First().LastChild;
                Table = Table.SelectSingleNode("tbody/tr[1]");
                result = Table.SelectSingleNode("td[3]").InnerText;
                result += " " + Table.SelectSingleNode("td[4]").InnerText;
            }
            catch (Exception)
            {
                result = "could not find clover";

            }
            return result;
        }
    }
}
