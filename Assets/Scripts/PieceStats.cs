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

    int BasicDamage()
    {
        return creatureData.attackDamage;
    }

    void ReceiveBasicDamage(int damage)
    {
        int finalDamage = Mathf.Clamp(damage - creatureData.armor, 1, 100000);

        currentHealth -= finalDamage;
    }

    void AbilityDamage(int power)
    {
        int finalDamage = 0;
        float finalPower = (float)power / 100.0f;

        finalDamage = Mathf.FloorToInt(creatureData.attackDamage * finalPower);
    }

    void ReceiveAbilityDamage(int damage)
    {
        int finalDamage = Mathf.Clamp(damage - creatureData.resistance, 1, 100000);

        currentHealth -= finalDamage;
    }
}
