using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerGate : MonoBehaviour {
    int levelIndex;
    public Animator animator;
    private GameObject player;
    private GameObject cam;

    private Vector2 cpuLoc1 = new Vector2(-6.75f, 5.43f);

    public void Start() {
        player = GameObject.Find("Player");
        cam = GameObject.Find("Main Camera");
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            StartCoroutine(waitForAnimation());
        }
    }

    IEnumerator waitForAnimation() {
        animator.SetTrigger("TransitionOut");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Choose the level to change based on collider's tag
        int.TryParse(gameObject.tag, out levelIndex);

        if (levelIndex == 1) {
            // Setting location and rotation of player and camera

            if (GlobalState.spawnInMiddleCpu) {
                GlobalState.spawnInMiddleCpu = false;
            } else {
                player.transform.position = cpuLoc1;
                cam.transform.position = cpuLoc1;
            }
        }

        SceneManager.LoadScene(levelIndex);
    }
}
