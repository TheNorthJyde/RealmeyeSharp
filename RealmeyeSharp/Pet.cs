using HtmlAgilityPack;

namespace EyeSharp;

public class Pet
{
    public string Name { get; }
    public PetRarity Rarity { get; }
    public PetFamily Family { get; }
    public PetAbility Ability1 { get; }
    public PetAbility Ability2 { get; }
    public PetAbility Ability3 { get; }
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

public class PetAbility
{
    internal PetAbility(string type, string level)
    {
        Type = ToPetAbilityType(type);
        Level = int.TryParse(level, out var lvl) ? lvl : -1;
    }

    public PetAbilityType Type { get; }
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

public enum PetRarity
{
    Common,
    Uncommon,
    Rare,
    Legendary,
    Divine
}

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

public enum PetFamily
{
    Avian,
    Humanoid,
    Reptile,
    Automation,
    Insect,
    Penguin
}