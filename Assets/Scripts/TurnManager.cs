using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurnManager : MonoBehaviour
{
    public enum TurnPhases { MOVE, ATTACK, END };

    int currentTurn;
    TurnPhases currentTurnPhase;

    public TextMeshProUGUI turnDetails;
    public Button unselectButton;

    private static TurnManager instance;

    public static TurnManager Instance
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

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = 1;
        currentTurnPhase = TurnPhases.MOVE;

        UpdateTurnUI();
    }

    public void ChangeTurn()
    {
        if (currentTurn == 1)
            currentTurn += 1;
        else
            currentTurn -= 1;

        currentTurnPhase = TurnPhases.MOVE;

        unselectButton.gameObject.SetActive(true);
    }

    public bool IsPieceTurn(int pieceTurn)
    {
        if (pieceTurn == currentTurn)
            return true;

        return false;
    }

    public TurnPhases GetTurnPhase()
    {
        return currentTurnPhase;
    }

    public void NextTurnPhase()
    {
        currentTurnPhase++;

        if (currentTurnPhase == TurnPhases.ATTACK)
            unselectButton.gameObject.SetActive(false);

        if (currentTurnPhase == TurnPhases.END)
            ChangeTurn();

        UpdateTurnUI();
    }

    void UpdateTurnUI()
    {
        turnDetails.text = "Current Turn: " + currentTurn + "\nCurrent Turn Phase: " + currentTurnPhase.ToString();
    }

}
