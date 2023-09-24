using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallScript : MonoBehaviour {
    //References
    private Rigidbody2D _rb;
    private BallManagerScript _bm;
    
    //Ball Traits
    [SerializeField] private int speed = 30;
    public int ballValue = 1;
    
    private Vector2 _lastDirection;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _bm = BallManagerScript.Instance;
    }
    
    private void Start() {
        StartCoroutine(ActivateBall(2));
    }
    
    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            _rb.transform.position = Vector2.zero;
            ServeBall();
        }
    }

    IEnumerator ActivateBall(float startDelay) {
        yield return new WaitForSecondsRealtime(startDelay);
        ServeBall();
    }

    private void ServeBall() {
        int x;
        
        if (_bm.lastRoundWinner == 1)
            x = 1;
        else 
            x = -1;
        
        float randomY = Random.Range(-0.7f, 0.7f);
        Vector2 serveDirection = new Vector2(x, randomY).normalized;
        _rb.velocity = serveDirection * speed;
        _lastDirection = serveDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Goal1")) {
            _bm.RemoveBall(ballValue, 1);
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Goal2")) {
            _bm.RemoveBall(ballValue, 2);
            Destroy(gameObject);
        }
        
        Vector2 collisionNormal = collision.GetContact(0).normal;
        Vector2 newDirection = Vector2.Reflect(_lastDirection, collisionNormal).normalized;
        _rb.velocity = newDirection * speed;
        _lastDirection = newDirection;
    }
}
