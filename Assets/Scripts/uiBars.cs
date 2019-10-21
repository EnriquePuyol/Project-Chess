using UnityEngine;
using UnityEngine.UI;

public class uiBars : MonoBehaviour
{
    [SerializeField]
    Image healthBar;
    [SerializeField]
    Image manaBar;

    public void SetBarsFill(float healthFill, float manaFill)
    {
        healthBar.fillAmount    = healthFill;
        manaBar.fillAmount      = manaFill;
    }
}
