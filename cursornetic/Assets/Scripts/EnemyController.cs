using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour {

    public Transform enemyPos;
    public ParticleSystem deathParticle;
    public int health = 100;

    // Filed of view
    private GameObject player;
    public float FieldOfViewDistance = 5;
    private bool chasePlayer = false;

    // Elude
    public float eludeSpeed = 2;
    public float eludeDist = 3;
    public float startEludeTime;
    private float eludeTime;
    private float distToPlayer;

    public void Start() {
        player = GameObject.FindWithTag("Player");
        PathfindingToPlayer(true);
    }

    public void Update() {
        PathfindingToPlayer(CanSeePlayer());
        
        distToPlayer = Vector3.Distance(enemyPos.transform.position, player.transform.position);

        //Elude(player, distToPlayer);
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

    void Elude(GameObject player, float distance) {

        playerController pc = player.GetComponent<playerController>();
        bool playerIsDashing = pc.isDashing;

        if (distance <= eludeDist && playerIsDashing) {

            eludeTime = startEludeTime;
            float r = Random.Range(0f, 1f);

            Vector2 direction = Vector2.zero;

            if (r >= 0 && r < 0.25f)
                direction = new Vector2(1, 1);
            else if (r >= 0.25f && r < 0.5f)
                direction = new Vector2(1, -1);
            else if (r >= 0.5f && r < 0.75f)
                direction = new Vector2(-1, 1);
            else if (r >= 0.75f && r < 1f)
                direction = new Vector2(-1, -1);

            AIPath script = this.GetComponent<AIPath>();
            script.enabled = false;

            while (eludeTime >= 0) {
                //rb.AddForce(direction * eludeSpeed);
                enemyPos.position = (Vector2) enemyPos.position + direction * new Vector2(0.1f, 0.1f);

                eludeTime -= Time.deltaTime;
            }

            script.enabled = true;
        }
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
