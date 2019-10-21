using UnityEngine;
using UnityEngine.UI;

public class PieceStats : MonoBehaviour
{
    public CreatureData creatureData;
    public AttackData attackData;

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

    public void ReceiveBasicDamage(int damage)
    {
        int finalDamage = Mathf.Clamp(damage - creatureData.armor, 1, 100000);

        currentHealth -= finalDamage;
    }

    public int AbilityDamage(int power)
    {
        int finalDamage = Mathf.FloorToInt(creatureData.attackDamage + power);

        return finalDamage;
    }

    public void ReceiveAbilityDamage(int damage, AttackData data)
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
        
        finalDamage = Mathf.Clamp(damage - Mathf.FloorToInt(resistance / damageMod), 1, 100000);
        currentHealth -= finalDamage;

        UpdateUI();
    }
}
