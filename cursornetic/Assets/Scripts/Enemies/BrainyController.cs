using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basically the same class of EnemyController but with some
// important changes:
// -No A* ---> Brainy will move based on BrainyBehaviour.cs
// -No Elude method
public class BrainyController : MonoBehaviour {

    public int health = 300;

    // Filed of view
    private GameObject player;
    public float FieldOfViewDistance = 5;
    private bool chasePlayer = false;

    // Death
    public ParticleSystem deathParticle;

    public void Start() {
        player = GameObject.FindWithTag("Player");
        Movement(true);
    }

    public void Update() {
        Movement(CanSeePlayer());
    }

    public bool CanSeePlayer() {
        if (!player) {
            Debug.LogError("No Player set in EnemyController");
            return false;
        }

        Vector2 playerPos = player.transform.position;
        Vector2 enemyPos = this.transform.position;
        float distance = Vector2.Distance(enemyPos, playerPos);

        return FieldOfViewDistance >= distance;
    }

    public void Movement(bool enable) {
        // chasePlayer is a flag to check if the enable parameter has changed or not.
        // This is a minor optimization so we do not need to call GetComponent constantly.
        if (chasePlayer == enable) {
            return;
        }

        BrainyBehaviour script = this.GetComponent<BrainyBehaviour>();
        if (script == null) {
            Debug.LogError("Failed to find BrainyBehaviour script");
            return;
        }

        script.enabled = enable;
        chasePlayer = enable;
    }

    public void TakeDamage(int damage) {
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        //Instantiate(dieParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
}
