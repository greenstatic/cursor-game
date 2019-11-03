using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    private Rigidbody2D rb;
    
    // Movements variables
    public float speed;
    public float angle;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    // Dash Variables
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private bool isFinished;
    private Vector2 direction;
    public float startRechargeTime;
    private float rechargeTime;
    public GameObject dashEffect;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rechargeTime = 0;
    }
    
    void Update() {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    void FixedUpdate() {
        
        // Translation
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        // Rotation
        if (moveInput != Vector2.zero) {
            angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }

        // Dash
        if (Input.GetButton("Dash")) {
            if (rechargeTime <= 0) {
                isFinished = true;
                Dash();
                rechargeTime = startRechargeTime;
            }
        }
        rechargeTime -= Time.fixedDeltaTime;

        /* // Roll
        if (Input.GetButton("Roll")) {
            Roll();
        }
        */
    }

    public void Dash() {
        dashTime = startDashTime;

        // Play Dash Effect
        Instantiate(dashEffect, transform.position, Quaternion.identity);

        while (true) {
            if (isFinished) {
                direction = new Vector2(transform.right.x, transform.right.y);
                isFinished = false;
            } else {
                if (dashTime <= 0) {
                    isFinished = true;
                    dashTime = startDashTime;
                    break;
                } else {
                    dashTime -= Time.deltaTime;
                    //camAnim.setTrigger("Shake");
                    rb.MovePosition(rb.position + direction * dashSpeed * Time.fixedDeltaTime );
                }
            }
        }
    }

    /*
    public void Roll() {
        
    }
    */
}