using System.Collections.ObjectModel;

namespace RealmeyeSharp;

public class Guild
{
    public string Name { get; internal set; }
    public string MemberCount { get; internal set; }
    public string Chars { get; internal set; }
    public string Fame { get; internal set; }
    public string MostActiveOn { get; internal set; }
    public string Desc1 { get; internal set; }
    public string Desc2 { get; internal set; }
    public string Desc3 { get; internal set; }
    public ObservableCollection<Member> Members { get; internal set; } = new();

    internal Guild()
    {
    }
}

public class Member
{
    public string Name { get; }
    public string GuildRank { get; }
    public int Fame { get; }
    public int Rank { get; }
    public int Chars { get; }

    internal Member(string name, string guildRank, int fame, int rank, int chars)
    {
        Name = name;
        GuildRank = guildRank;
        Fame = fame;
        Rank = rank;
        Chars = chars;
    }
}