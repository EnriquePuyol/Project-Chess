using UnityEngine;

public class Piece : MonoBehaviour
{
    Vector3 PIECE_OFFSET = new Vector3(0.5f, 0f, 0.5f);
    // Current X,Y coords in the board
    Vector2 currentPos;

    public CreatureData creatureData;

    int layerMask;

    int turnPiece;
    int currentHealth;

    void Start()
    {
        currentPos.x = transform.position.x;
        currentPos.y = transform.position.z;

        BoardManager.Instance.SetStartOccupation(Mathf.FloorToInt(currentPos.x), Mathf.FloorToInt(currentPos.y));
    }

    private void OnMouseOver()
    {
        ShowPossibleMovements();
    }

    private void OnMouseDrag()
    {
        layerMask = 1 << 9;
        // Line below makes ray ignore only this layer (right now only collides with layer 9)
        //layerMask = ~layerMask;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            transform.position = hit.point;
        }
    }

    private void OnMouseUp()
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
            return;
        }

        transform.position = new Vector3(currentPos.x, 0, currentPos.y);
    }

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
}
