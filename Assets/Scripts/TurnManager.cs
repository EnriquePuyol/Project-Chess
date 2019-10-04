using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    int currentTurn;

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
    }

    public void ChangeTurn()
    {
        if (currentTurn == 1)
            currentTurn += 1;
        else
            currentTurn -= 1;
    }

    public bool IsPieceTurn(int pieceTurn)
    {
        if (pieceTurn == currentTurn)
            return true;

        return false;
    }

}
