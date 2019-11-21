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
    public bool isDashing;
    private Vector2 direction;
    public float startRechargeTime;
    private float rechargeTime;
    public GameObject dashEffect;
    //public Animator camAnim;

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
        rb.velocity = moveInput * speed * Time.deltaTime;

        // Rotation
        if (moveInput != Vector2.zero) {
            angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }

        // Dash
        if (Input.GetButton("Dash")) {
            if (rechargeTime <= 0) {
                isDashing = true;
                StartCoroutine(Dash());
                rechargeTime = startRechargeTime;
            }
        }

        /* // Roll
        if (Input.GetButton("Roll")) {
            Roll();
        }
        */

        rechargeTime -= Time.fixedDeltaTime;
    }

    IEnumerator Dash() {
        dashTime = startDashTime;

        // Play Dash Effect
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        direction = new Vector2(transform.right.x, transform.right.y);

        while (isDashing) {

            if (dashTime <= 0) {
                yield return new WaitForSeconds(0.3f);
                isDashing = false;
                dashTime = startDashTime;
            } else {
                dashTime -= Time.deltaTime;
                //camAnim.SetTrigger("Shake");
                rb.AddForce(direction * dashSpeed);
            }
        }
    }

    /*
    public void Roll() {
        
    }
    */


    public void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Enemy")) {
            EnemyController enemy = col.GetComponent<EnemyController>();

            if (enemy != null) {
                if (isDashing) {
                    Debug.Log("Enemy has died.");
                    enemy.Die();
                }
                else {
                    Die();
                }
            }
        }
    }

    public void Die() {
        // TODO - implement die mechanics
        Debug.Log("You died!");
        //Destroy(gameObject);
    }
}