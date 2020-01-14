using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;
    public float lifeDuration = 1f;
    public int damage = 100;

    private float lifeTimer;

    // Start is called before the first frame update
    void Start() {
        rb.velocity = transform.right * speed;
        lifeTimer = lifeDuration;
    }

    void FixedUpdate() {
        lifeTimer -= Time.fixedDeltaTime;
        if (lifeTimer <= 0f){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D hit) {
        // Do not interact with player
        if (hit.name == "Player") {
            return;
        }

        // If hit enemy, reduce health of enemy
        if (hit.tag == "Enemy") {
            EnemyController enemy = hit.gameObject.GetComponent<EnemyController>();
            if (enemy != null) {
                enemy.TakeDamage(damage);
            }
        }

        if (hit.CompareTag("ButtonWall")) {
            ButtonController button = hit.GetComponent<ButtonController>();
            button.Toggle();
        }
        
        if (hit.CompareTag("ButtonWire")) {
            WireController button = hit.GetComponent<WireController>();
            if (button.transform.name == "Button Unlock Puzzle1") {
                button.TurnOn();
            }
        }

        if (hit.CompareTag("DialogTrigger")) {
            // Ignore
            return;
        }

        if (hit.CompareTag("Ditch")) {
            // Ignore
            return;
        }

        Destroy(gameObject);
    }
}
