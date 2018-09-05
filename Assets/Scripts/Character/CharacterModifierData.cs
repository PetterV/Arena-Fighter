[System.Serializable]
public class CharacterModifierData
{
    public CharacterModifierItem[] items;
}

[System.Serializable]
public class CharacterModifierItem
{
    public string type;
    public bool injury = false;
    public int attackImpact = 0;
    public int defenseImpact = 0;
    public int magicImpact = 0;
    public int strategyImpact = 0;
    public int showcreatureshipImpact = 0;
    public int comebackitudeImpact = 0;
}