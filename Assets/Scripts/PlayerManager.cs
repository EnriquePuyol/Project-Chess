using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pieces;

    public int playerTurn;

    private GameObject selectedPiece;

    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);

        instance = this;
    }

    void Start()
    {
        SetPiecesTurn();
    }

    void SetPiecesTurn()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].GetComponent<Piece>().turnPiece = playerTurn;
        }
    }

    public void SelectPiece(GameObject selPiece)
    {
        if(selectedPiece != null)
            selectedPiece.GetComponent<Piece>().ToggleSelected(false);

        selPiece.GetComponent<Piece>().ToggleSelected(true);
        selectedPiece = selPiece;

        PieceStats stats = selectedPiece.GetComponent<PieceStats>();

        UiManager.Instance.Select(stats.creatureData.name, stats.currentHealth.ToString(), stats.currentMana.ToString());
    }

    public void UnSelectPiece()
    {
        selectedPiece.GetComponent<Piece>().ToggleSelected(false);
        selectedPiece = null;

        UiManager.Instance.UnSelect();
    }

}
