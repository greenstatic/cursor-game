using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb;
    public float lifeDuration = 3f;

    private float lifeTimer;

    void Start() {
        lifeTimer = lifeDuration;
    }

    void FixedUpdate() {
        lifeTimer -= Time.fixedDeltaTime;
        if (lifeTimer <= 0f) {
            Destroy(gameObject);
        }
    }

    public void MoveDirection(Vector2 vec) {
        rb.velocity = vec * speed;
    }

    void OnTriggerEnter2D(Collider2D hit) {
        if (hit.name == "Player") {
            // Use default
        }

        if (hit.tag == "Enemy") {
            EnemyController enemy = hit.gameObject.GetComponent<EnemyController>();
            if (enemy != null && enemy.GetEnemyType() == EnemyType.Worm) {
                // If the laser hit's a Worm enemey, ignore it.
                return;
            }
        }

        if (hit.tag == "ForceField")
            return;

        if (hit.tag == "DialogTrigger")
            return;

        Debug.Log(hit.name);

        Destroy(gameObject);
    }
}
