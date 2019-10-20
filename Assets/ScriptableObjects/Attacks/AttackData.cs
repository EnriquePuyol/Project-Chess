using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class AttackData : ScriptableObject
{
    [System.Flags]
    public enum AttackFlags
    {
        None                = 0,
        IgnoreResistances   = 1 << 0,
        IgnoreType          = 1 << 1,
        MultiplyOnSameType  = 1 << 2
    };

    public enum AbilityThrowTypes { Radius, Line, Objective, Diagonal };
    public enum AbilityTypes { Nature, Water, Light, Darkness, Mental, Corruption, TOTAL };

    public new string name;

    public int power;

    public AttackFlags flags;
    public AbilityThrowTypes thrownType;
    public AbilityTypes abilityType;

    public bool HasFlag (AttackFlags flag)
    {
        // Checks if it has the flag
        if ((flags & flag) == flag)
            return true;

        return false;
    }

    public float GetTypeMatchUp(CreatureData.CreatureTypes creatureType)
    {
        if(abilityType == AbilityTypes.Nature)
        {
            if (creatureType == CreatureData.CreatureTypes.Corruption)
                return 0.5f;
            else if (creatureType == CreatureData.CreatureTypes.Water)
                return 2f;
            else
                return 1f;
        }
        else if(abilityType == AbilityTypes.Corruption)
        {
            if (creatureType == CreatureData.CreatureTypes.Mental)
                return 0.5f;
            else if (creatureType == CreatureData.CreatureTypes.Nature)
                return 2f;
            else
                return 1f;
        }
        else
        {
            int weakType   = (int)abilityType - 1;
            int strongType = (int)abilityType + 1;
            int creatType  = (int)creatureType;

            if (creatType == weakType)
                return 0.5f;
            else if (creatType == strongType)
                return 2f;
            else
                return 1f;
        }
    }

}
