using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BallManagerScript : MonoBehaviour
{
    //References
    [SerializeField] private GameObject ballPrefab;
    private GameObject _scoreManagerObj;
    private ScoreManager _sm;
    
    public static BallManagerScript Instance { get; private set; }
    [Range(0, 5)] public int numBalls;
    public int lastRoundWinner = 2;

    
    private void Awake()
    {
        //Singleton Pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        _scoreManagerObj = GameObject.FindWithTag("ScoreManager");
        _sm = _scoreManagerObj.GetComponent<ScoreManager>();
    }

    private void Start() {
        RoundStart();
    }

    private void RoundStart(int startBalls = 1) {
        numBalls = startBalls;
        Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
    }

    public void RemoveBall(int ballValue = 0, int playerNum = 0) {
        numBalls--;

        if (playerNum == 1) {
            _sm.AddScore1(ballValue);
            lastRoundWinner = 1;
        } else if (playerNum == 2) {
            _sm.AddScore2(ballValue);
            lastRoundWinner = 2;
        }

        if (numBalls <= 0) {
            RoundStart();
        }
    }
}
