using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace EyeSharp;

/// <summary>
/// Client to interact with RealmEye
/// </summary>
public class RealmEyeClient
{
    private readonly Uri _player = new("https://www.realmeye.com/player/");
    private readonly Uri _pets = new("https://www.realmeye.com/pets-of/");
    private readonly Uri _guild = new("https://www.realmeye.com/guild/");

    private async Task<WebPage> DoRequest(Uri uri)
    {
        var browser = new ScrapingBrowser
        {
            AllowAutoRedirect = true,
            AllowMetaRedirect = true,
            Headers =
            {
                {
                    "Accept",
                    "*/*"
                },
                { "Accept-Encoding", "gzip, deflate, br" },
                { "Accept-Language", "en-US,en;q=0.9,sv-SE;q=0.8,sv;q=0.7,la;q=0.6,tg;q=0.5" },
                {
                    "User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36"
                }
            }
        };
        
        var page = await browser.NavigateToPageAsync(uri);

        return page;
    }

    /// <summary>
    /// Fetches a user from RealmEye and populates a <see cref="User"/> object
    /// </summary>
    /// <param name="ign">RotMG in-game name</param>
    /// <returns><see cref="User"/> or null if invalid</returns>
    public async Task<User> GetUserAsync(string ign)
    {
        var user = new User();
        var description = new Description();

        var page = await DoRequest(_player.Combine(ign));
        var username = page.Html.CssSelect(".entity-name").First();
        user.Name = username.InnerText;

        /* Get base information */

        var table = page.Html.CssSelect(".summary").First();

        foreach (var row in table.SelectNodes("tr"))
        {
            foreach (var cell in row.SelectNodes("td[1]"))
                switch (cell.InnerText)
                {
                    case "Characters":
                        user.Chars = int.TryParse(cell.NextSibling.InnerText, out var chars) ? chars : 0;
                        break;
                    case "Skins":
                        user.Skins = int.TryParse(cell.NextSibling.InnerText, out var skins) ? skins : 0;
                        break;
                    case "Fame":
                        user.Fame = int.TryParse(cell.NextSibling.InnerText, out var fame) ? fame : 0;
                        break;
                    case "Rank":
                        user.Stars = int.TryParse(cell.NextSibling.InnerText, out var stars) ? stars : 0;
                        break;
                    case "Account fame":
                        user.AccFame = int.TryParse(cell.NextSibling.InnerText, out var accFame) ? accFame : 0;
                        break;
                    case "Guild":
                        user.Guild = cell.NextSibling.InnerText;
                        break;
                    case "Guild Rank":
                        user.GuildRank = Guild.ToGuildRank(cell.NextSibling.InnerText);
                        break;
                    case "Created":
                    case "First seen":
                        user.Created = cell.NextSibling.InnerText;
                        break;
                }
        }

        /* Get description */
        table = page.Html.CssSelect("#d").First();
        if (table.HasClass("close"))
            table = page.Html.CssSelect("#e").First();
        
        description.Desc1 = table.FirstChild.InnerText;
        description.Desc2 = table.FirstChild.NextSibling.InnerText;
        description.Desc3 = table.FirstChild.NextSibling.NextSibling.InnerText;

        user.Description = description;

        /* Get classes */

        table = page.Html.CssSelect(".table-responsive").First().LastChild;

        foreach (var row in table.SelectNodes("tbody/tr"))
        {
            var t = row.SelectSingleNode("td[7]").ChildNodes.Count == 5;

            var weapon = FixItemString(row.SelectSingleNode("td[7]").FirstChild.FirstChild.ChildNodes.Count == 1
                ? row.SelectSingleNode("td[7]").FirstChild.FirstChild.FirstChild.Attributes[1].Value
                : row.SelectSingleNode("td[7]").FirstChild.FirstChild.Attributes[1].Value);

            var ability = FixItemString(row.SelectSingleNode("td[7]").FirstChild.NextSibling.FirstChild.ChildNodes.Count == 1
                ? row.SelectSingleNode("td[7]").FirstChild.NextSibling.FirstChild.FirstChild.Attributes[1].Value
                : row.SelectSingleNode("td[7]").FirstChild.NextSibling.FirstChild.Attributes[1].Value);

            var armor = FixItemString(row.SelectSingleNode("td[7]").FirstChild.NextSibling.NextSibling.FirstChild.ChildNodes.Count ==
                                      1
                    ? row.SelectSingleNode("td[7]").FirstChild.NextSibling.NextSibling.FirstChild.FirstChild
                        .Attributes[1].Value
                    : row.SelectSingleNode("td[7]").FirstChild.NextSibling.NextSibling.FirstChild.Attributes[1]
                        .Value);

            var ring = FixItemString(row.SelectSingleNode("td[7]").FirstChild.NextSibling.NextSibling.NextSibling.FirstChild
                .ChildNodes
                .Count == 1
                ? row.SelectSingleNode("td[7]").FirstChild.NextSibling.NextSibling.NextSibling.FirstChild
                    .FirstChild.Attributes[1].Value
                : row.SelectSingleNode("td[7]").FirstChild.NextSibling.NextSibling.NextSibling.FirstChild
                    .Attributes[1].Value);

            var name = row.SelectSingleNode("td[3]").InnerText;
            var lvl = Convert.ToInt32(row.SelectSingleNode("td[4]").InnerText);
            var fame = Convert.ToInt32(row.SelectSingleNode("td[5]").InnerText);
            var equipment = new ClassEquipment(weapon, ability, armor, ring);
            var stats = row.SelectSingleNode("td[8]").InnerText;
            
            user.Characters.Add(new Character(name,
                lvl,
                fame,
                equipment, t,
                stats));
        }
        
        return user;
    }

    private string FixItemString(string itemName)
    {
        return itemName.Replace("&apos;", "'");
    }

    /// <summary>
    /// Fetches a users pets and returns them in a readonly list
    /// </summary>
    /// <param name="user">RealmEye user</param>
    /// <returns>Readonly List with pets. Empty if none could be found.</returns>
    public async Task<IReadOnlyList<Pet>> GetUserPetsAsync(User user)
    {
        if (user == null) return new ReadOnlyCollection<Pet>(new List<Pet>());

        return await GetPets(user.Name);
    }

    /// <summary>
    /// Fetches a users pets and returns them in a readonly list
    /// </summary>
    /// <param name="ign">RotMG in-game name</param>
    /// <returns>Readonly List with pets. Empty if none could be found.</returns>
    public async Task<IReadOnlyList<Pet>> GetUserPetsAsync(string ign)
    {
        if (string.IsNullOrEmpty(ign)) return new ReadOnlyCollection<Pet>(new List<Pet>());
        
        return await GetPets(ign);
    }

    private async Task<IReadOnlyList<Pet>> GetPets(string ign)
    {
        List<Pet> pets = new();
        try
        {
            var page = await DoRequest(_pets.Combine(ign));
            var table = page.Html.CssSelect(".table-responsive").First();
            var petTable = table.LastChild;

            pets.AddRange(petTable.SelectNodes("tbody/tr").Select(row => new Pet(row)));
        }
        catch (Exception)
        {
            return new ReadOnlyCollection<Pet>(new List<Pet>());
        }

        return pets.AsReadOnly();
    }

    /// <summary>
    /// Fetches a Guild using it's name
    /// </summary>
    /// <param name="guildName">The guild's name</param>
    /// <returns>A <see cref="Guild"/> object. Null if not found.</returns>
    public async Task<Guild> GetGuildAsync(string guildName)
    {
        if (string.IsNullOrWhiteSpace(guildName))
            return null;
        
        return await GetGuild(guildName);
    }
    
    /// <summary>
    /// Fetches a users guild
    /// </summary>
    /// <param name="user">The user whose guild to fetch</param>
    /// <returns>A <see cref="Guild"/> object. Null if not found.</returns>
    public async Task<Guild> GetGuildAsync(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Guild))
            return null;
        
        return await GetGuild(user.Guild);
    }

    private async Task<Guild> GetGuild(string guildName)
    {
        var guild = new Guild();
        var description = new Description();
        var offset = 0;

        if (guildName.Contains(' '))
        {
            guildName = guildName.Replace(" ", "%20");
            offset = 1;
        }

        var page = await DoRequest(_guild.Combine(guildName));
        var name = page.Html.CssSelect(".entity-name").First();
        guild.Name = name.InnerText;
            
        var table = page.Html.CssSelect("#d").First();
        if (table.HasClass("close"))
            table = page.Html.CssSelect("#e").First();
        
        description.Desc1 = table.FirstChild.InnerText;
        description.Desc2 = table.FirstChild.NextSibling.InnerText;
        description.Desc3 = table.FirstChild.NextSibling.NextSibling.InnerText;

        guild.Description = description;

        table = page.Html.CssSelect(".summary").First();

        foreach (var row in table.SelectNodes("tr"))
        {
            foreach (var cell in row.SelectNodes("td[1]"))
                switch (cell.InnerText)
                {
                    case "Members":
                        guild.MemberCount = cell.NextSibling.InnerText;
                        break;
                    case "Characters":
                        guild.Chars = cell.NextSibling.InnerText;
                        break;
                    case "Fame":
                        guild.Fame = cell.NextSibling.InnerText;
                        break;
                    case "Most active on":
                        guild.MostActiveOn = cell.NextSibling.InnerText;
                        break;
                }
        }

        table = page.Html.CssSelect(".table-responsive").First().LastChild;
        guild.Members = new ObservableCollection<GuildMember>();

        foreach (var row in table.SelectNodes("tbody/tr"))
            guild.Members.Add(new GuildMember(
                row.SelectSingleNode($"td[{1 + offset}]").FirstChild.InnerText,
                row.SelectSingleNode($"td[{2 + offset}]").InnerText,
                int.Parse(row.SelectSingleNode($"td[{3 + offset}]").FirstChild.InnerText),
                int.Parse(row.SelectSingleNode($"td[{4 + offset}]").InnerText),
                int.Parse(row.SelectSingleNode($"td[{5 + offset}]").InnerText)
            ));

        return guild;
    }
}