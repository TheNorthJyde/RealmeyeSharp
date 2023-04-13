using System.Collections.ObjectModel;

namespace EyeSharp;

/// <summary>
/// Guild object containing information about a RotMG guild
/// Get it using <see cref="RealmEyeClient.GetGuildAsync"/>
/// </summary>
public class Guild
{
    /// <summary>
    /// Guild name
    /// </summary>
    public string Name { get; internal set; }
    /// <summary>
    /// Guild member count
    /// </summary>
    public string MemberCount { get; internal set; }
    /// <summary>
    /// Total amount of characters across all members
    /// </summary>
    public string Chars { get; internal set; }
    /// <summary>
    /// Total guild fame
    /// </summary>
    public string Fame { get; internal set; }
    /// <summary>
    /// Server this guild is most active on, and their position on leaderboard
    /// </summary>
    public string MostActiveOn { get; internal set; }
    /// <summary>
    /// The Guild's RealmEye description
    /// </summary>
    public Description Description { get; internal set; }
    /// <summary>
    /// List of all the members
    /// </summary>
    public ObservableCollection<GuildMember> Members { get; internal set; } = new();

    internal Guild()
    {
    }
    
    internal static GuildRank ToGuildRank(string input)
    {
        return input switch
        {
            "Member" => GuildRank.Member,
            "Officer" => GuildRank.Officer,
            "Leader" => GuildRank.Leader,
            "Founder" => GuildRank.Founder,
            _ => GuildRank.Member
        };
    }
}

/// <summary>
/// A Guild Member
/// </summary>
public class GuildMember
{
    /// <summary>
    /// In-game name
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Their Guild Rank
    /// </summary>
    public GuildRank GuildRank { get; }
    /// <summary>
    /// Account Fame
    /// </summary>
    public int Fame { get; }
    /// <summary>
    /// How many stars they have
    /// </summary>
    public int Stars { get; }
    /// <summary>
    /// Amount of characters they have
    /// </summary>
    public int Chars { get; }

    internal GuildMember(string name, string guildRank, int fame, int stars, int chars)
    {
        Name = name;
        GuildRank = Guild.ToGuildRank(guildRank);
        Fame = fame;
        Stars = stars;
        Chars = chars;
    }
}

/// <summary>
/// Guild Ranks
/// </summary>
public enum GuildRank
{
    Member,
    Officer,
    Leader,
    Founder
}