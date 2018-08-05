namespace RealmeyeSharp
{
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
