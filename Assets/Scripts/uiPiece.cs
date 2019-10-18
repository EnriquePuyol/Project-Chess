using UnityEngine;
using UnityEngine.UI;

public class uiPiece : MonoBehaviour
{
    public GameObject UI_prefab;

    private Image UI_Image;
    private Image healthBar, manaBar;

    private const float Y_UI_OFFSET = 0.8f;

    void Awake()
    {
        UI_Image = Instantiate(UI_prefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();

        Image[] images = UI_prefab.GetComponentsInChildren<Image>();

        for (int i = 0; i < images.Length; i++)
        {
            if (healthBar == null && images[i].tag == "HealthBar")
                healthBar = images[i];
            else if (manaBar == null && images[i].tag == "ManaBar")
                manaBar = images[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        UI_Image.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * Y_UI_OFFSET);
    }

    public void UpdateUI(float healthBarFill, float manaBarFill)
    {
        healthBar.fillAmount = healthBarFill;
        manaBar.fillAmount = manaBarFill;
    }

}
