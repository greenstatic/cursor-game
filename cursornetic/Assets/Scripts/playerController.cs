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

    // Slow Time variables
    public float timeScale;
    public float slowingTimeDuration;
    private float slowSpeed;
    private float initialSpeed;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        camera = GameObject.FindWithTag("MainCamera");
        animator = GetComponent<Animator>();
        rechargeTime = 0;

        // Initialize the health bar - if we change scenes the health is not necessarly 100%.
        healthBar.GetComponent<HealthBar>().SetSize(GlobalState.health / 100);

        // Setting Slow Time parameters before Update() to avoid wired behaviours
        initialSpeed = speed;
        slowSpeed = initialSpeed / timeScale;
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
        if (Time.timeScale > 0) {
            // Dash
            if (Input.GetButton("Dash")) {
                if (rechargeTime <= 0) {
                    isDashing = true;
                    StartCoroutine(Dash());
                    rechargeTime = startRechargeTime;
                }
            }

            // Slow Time
            if (Input.GetButton("SlowTime")) {
                StartCoroutine(SlowTime(timeScale, slowingTimeDuration, slowSpeed, initialSpeed));
            }
        }

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

    IEnumerator SlowTime(float timeScale, float duration, float slowSpeed, float initialSpeed) {
        FindObjectOfType<AudioManager>().Play("SlowTimeIn");
        Time.timeScale = timeScale;
        duration *= timeScale;
        speed = slowSpeed;
        yield return new WaitForSeconds(duration);
        FindObjectOfType<AudioManager>().Play("SlowTimeOut");
        speed = initialSpeed;
        Time.timeScale = 1f;
    }


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

        if (col.CompareTag("Orb")) {
            TakeDamage(20f);
        }

        if (col.CompareTag("ButtonWall")) {
            ButtonController button = col.GetComponent<ButtonController>();
            if (isDashing) {
                button.Toggle();
            }
        }

        if (col.CompareTag("ButtonDoor")) {
            DoorController button = col.GetComponent<DoorController>();
            if (isDashing) {
                button.TurnOn();
            }
        }

        if (col.CompareTag("ButtonWire")) {
            WireController button = col.GetComponent<WireController>();
            if (isDashing) {
                button.TurnOn();
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
        //Destroy(gameObject);
        // TODO - play some game over animation
        GlobalState.health = 100;

        if (SceneManager.GetActiveScene().buildIndex == 1) {
            // Spawn in the middle of the CPU level (level1) if we die in the cpu
            GlobalState.spawnInMiddleCpu = true;
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}