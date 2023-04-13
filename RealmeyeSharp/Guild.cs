using System.Collections.ObjectModel;

namespace EyeSharp;

public class Guild
{
    public string Name { get; internal set; }
    public string MemberCount { get; internal set; }
    public string Chars { get; internal set; }
    public string Fame { get; internal set; }
    public string MostActiveOn { get; internal set; }
    public Description Description { get; internal set; }
    public ObservableCollection<GuildMember> Members { get; internal set; } = new();

    internal Guild()
    {
    }
}

public class GuildMember
{
    public string Name { get; }
    public string GuildRank { get; }
    public int Fame { get; }
    public int Rank { get; }
    public int Chars { get; }

    internal GuildMember(string name, string guildRank, int fame, int rank, int chars)
    {
        Name = name;
        GuildRank = guildRank;
        Fame = fame;
        Rank = rank;
        Chars = chars;
    }
}