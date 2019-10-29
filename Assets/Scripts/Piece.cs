using UnityEngine;

public class Piece : MonoBehaviour
{
    Vector3 PIECE_OFFSET = new Vector3(0.5f, 0f, 0.5f);
    // Current X,Y coords in the board
    Vector2 currentPos;

    int layerMask;
    bool selected;

    public int turnPiece;

    public CreatureData creatureData;

    public PieceStats venusaurTest;
    public PieceStats gengarTest;


    void Start()
    {
        currentPos.x = transform.position.x;
        currentPos.y = transform.position.z;
        selected = false;

        BoardManager.Instance.SetStartOccupation(Mathf.FloorToInt(currentPos.x), Mathf.FloorToInt(currentPos.y));
    }

    private void Update()
    {
        if (!selected)
            return;

        if (selected && TurnManager.Instance.GetTurnPhase() == TurnManager.TurnPhases.MOVE)
            MovePhase();

        if (selected && TurnManager.Instance.GetTurnPhase() == TurnManager.TurnPhases.ATTACK)
            AttackPhase();
    }

    void OnMouseOver()
    {
        //ToggleUI(true);

        if(Input.GetMouseButtonUp(1) && TurnManager.Instance.IsPieceTurn(turnPiece) && TurnManager.Instance.GetTurnPhase() == TurnManager.TurnPhases.MOVE)
        {
            PlayerManager.Instance.SelectPiece(gameObject);
        }
    }

    /*void OnMouseExit()
    {
        ToggleUI(false);
    }*/

    void MovePhase()
    {
        ShowPossibleMovements();

        if (Input.GetMouseButtonUp(0))
        {
            layerMask = 1 << 9;
            // Line below makes ray ignore only this layer (right now only collides with layer 9)
            //layerMask = ~layerMask;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (BoardManager.Instance.CheckCorrectMove(creatureData, (int)currentPos.x, (int)currentPos.y, Mathf.FloorToInt(hit.point.x), Mathf.FloorToInt(hit.point.z)))
                {
                    transform.position = hit.point;
                    FixPosition();
                    TurnManager.Instance.NextTurnPhase();
                }
            }
        }
    }

    void AttackPhase()
    {

    }

    /*void ToggleUI(bool toggle)
    {

    }*/

    /*private void OnMouseDrag()
    {
        if (TurnManager.Instance.GetTurnPhase() != TurnManager.TurnPhases.MOVE || !TurnManager.Instance.IsPieceTurn(turnPiece))
            return;

        layerMask = 1 << 9;
        // Line below makes ray ignore only this layer (right now only collides with layer 9)
        //layerMask = ~layerMask;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            transform.position = hit.point;
        }
    }*/

    /*private void OnMouseUp()
    {
        if(Mathf.Floor(transform.position.x) + 0.5f == currentPos.x && Mathf.Floor(transform.position.z) + 0.5f == currentPos.y)
        {
            //No hay turno
            FixPosition();
            return;
        }

        if (BoardManager.Instance.CheckCorrectMove(creatureData, (int)currentPos.x, (int)currentPos.y, Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z)))
        {
            FixPosition();
            TurnManager.Instance.NextTurnPhase();
            return;
        }

        transform.position = new Vector3(currentPos.x, 0, currentPos.y);
    }*/

    void FixPosition()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(Mathf.Floor(pos.x), 0, Mathf.Floor(pos.z)) + PIECE_OFFSET;
        currentPos.x = transform.position.x;
        currentPos.y = transform.position.z;
    }

    void ShowPossibleMovements()
    {
        Vector3 piecePos = new Vector3(currentPos.x, 0, currentPos.y) - PIECE_OFFSET;
        BoardManager.Instance.ShowPossibleMovements(piecePos, creatureData);
    }

    public void ToggleSelected(bool sel)
    {
        selected = sel;
    }
}
