using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour {

    private Rigidbody2D rb;
    private GameObject camera;
    private Animator animator;

    // Health variables
    public GameObject healthBar;

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
        camera = GameObject.FindWithTag("MainCamera");
        animator = GetComponent<Animator>();
        rechargeTime = 0;

        // Initialize the health bar - if we change scenes the health is not necessarly 100%.
        healthBar.GetComponent<HealthBar>().SetSize(GlobalState.health / 100);
    }

    void Update() {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    void FixedUpdate() {

        direction = new Vector2(transform.right.x, transform.right.y);

        // Translation
        rb.velocity = moveInput * speed * Time.deltaTime;

        animator.SetFloat("PlayerVelocity", rb.velocity.sqrMagnitude);

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

        animator.SetTrigger("isDashing");
        yield return new WaitForSeconds(0.3f);

        // Play Dash Effect
        Instantiate(dashEffect, transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Dash");

        while (isDashing) {

            if (dashTime <= 0) {
                yield return new WaitForSeconds(0.3f);
                isDashing = false;
                dashTime = startDashTime;
            } else {
                dashTime -= Time.deltaTime;
                camera.GetComponent<cameraController>().ShakeCamera();
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
                    TakeDamage(10f);
                }
            }
        }

        if (col.CompareTag("ButtonWall")) {
            ButtonController button = col.GetComponent<ButtonController>();
            if (isDashing) {
                button.Toggle();
            }
        }

        if (col.CompareTag("ButtonWall")) {
            ButtonController button = col.GetComponent<ButtonController>();
            if (isDashing) {
                //sblocca porte
            }
        }
    }

    public void TakeDamage(float damage) {
        GlobalState.health -= damage;
        healthBar.GetComponent<HealthBar>().SetSize(GlobalState.health / 100);
        if (GlobalState.health <= 0)
            GameOver();
        animator.SetTrigger("TakingDamge");
        FindObjectOfType<AudioManager>().Play("Damage");
        transform.position -= (Vector3) direction*2;

    }

    public void GameOver() {
        Destroy(gameObject);
        // play some game over animation
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}