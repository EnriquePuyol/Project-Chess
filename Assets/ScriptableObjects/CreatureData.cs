using UnityEngine;

[CreateAssetMenu(fileName = "New Creature", menuName = "Creature")]
public class CreatureData : ScriptableObject
{
    public enum MovementTypes { Cruceta, Reina, Diagonal, Salto };
    public enum CreatureTypes { Naturaleza, Agua, Luz, Oscuridad, Mental, Corrupcion, TOTAL};

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
