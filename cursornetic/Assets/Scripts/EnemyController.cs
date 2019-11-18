using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour {

    public ParticleSystem deathParticle;
    public int health = 100;

    // Filed of view
    public GameObject Player;
    public float FieldOfViewDistance = 100;
    private bool chasePlayer;

    public void Start() {
        PathfindingToPlayer(true);
        chasePlayer = true;
    }
    public void Update() {
        PathfindingToPlayer(CanSeePlayer());
    }

    //private void OnTriggerEnter2D(Collider2D col) {
    //    if (col.CompareTag("Player")) {
    //        if (true) //we need a parameter that tells us if the player is dashing!!
    //            Die();
    //    }
    //}

    public void Die() {
        Destroy(gameObject);
    }


    public void TakeDamage(int damage) {
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    public bool CanSeePlayer() {
        if (!Player) {
            Debug.LogError("No Player set in EnemyController");
            return false;
        }


        Vector2 playerPos = Player.transform.position;
        Vector2 enemyPos = this.transform.position;
        float distance = Vector2.Distance(enemyPos, playerPos);

        bool d = FieldOfViewDistance >= distance;
        return d;
    }
    
    // Enables or disables the AIPath script which causes the enemy to follow the player
    public void PathfindingToPlayer(bool enable) {
        // chasePlayer is a flag to check if the enable parameter has changed or not.
        // This is a minor optimization so we do not need to call GetComponent constantly.
        if (chasePlayer == enable) {
            return;
        }

        AIPath script = this.GetComponent<AIPath>();
        if (script == null) {
            Debug.LogError("Failed to find AIPath script");
            return;
        }

        script.enabled = enable;
        chasePlayer = enable;
    }
}
