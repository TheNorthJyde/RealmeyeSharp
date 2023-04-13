using System.Collections.ObjectModel;

namespace EyeSharp
{
    public class User
    {
        public string Name { get; internal set; }
        public Description Description { get; internal set; }
        public string Chars { get; internal set; }
        public string Skins { get; internal set; }
        public string Fame { get; internal set; }
        public string Rank { get; internal set; }
        public string AccFame { get; internal set; }
        public string Guild { get; internal set; }
        public string GuildRank { get; internal set; }
        public string Created { get; internal set; }
        public ObservableCollection<Class> Classes { get; } = new();

        internal User()
        {
        }
    }

    public class Class
    {
        internal Class(string classname, string imageUrl, int lvl, int fame, ClassEquipment equipment, bool backpack,
            string stats)
        {
            ClassName = classname;
            ImageUrl = imageUrl;
            Lvl = lvl;
            Fame = fame;
            Equipment = equipment;
            Backpack = backpack;
            Stats = stats;
        }

        public string ClassName { get; }
        public string ImageUrl { get; }
        public int Lvl { get; }
        public int Fame { get; }
        public ClassEquipment Equipment { get; }
        public bool Backpack { get; }
        public string Stats { get; }
    }

    public class ClassEquipment
    {
        internal ClassEquipment(string weapon, string ability, string armor, string ring)
        {
            Weapon = weapon;
            Ability = ability;
            Armor = armor;
            Ring = ring;
        }

        public string Weapon { get; }
        public string Ability { get; }
        public string Armor { get; }
        public string Ring { get; }
    }
}