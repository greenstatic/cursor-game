using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem deathParticle;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            if (true) //we need a parameter that tells us if the player is dashing!!
                Destroy();
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
