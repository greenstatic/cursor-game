using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainyController : MonoBehaviour {

    public Transform enemyPos;
    public int health = 300;
    private float distToPlayer;

    // Filed of view
    private GameObject player;
    public float FieldOfViewDistance = 5;
    private bool chasePlayer = false;

    // Death
    public ParticleSystem deathParticle;

    public void Start() {
        player = GameObject.FindWithTag("Player");
        PathfindingToPlayer(true);
    }

    public void Update() {
        PathfindingToPlayer(CanSeePlayer());

        distToPlayer = Vector3.Distance(enemyPos.transform.position, player.transform.position);

        //Elude(player, distToPlayer);
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
