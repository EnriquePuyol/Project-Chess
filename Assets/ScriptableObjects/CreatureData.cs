using UnityEngine;

[CreateAssetMenu(fileName = "New Creature", menuName = "Creature")]
public class CreatureData : ScriptableObject
{
    public enum MovementTypes { Cruceta, Reina, Diagonal, Salto };

    public new string name;

    public MovementTypes movementType;

    public int movementLimit;

}
