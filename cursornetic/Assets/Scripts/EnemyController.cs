﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum EnemyType { Unknown, Minion, Brainy, Worm };

public class EnemyController : MonoBehaviour {
    
    public int health = 100;
    private Animator animator;
    private GameObject player;

    private float distToPlayer;


    // Field of view
    public bool enableFOV = true;
    public float FieldOfViewDistance = 5;
    private bool chasePlayer = false;

    // Keep distance
    public bool enableKeepDistance = true;
    public float KeepDistance = 5;

    // Periodic Orb Shooting
    public GameObject orbPrefab;
    public bool PeriodicOrbShootEnable = true;
    public float periodicOrbShootingDelta = 5;
    private float lastPerioidOrbShoot;

    // Death
    public ParticleSystem deathParticle;

    public void Start() {
        player = GameObject.FindWithTag("Player");
        animator = GetComponentInChildren<Animator>();

        if (this.GetEnemyType() == EnemyType.Minion) {
            FOVDistancePathFindingInit();
        }

        if (this.GetEnemyType() == EnemyType.Brainy) {
            KeepDistancePathFindingInit();
        }
            
    }

    public void Update() {

        if (this.GetEnemyType() == EnemyType.Minion) {
            FOVDistancePathFindingUpdate();
        }

        if (this.GetEnemyType() == EnemyType.Brainy) {
            KeepDistancePathFindingUpdate();
            if (PeriodicOrbShootEnable)
                PeriodicOrbShoot(transform.Find("FirePoint"));
        }

        distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        
    }

    //private void OnTriggerEnter2D(Collider2D col) {
    //    if (col.CompareTag("Player")) {
    //        if (true) //we need a parameter that tells us if the player is dashing!!
    //            Die();
    //    }
    //}

    public void Die() {
        //Instantiate(dieParticle, transform.position, Quaternion.identity);

        FindObjectOfType<AudioManager>().Stop("Virus");
        FindObjectOfType<AudioManager>().Play("EnemyDying");

        GetComponent<AIPath>().enabled = false;


        if (this.GetEnemyType() == EnemyType.Minion) {
            GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(waitForAnimation());
        }

        else if (this.GetEnemyType() == EnemyType.Brainy) {
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject);
        }
    }

    IEnumerator waitForAnimation() {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage) {
        health -= damage;
        animator.SetTrigger("IsTakingDamage");
        if (health <= 0) {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            Die();
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

        if (!(FieldOfViewDistance >= distance))
            FindObjectOfType<AudioManager>().Play("Virus");

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

    public EnemyType GetEnemyType() {
        if (this.name.ToLower().StartsWith("enemy")) {
            return EnemyType.Minion;
        }

        if (this.name.ToLower().StartsWith("brainy")) {
            return EnemyType.Brainy;
        }

        return EnemyType.Unknown;
    }

    private void KeepDistancePathFindingUpdate() {
        if (enableKeepDistance) {
            bool isTooClose = KeepDistanceTooClose();
            PathfindingToPlayer(!isTooClose && CanSeePlayer());
        }
    }

    private void KeepDistancePathFindingInit() {
        if (enableKeepDistance) {
            chasePlayer = true;
            PathfindingToPlayer(false);
        }
    }

    private bool KeepDistanceTooClose() {
        if (!player) {
            Debug.LogError("No Player set in EnemyController");
            return false;
        }

        Vector2 playerPos = player.transform.position;
        Vector2 enemyPos = this.transform.position;
        float distance = Vector2.Distance(enemyPos, playerPos);

        return KeepDistance >= distance;
    }

    private void FOVDistancePathFindingUpdate() {
        if (enableFOV) {
            PathfindingToPlayer(CanSeePlayer());
        }
    }
    private void FOVDistancePathFindingInit() {
        if (enableFOV) {
            PathfindingToPlayer(true);
        }
    }

    public void PeriodicOrbShoot(Transform firePoint) {
        Debug.Assert(firePoint);

        if (lastPerioidOrbShoot + periodicOrbShootingDelta < Time.time && CanSeePlayer()) {
            lastPerioidOrbShoot = Time.time;

            GameObject orb = Instantiate(orbPrefab, firePoint.position, firePoint.rotation);
            Orb orbScript = orb.GetComponent<Orb>();
            orbScript.MoveDirection(GetPlayerVector(firePoint));
        }
    }


    private Vector2 GetPlayerVector(Transform origin) {
        Vector2 playerPos = player.transform.position;
        Vector2 enemyPos = origin.transform.position;

        return playerPos - enemyPos;
    }
}
