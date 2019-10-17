using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pieces;

    public int playerTurn;

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

}
