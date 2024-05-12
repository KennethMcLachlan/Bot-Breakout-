using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is NULL");
            }
            return _instance;
        }
    }

    [SerializeField] private TMP_Text _score;

    private void Awake()
    {
        {
            _instance = this;   
        }
    }

    public void UpdateScore(int playerScore)
    {
        //Make a switch statement to update the score depending on which enemy was defeated
        _score.text = playerScore.ToString();
    }
}
