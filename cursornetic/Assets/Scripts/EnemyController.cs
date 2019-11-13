using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public ParticleSystem deathParticle;
    public int health = 100;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            if (true) //we need a parameter that tells us if the player is dashing!!
                Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }

    }
}
