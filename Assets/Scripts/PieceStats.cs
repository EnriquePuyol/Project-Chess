using UnityEngine;
using UnityEngine.UI;

public class PieceStats : MonoBehaviour
{
    public CreatureData creatureData;

    int currentHealth;
    int currentMana;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = creatureData.health;
        currentMana = creatureData.startingMana;

        UpdateUI();
    }

    void UpdateUI()
    {
        float h = (float)currentHealth / (float)creatureData.health;
        float m = (float)currentMana / (float)creatureData.mana;

        GetComponent<uiPiece>().UpdateUI(h, m);
    }

}
