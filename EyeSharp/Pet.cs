using HtmlAgilityPack;

namespace EyeSharp;

/// <summary>
/// A <see cref="User"/>'s pet
/// </summary>
public class Pet
{
    /// <summary>
    /// What pet it is
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Pet rarity
    /// </summary>
    public PetRarity Rarity { get; }
    /// <summary>
    /// Pet family
    /// </summary>
    public PetFamily Family { get; }
    /// <summary>
    /// First unlocked ability
    /// </summary>
    public PetAbility Ability1 { get; }
    /// <summary>
    /// Second unlocked ability
    /// </summary>
    public PetAbility Ability2 { get; }
    /// <summary>
    /// Third unlocked ability
    /// </summary>
    public PetAbility Ability3 { get; }
    /// <summary>
    /// Max potential level
    /// </summary>
    public int MaxLevel { get; }
    
    internal Pet(HtmlNode row)
    {
        Name = row.SelectSingleNode("td[2]").InnerText;
        Rarity = ToPetRarity(row.SelectSingleNode("td[3]").InnerText);
        Family = ToPetFamily(row.SelectSingleNode("td[4]").InnerText);

        Ability1 = new PetAbility(row.SelectSingleNode("td[6]").InnerText, row.SelectSingleNode("td[7]").InnerText);
        Ability2 = new PetAbility(row.SelectSingleNode("td[8]").InnerText, row.SelectSingleNode("td[9]").InnerText);
        Ability3 = new PetAbility(row.SelectSingleNode("td[10]").InnerText, row.SelectSingleNode("td[11]").InnerText);

        if (int.TryParse(row.SelectSingleNode("td[11]").InnerText, out var level)) MaxLevel = level;
    }

    private PetRarity ToPetRarity(string input)
    {
        return input switch
        {
            "Common" => PetRarity.Common,
            "Uncommon" => PetRarity.Uncommon,
            "Rare" => PetRarity.Rare,
            "Legendary" => PetRarity.Legendary,
            "Divine" => PetRarity.Divine,
            _ => PetRarity.Common
        };
    }

    private PetFamily ToPetFamily(string input)
    {
        return input switch
        {
            "Avian" => PetFamily.Avian,
            "Humanoid" => PetFamily.Humanoid,
            "Reptile" => PetFamily.Reptile,
            "Automation" => PetFamily.Automation,
            "Insect" => PetFamily.Insect,
            "Penguin" => PetFamily.Penguin,
            _ => PetFamily.Avian
        };
    }
}

/// <summary>
/// A pet ability
/// </summary>
public class PetAbility
{
    internal PetAbility(string type, string level)
    {
        Type = ToPetAbilityType(type);
        Level = int.TryParse(level, out var lvl) ? lvl : -1;
    }

    /// <summary>
    /// Ability type
    /// </summary>
    public PetAbilityType Type { get; }
    /// <summary>
    /// Ability level
    /// </summary>
    public int Level { get; }

    private PetAbilityType ToPetAbilityType(string input)
    {
        return input switch
        {
            "Heal" => PetAbilityType.Heal,
            "Electric" => PetAbilityType.Electric,
            "Attack Far" => PetAbilityType.AttackFar,
            "Magic Heal" => PetAbilityType.MagicHeal,
            "Attack Mid" => PetAbilityType.AttackMid,
            "Savage" => PetAbilityType.Savage,
            "Rising Fury" => PetAbilityType.RisingFury,
            "Attack Close" => PetAbilityType.AttackClose,
            _ => PetAbilityType.None
        };
    }
}

/// <summary>
/// Pet rarity
/// </summary>
public enum PetRarity
{
    Common,
    Uncommon,
    Rare,
    Legendary,
    Divine
}

/// <summary>
/// Pet ability type
/// </summary>
public enum PetAbilityType
{
    None,
    Heal,
    Electric,
    AttackFar,
    MagicHeal,
    AttackMid,
    Savage,
    RisingFury,
    AttackClose
}

/// <summary>
/// The pet family
/// </summary>
public enum PetFamily
{
    Avian,
    Humanoid,
    Reptile,
    Automation,
    Insect,
    Penguin
}