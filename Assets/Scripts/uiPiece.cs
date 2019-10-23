using UnityEngine;
using UnityEngine.UI;

public class uiPiece : MonoBehaviour
{
    public GameObject UI_prefab;

    private Image UI_Image;

    private const float Y_UI_OFFSET = 0.8f;

    void Awake()
    {
        UI_Image = Instantiate(UI_prefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        UI_Image.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * Y_UI_OFFSET);
    }

    public void UpdateUI(float healthBarFill, float manaBarFill)
    {
        UI_Image.GetComponent<uiBars>().SetBarsFill(healthBarFill, manaBarFill);
    }
}
