using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

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

    // Periodic Laser Shooting
    public GameObject laserPrefab;
    public bool PeriodicLaserShootEnable = true;
    public float periodicLaserShootingDelta = 5;
    private float lastPeriodicLaserShoot;
    //public GameObject firePointWorm;

    // Death
    public ParticleSystem deathParticle;

    //Dialogs (just for bosses)
    public GameObject dialog;

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

        if (this.GetEnemyType() == EnemyType.Worm) {
            if (PeriodicLaserShootEnable)
                PeriodicLaserShoot(transform.Find("FirePointWorm"));
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

        if (GetComponent<AIPath>() != null) {
            GetComponent<AIPath>().enabled = false;
        }


        if (this.GetEnemyType() == EnemyType.Minion) {
            GetComponent<CapsuleCollider2D>().enabled = false;
            StartCoroutine(waitForAnimation());
        } else if (this.GetEnemyType() == EnemyType.Brainy) {
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject);
        } else if (this.GetEnemyType() == EnemyType.Worm) {
            GetComponent<SpriteRenderer>().enabled = false;

            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            animator = transform.GetChild(1).gameObject.GetComponent<Animator>();

            StartCoroutine(waitForAnimation());
        }

        if (this.name == "BrainyBoss") {
            GlobalState.brainyAlive = false;

            if (!GlobalState.wormAlive) {
                // Game over
                GlobalState.hasWon = true;
                SceneManager.LoadScene(0);
            }

            // Recharge life
            GlobalState.health = 100;

            // Delete eletric walls in level
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("ElectricWall")) {
                Destroy(go);
            }

            // Win dialog
            dialog.GetComponent<DialogTrigger>().TriggerDialogue();
        }
    }

    IEnumerator waitForAnimation() {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length-0.1f);
        if (this.name == "WormBoss") {
            GlobalState.wormAlive = false;

            // Win dialog
            dialog.GetComponent<DialogTrigger>().TriggerDialogue();

            // Recharge life
            GlobalState.health = 100;

            if (!GlobalState.brainyAlive) {
                // Game over
                GlobalState.hasWon = true;
                SceneManager.LoadScene(0);
            }
        }
        Destroy(gameObject);
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (GetEnemyType() == EnemyType.Brainy || GetEnemyType() == EnemyType.Worm) {
            animator.SetTrigger("IsTakingDamage");
        }
        if (health <= 0) {
            //Instantiate(deathParticle, transform.position, Quaternion.identity);
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

        if (this.name.ToLower().StartsWith("worm")) {
            return EnemyType.Worm;
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

    public void PeriodicLaserShoot(Transform firePoint) {
        Debug.Assert(firePoint);
        
        if (lastPeriodicLaserShoot + periodicLaserShootingDelta < Time.time && CanSeePlayer()) {
            lastPeriodicLaserShoot = Time.time;

            Vector2 vecToPlayer = GetPlayerVector(firePoint);
            float angle = Mathf.Asin(vecToPlayer.y / vecToPlayer.magnitude) * Mathf.Rad2Deg;

            // Calculates the rotation of the laser to point at the player
            Vector2 dir = (firePoint.position - player.transform.position);
            if (dir.x > 0) {
                if (dir.y > 0)
                    angle = 180 + Mathf.Abs(angle);
                else
                    angle = 180 - angle;
            }

            GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.Euler(0,0,angle));

            Laser laserScript = laser.GetComponent<Laser>();
            laserScript.MoveDirection(vecToPlayer);
        }
    }


    private Vector2 GetPlayerVector(Transform origin) {
        Vector2 playerPos = player.transform.position;
        Vector2 enemyPos = origin.transform.position;

        return playerPos - enemyPos;
    }
}
