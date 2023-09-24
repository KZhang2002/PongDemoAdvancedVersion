using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    [SerializeField] private GameObject score1Obj;
    [SerializeField] private GameObject score2Obj;
    [SerializeField] private GameObject gameEndTextObj;

    private Text _score1Text;
    private Text _score2Text;
    private Text _gameEndText;

    public int score1;
    public int score2;

    [SerializeField] private int scoreThreshold = 10;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _score1Text = score1Obj.GetComponent<Text>();
        _score2Text = score2Obj.GetComponent<Text>();
        _gameEndText = gameEndTextObj.GetComponent<Text>();

        _gameEndText.enabled = false;
    }

    public void AddScore1(int points = 1)
    {
        score1 += points;
        _score1Text.text = score1.ToString();

        if (score1 >= scoreThreshold) {
            Debug.Log("Player 1 win threshold reached");
            DisplayWinText(1);
        }
    }
    
    public void AddScore2(int points = 1)
    {
        score2 += points;
        _score2Text.text = score2.ToString();

        if (score2 >= scoreThreshold) {
            Debug.Log("Player 2 win threshold reached");
            DisplayWinText(2);
        }
    }

    private void DisplayWinText(int playerNum) {
        _gameEndText.text = $"Player {playerNum} wins!";
        _gameEndText.enabled = true;
    }
}
