using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    int levelIndex;
    GameObject player, cam;
    public Animator animator;

    private Vector2 cpuLoc1 = new Vector2(-6.685872f, 6.902206f);
    //private Vector2 cpuLoc2 = new Vector2(-12.69f, 13.02f);

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Player")) {

            PlayTransition(); // (In case we decide to add more sofisticated animation)

            // Choose the level to change based on collider's tag
            int.TryParse(gameObject.tag, out levelIndex);
            SceneManager.LoadScene(levelIndex);
        }
    }

    private void OnLevelWasLoadedCustom(Scene scene, LoadSceneMode mode) {

        Debug.Log("shit");
        levelIndex = SceneManager.GetActiveScene().buildIndex;

        // Retrieving camera and player objects in cpu scene
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");

        if (levelIndex == 1) {

            // Setting location and rotation of player and camera
            player.transform.position = cpuLoc1;
            player.GetComponent<Rigidbody2D>().rotation = 90;
            cam.transform.position = cpuLoc1;

        } else if (levelIndex == 2) {

        }
    }

    public void onEnable() {
        SceneManager.sceneLoaded += OnLevelWasLoadedCustom;
    }

    public void onDisable() {
        SceneManager.sceneLoaded -= OnLevelWasLoadedCustom;
    }

    

    public void PlayTransition() {

        animator.SetTrigger("TransitionIn");
    }
}
