using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem deathParticle;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            Destroy();
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
