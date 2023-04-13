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

    /// <summary>
    /// A RotMG character
    /// </summary>
    public class Character
    {
        /// <summary>
        /// What class the character is
        /// </summary>
        public string ClassName { get; }
        /// <summary>
        /// Current level out of 20
        /// </summary>
        public int Level { get; }
        /// <summary>
        /// Character fame
        /// </summary>
        public int Fame { get; }
        /// <summary>
        /// Currently equipped items
        /// </summary>
        public ClassEquipment Equipment { get; }
        /// <summary>
        /// If the character has a backpack equipped
        /// </summary>
        public bool HasBackpack { get; }
        /// <summary>
        /// How maxed the character is out of 8
        /// </summary>
        public int Maxed { get; }
        
        internal Character(string classname, int level, int fame, ClassEquipment equipment, bool hasBackpack,
            string stats)
        {
            ClassName = classname;
            Level = level;
            Fame = fame;
            Equipment = equipment;
            HasBackpack = hasBackpack;
            Maxed = TryGetMaxed(stats);
        }

        private int TryGetMaxed(string stats)
        {
            if (stats.Split('/').Length > 0)
                return int.TryParse(stats.Split('/')[0], out var stat) ? stat : 0;
            
            return 0;
        }
    }

    /// <summary>
    /// Equipped items on a character
    /// </summary>
    public class ClassEquipment
    {
        public Item Weapon { get; }
        public Item Ability { get; }
        public Item Armor { get; }
        public Item Ring { get; }
        
        internal ClassEquipment(string weapon, string ability, string armor, string ring)
        {
            Weapon = new Item(weapon);
            Ability = new Item(ability);
            Armor = new Item(armor);
            Ring = new Item(ring);
        }
    }

    /// <summary>
    /// A RotMG item
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Item name
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Item rarity, any of the tiers or UT
        /// </summary>
        public ItemRarity Rarity { get; }

        internal Item(string htmlInput)
        {
            var rarity = htmlInput.Remove(htmlInput.Length - 3);
            Name = htmlInput.Substring(htmlInput.Length - 3);
            Rarity = ToItemRarity(rarity);
        }

        private ItemRarity ToItemRarity(string input)
        {
            return input.ToUpper() switch
            {
                "T1" => ItemRarity.T1,
                "T2" => ItemRarity.T2,
                "T3" => ItemRarity.T3,
                "T4" => ItemRarity.T4,
                "T5" => ItemRarity.T5,
                "T6" => ItemRarity.T6,
                "UT" => ItemRarity.Ut,
                _ => ItemRarity.Ut
            };
        }
    }

    /// <summary>
    /// Rarity of a item
    /// </summary>
    public enum ItemRarity
    {
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        Ut
    }
}