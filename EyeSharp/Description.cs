namespace EyeSharp;

/// <summary>
/// Contains the three rows of description a guild or user can have on their RealmEye profile
/// </summary>
public class Description
{
    /// <summary>
    /// Top row in RealmEye description
    /// </summary>
    public string Desc1 { get; internal set; }
    /// <summary>
    /// Middle row in RealmEye description
    /// </summary>
    public string Desc2 { get; internal set; }
    /// <summary>
    /// Bottom row in RealmEye description
    /// </summary>
    public string Desc3 { get; internal set; }
    
    internal Description()
    {
    }
}