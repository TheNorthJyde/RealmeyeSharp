using System.Collections.ObjectModel;

namespace EyeSharp
{
    /// <summary>
    /// A RealmEye User
    /// </summary>
    public class User
    {
        /// <summary>
        /// In-game name
        /// </summary>
        public string Name { get; internal set; }
        /// <summary>
        /// RealmEye description
        /// </summary>
        public Description Description { get; internal set; }
        /// <summary>
        /// Amount of characters
        /// </summary>
        public int Chars { get; internal set; }
        /// <summary>
        /// Amount of skins
        /// </summary>
        public int Skins { get; internal set; }
        /// <summary>
        /// Amount of fame
        /// </summary>
        public int Fame { get; internal set; }
        /// <summary>
        /// Amount of stars
        /// </summary>
        public int Stars { get; internal set; }
        /// <summary>
        /// Account fame
        /// </summary>
        public int AccFame { get; internal set; }
        /// <summary>
        /// Guild name
        /// </summary>
        public string Guild { get; internal set; }
        /// <summary>
        /// Guild rank
        /// </summary>
        public GuildRank GuildRank { get; internal set; }
        /// <summary>
        /// Time since creation
        /// </summary>
        public string Created { get; internal set; }
        /// <summary>
        /// Characters that RealmEye has spotted
        /// </summary>
        public ObservableCollection<Character> Characters { get; } = new();

        internal User()
        {
        }
    }
}