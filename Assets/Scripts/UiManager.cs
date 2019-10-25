using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{

    public TextMeshProUGUI creatureName;
    public TextMeshProUGUI healthDetail;
    public TextMeshProUGUI manaDetail;

    [SerializeField]
    Animator anim;

    private static UiManager instance;

    public static UiManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null & instance != this)
            Destroy(this.gameObject);

        instance = this;
    }

    public void Select(string name, string health, string mana)
    {
        creatureName.text = name;
        healthDetail.text = health;
        manaDetail.text = mana;

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Show"))
            anim.SetTrigger("Show");
    }

    public void UnSelect()
    {
        anim.SetTrigger("Hide");
    }
}
