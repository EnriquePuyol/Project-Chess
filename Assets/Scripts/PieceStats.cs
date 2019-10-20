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

    int AbilityDamage(int power)
    {
        float finalPower = (float)power / 100.0f;
        int finalDamage = Mathf.FloorToInt(creatureData.attackDamage * finalPower);

        return finalDamage;
    }

    void ReceiveAbilityDamage(int damage, AttackData data)
    {
        int finalDamage = 0;

        int resistance = creatureData.resistance;
        float damageMod = data.GetTypeMatchUp(creatureData.creatureType);

        if (data.HasFlag(AttackData.AttackFlags.IgnoreResistances))
        {
            resistance = 0;
        }
        if (data.HasFlag(AttackData.AttackFlags.IgnoreType))
        {
            damageMod = 1;
        }
        
        finalDamage = Mathf.Clamp(Mathf.FloorToInt(damage * damageMod) - resistance, 1, 100000);

        currentHealth -= finalDamage;
    }
}
