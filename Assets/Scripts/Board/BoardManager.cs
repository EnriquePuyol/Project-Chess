using UnityEngine;

public class BoardManager : MonoBehaviour
{
    const float TILE_SIZE   = 1.0f;

    const int SIZE_X = 6;
    const int SIZE_Y = 6;

    //int selectionX = -1;
    //int selectionY = -1;

    [SerializeField]
    private Cell[] cells;

    private static BoardManager instance;

    public static BoardManager Instance
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

        for(int i = 0; i < cells.Length; i++)
        {
            cells[i].isEmpty = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DrawChessBoard();
    }

    void DrawChessBoard()
    {
        Vector3 widthLine = Vector3.right * SIZE_X;
        Vector3 heightLine = Vector3.forward * SIZE_Y;

        for(int i = 0; i <= SIZE_X; i++)
        {
            Vector3 lineStart = Vector3.forward * i;
            Debug.DrawLine(lineStart, lineStart + widthLine);

            for (int j = 0; j <= SIZE_Y; j++)
            {
                lineStart = Vector3.right * i;
                Debug.DrawLine(lineStart, lineStart + heightLine);
            }
        }
    }

    void PreSelectCell(float selectedX, float selectedY)
    {
        Debug.DrawLine(Vector3.forward * selectedY + Vector3.right * selectedX,
                       Vector3.forward * (selectedY +1) + Vector3.right * (selectedX + 1));

        Debug.DrawLine(Vector3.forward * (selectedY + 1) + Vector3.right * selectedX,
                       Vector3.forward * selectedY + Vector3.right * (selectedX + 1));
    }

    public Vector2 GetBoardLimits()
    {
        return new Vector2(SIZE_X, SIZE_Y);
    }

    public void ShowPossibleMovements(Vector3 currentPos, CreatureData data)
    {
        int moveLimit = data.movementLimit;
        int currentX = (int)currentPos.x;
        int currentY = (int)currentPos.z;

        if (data.movementType == CreatureData.MovementTypes.Cruceta)
        {
            int minX = (int)Mathf.Clamp(currentX - moveLimit, 0f, SIZE_X - 1);
            int maxX = (int)Mathf.Clamp(currentX + moveLimit, 0f, SIZE_X - 1);

            int minY = (int)Mathf.Clamp(currentY - moveLimit, 0f, SIZE_Y - 1);
            int maxY = (int)Mathf.Clamp(currentY + moveLimit, 0f, SIZE_Y - 1);

            for (int i = minX; i <= maxX; i++)
            {
                if(i != currentX)
                {
                    PreSelectCell(i, currentY);
                }
            }
            for (int j = minY; j <= maxY; j++)
            {
                if(j != currentY)
                {
                    PreSelectCell(currentX, j);
                }
            }
        }
        else if(data.movementType == CreatureData.MovementTypes.Diagonal)
        {
            int minX = (int)Mathf.Clamp(currentX - moveLimit, 0f, SIZE_X - 1);
            int maxX = (int)Mathf.Clamp(currentX + moveLimit, 0f, SIZE_X - 1);

            int pre_Y = 0;
            int sum = -1;

            int vertical = moveLimit;
            if(currentX - moveLimit < 0)
            {
                vertical = currentX;
            }

            for(int i = minX; i <= maxX; i++)
            {
                if (i == currentX)
                {
                    sum = 1;
                    vertical = 1;
                    continue;
                }

                // Limite inferior
                pre_Y = currentY - vertical;
                if(pre_Y >= 0)
                {
                    PreSelectCell(i, pre_Y);
                }

                // Limite superior
                pre_Y = currentY + vertical;
                if(pre_Y < 8)
                {
                    PreSelectCell(i, pre_Y);
                }

                vertical += sum;
            }
        }
        else if (data.movementType == CreatureData.MovementTypes.Reina)
        {
            int minX = (int)Mathf.Clamp(currentX - moveLimit, 0f, SIZE_X - 1);
            int maxX = (int)Mathf.Clamp(currentX + moveLimit, 0f, SIZE_X - 1);

            int minY = (int)Mathf.Clamp(currentY - moveLimit, 0f, SIZE_Y - 1);
            int maxY = (int)Mathf.Clamp(currentY + moveLimit, 0f, SIZE_Y - 1);

            for (int i = minX; i <= maxX; i++)
            {
                if (i != currentX)
                {
                    PreSelectCell(i, currentY);
                }
            }
            for (int j = minY; j <= maxY; j++)
            {
                if (j != currentY)
                {
                    PreSelectCell(currentX, j);
                }
            }

            int pre_Y = 0;
            int sum = -1;

            int vertical = moveLimit;
            if (currentX - moveLimit < 0)
            {
                vertical = currentX;
            }

            for (int i = minX; i <= maxX; i++)
            {
                if (i == currentX)
                {
                    sum = 1;
                    vertical = 1;
                    continue;
                }

                // Limite inferior
                pre_Y = currentY - vertical;
                if (pre_Y >= 0)
                {
                    PreSelectCell(i, pre_Y);
                }

                // Limite superior
                pre_Y = currentY + vertical;
                if (pre_Y < 8)
                {
                    PreSelectCell(i, pre_Y);
                }

                vertical += sum;
            }
        }
        else if (data.movementType == CreatureData.MovementTypes.Salto)
        {
            int minX = (int)Mathf.Clamp(currentX - moveLimit, 0f, SIZE_X - 1);
            int maxX = (int)Mathf.Clamp(currentX + moveLimit, 0f, SIZE_X - 1);

            int minY = (int)Mathf.Clamp(currentY - moveLimit, 0f, SIZE_Y - 1);
            int maxY = (int)Mathf.Clamp(currentY + moveLimit, 0f, SIZE_Y - 1);

            for (int i = minX; i <= maxX; i++)
            {
                if (i == currentX)
                    continue;

                if (currentY - 1 >= 0)
                {
                    PreSelectCell(i, currentY - 1);
                }
                if(currentY + 1 < 8)
                {
                    PreSelectCell(i, currentY + 1);
                }
            }
            for (int j = minY; j <= maxY; j++)
            {
                if (j == currentY)
                    continue;

                if (currentX - 1 >= 0)
                {
                    PreSelectCell(currentX - 1, j);
                }
                if(currentX + 1 < 8)
                {
                    PreSelectCell(currentX + 1, j);
                }
            }
        }
    }

    public bool CheckCorrectMove(CreatureData data, int currentX, int currentY, int nextX, int nextY)
    {
        if(data.movementType == CreatureData.MovementTypes.Cruceta)
        {
            if(currentX != nextX && currentY == nextY)
            {
                int diff = Mathf.Abs(currentX - nextX);

                if(diff <= data.movementLimit)
                {
                    if (CheckEmptyMovement(data, currentX, currentY, nextX, nextY))
                    {
                        ChangeOccupation(currentX, currentY, nextX, nextY);
                        return true;
                    }
                }
            }
            else if(currentX == nextX && currentY != nextY)
            {
                int diff = Mathf.Abs(currentY - nextY);

                if (diff <= data.movementLimit)
                {
                    if (CheckEmptyMovement(data, currentX, currentY, nextX, nextY))
                    {
                        ChangeOccupation(currentX, currentY, nextX, nextY);
                        return true;
                    }
                }
            }
        }
        else if (data.movementType == CreatureData.MovementTypes.Diagonal)
        {
            if(currentX != nextX && currentY != nextY)
            {
                int diffX = Mathf.Abs(currentX - nextX);
                int diffY = Mathf.Abs(currentY - nextY);

                if (diffX == diffY && diffX <= data.movementLimit)
                    return true;
            }
        }
        else if (data.movementType == CreatureData.MovementTypes.Reina)
        {
            if (currentX != nextX && currentY == nextY)
            {
                int diff = Mathf.Abs(currentX - nextX);

                if (diff <= data.movementLimit)
                    return true;
            }
            else if (currentX == nextX && currentY != nextY)
            {
                int diff = Mathf.Abs(currentY - nextY);

                if (diff <= data.movementLimit)
                    return true;
            }
            else if (currentX != nextX && currentY != nextY)
            {
                int diffX = Mathf.Abs(currentX - nextX);
                int diffY = Mathf.Abs(currentY - nextY);

                if (diffX == diffY && diffX <= data.movementLimit)
                    return true;
            }
        }
        else if (data.movementType == CreatureData.MovementTypes.Salto)
        {
            int diffX = Mathf.Abs(currentX - nextX);
            int diffY = Mathf.Abs(currentY - nextY);

            if (currentX != nextX && diffY == 1 && diffX <= data.movementLimit)
                return true;

            if (currentY != nextY && diffX == 1 && diffY <= data.movementLimit)
                return true;
        }

        return false;
    }

    public bool CheckEmptyMovement(CreatureData data, int currentX, int currentY, int nextX, int nextY)
    {
        if(data.movementType == CreatureData.MovementTypes.Cruceta)
        {
            if(currentX != nextX)
            {
                if(currentX < nextX)
                {
                    for(int i = currentX + 1; i <= nextX; i++)
                    {
                        int pos = (currentY * 6) + i;

                        if (!cells[pos].isEmpty)
                            return false;
                    }
                }
                else
                {
                    for (int i = currentX - 1; i >= nextX; i--)
                    {
                        int pos = (currentY * 6) + i;

                        if (!cells[pos].isEmpty)
                            return false;
                    }
                }
            }
            else
            {
                if(currentY < nextY)
                {
                    for (int i = currentY + 1; i <= nextY; i++)
                    {
                        int pos = (i * 6) + currentX;

                        if (!cells[pos].isEmpty)
                            return false;
                    }
                }
                else
                {
                    for (int i = currentY - 1; i >= nextY; i--)
                    {
                        int pos = (i * 6) + currentX;

                        if (!cells[pos].isEmpty)
                            return false;
                    }
                }
            }
        }
        else if (data.movementType == CreatureData.MovementTypes.Diagonal)
        {

        }
        else if (data.movementType == CreatureData.MovementTypes.Reina)
        {

        }
        else if (data.movementType == CreatureData.MovementTypes.Cruceta)
        {

        }

        return true;
    }

    public void SetStartOccupation(int posX, int posY)
    {
        int pos = (posY * 6) + posX;
        cells[pos].isEmpty = false;
    }

    void ChangeOccupation(int oldX, int oldY, int newX, int newY)
    {
        int oldPos = (oldY * 6) + oldX;
        int newPos = (newY * 6) + newX;

        cells[oldPos].isEmpty = true;
        cells[newPos].isEmpty = false;
    }

}
