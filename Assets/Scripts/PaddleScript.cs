using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {
    private Rigidbody2D _rb;
    [SerializeField] private int speed = 30;
    [SerializeField] private string axis = "Vertical";
    
    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate() {
        float verticalInput = Input.GetAxisRaw(axis);
        _rb.velocity = new Vector2(0, verticalInput) * speed;
    }
}
