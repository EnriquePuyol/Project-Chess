using UnityEngine;

[CreateAssetMenu(fileName = "New Creature", menuName = "Creature")]
public class CreatureData : ScriptableObject
{
    public enum MovementTypes { Cross, Queen, Diagonal, Jump };
    public enum CreatureTypes { Nature, Water, Light, Darkness, Mental, Corruption, TOTAL};

    public new string name;

    public CreatureTypes creatureType;
    public MovementTypes movementType;

    public int movementLimit;

    public int health;
    public int mana;

    public int attackDamage;
    public int startingMana;
    public int armor;
    public int resistance;

    public int manaPerAttack;

}
