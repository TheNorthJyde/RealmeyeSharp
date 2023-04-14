using System.Collections.Generic;
using System.Linq;

namespace EyeSharp;

public class Exaltation
{
    public CharacterClass Class { get; }
    public int Exaltations { get; }
    public IReadOnlyList<ExaltedStat> ExaltedStats { get; }

    internal Exaltation(string className, int exaltations, params ExaltedStat[] stats)
    {
        Class = Character.ToCharacterClass(className);
        Exaltations = exaltations;
        ExaltedStats = stats.ToList().AsReadOnly();
    }
}

public class UserExaltations
{
    public IReadOnlyList<Exaltation> Exaltations { get; internal set; }
    public int TotalExaltations { get; internal set; }
    public double PercentageDone { get; internal set; }

    internal UserExaltations()
    {
    }
}

public class ExaltedStat
{
    public ExaltStatType StatType { get; }
    public int IncreasedBy { get; }

    internal ExaltedStat(ExaltStatType type, int increasedBy)
    {
        StatType = type;
        IncreasedBy = increasedBy;
    }
}

public enum ExaltStatType
{
    Health,
    Mana,
    Attack,
    Defense,
    Speed,
    Dexterity,
    Vitality,
    Wisdom
}