namespace EyeSharp;

/// <summary>
    /// A RotMG character
    /// </summary>
    public class Character
    {
        /// <summary>
        /// What class the character is
        /// </summary>
        public CharacterClass Class { get; }
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
            Class = ToCharacterClass(classname);
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

        internal static CharacterClass ToCharacterClass(string input)
        {
            return input.ToUpper() switch
            {
                "WARRIOR" => CharacterClass.Warrior,
                "PALADIN" => CharacterClass.Paladin,
                "ROGUE" => CharacterClass.Rogue,
                "PRIEST" => CharacterClass.Priest,
                "ARCHER" => CharacterClass.Archer,
                "WIZARD" => CharacterClass.Wizard,
                "NECROMANCER" => CharacterClass.Necromancer,
                "KNIGHT" => CharacterClass.Knight,
                "ASSASSIN" => CharacterClass.Assassin,
                "HUNTRESS" => CharacterClass.Huntress,
                "MYSTIC" => CharacterClass.Mystic,
                "TRICKSTER" => CharacterClass.Trickster,
                "SORCERER" => CharacterClass.Sorcerer,
                "NINJA" => CharacterClass.Ninja,
                "BARD" => CharacterClass.Bard,
                "SAMURAI" => CharacterClass.Samurai,
                "SUMMONER" => CharacterClass.Summoner,
                "KENSEI" => CharacterClass.Kensei,  
                _ => CharacterClass.Unknown
            };
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
            Name = htmlInput.Substring(0, htmlInput.Length - 3);
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
                "T7" => ItemRarity.T7,
                "T8" => ItemRarity.T8,
                "T9" => ItemRarity.T9,
                "T10" => ItemRarity.T10,
                "T11" => ItemRarity.T11,
                "T12" => ItemRarity.T12,
                "T13" => ItemRarity.T13,
                "T14" => ItemRarity.T14,
                "ST" => ItemRarity.St,
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
        T7,
        T8,
        T9,
        T10,
        T11,
        T12,
        T13,
        T14,
        St,
        Ut
    }

public enum CharacterClass
{
    Unknown,
    Rogue,
    Archer,
    Wizard,
    Priest,
    Warrior,
    Knight,
    Paladin,
    Assassin,
    Necromancer,
    Huntress,
    Mystic,
    Trickster,
    Sorcerer,
    Ninja,
    Samurai,
    Bard,
    Summoner,
    Kensei
}