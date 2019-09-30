using UnityEngine;

[CreateAssetMenu(fileName = "New Cell", menuName = "Cell")]
public class Cell : ScriptableObject
{
    public new string name;

    public int x_coord;
    public int y_coord;

    public bool isEmpty;
}
