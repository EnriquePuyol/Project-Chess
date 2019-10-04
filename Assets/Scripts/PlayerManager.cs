using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum turnPhases { Move, Attack};

    public turnPhases turnPhase;

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
}
