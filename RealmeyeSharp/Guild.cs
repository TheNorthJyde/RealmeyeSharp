using System.Collections.ObjectModel;

namespace RealmeyeSharp
{
    public class Guild
    {
        public string Name { get; set; }
        public string MemberCount { get; set; }
        public string Chars { get; set; }
        public string Fame { get; set; }
        public string MostActiveOn { get; set; }
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string Desc3 { get; set; }
        public ObservableCollection<Member> Members { get; set; } = new ObservableCollection<Member>();

        public Guild() { }
    }
    
    public class Member
    {
        public string Name { get; set; }
        public string GuildRank { get; set; }
        public int Fame { get; set; }
        public int Rank { get; set; }
        public int Chars { get; set; }

        public Member(string name, string guildRank, int fame, int rank, int chars)
        {
            Name = name;
            GuildRank = guildRank;
            Fame = fame;
            Rank = rank;
            Chars = chars;
        }
    }
}
