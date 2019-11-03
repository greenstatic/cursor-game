using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem deathParticle;

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Player")) {
            Destroy();
        }
    }

    public void Destroy()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
