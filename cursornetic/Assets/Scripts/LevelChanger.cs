using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {
    GameObject player, cam;

    private Vector2 cpuLocNewGame = new Vector2(0.06f, -1.48f);
    private Vector2 cpuLoc1 = new Vector2(-6.685872f, 6.902206f);
    //private Vector2 cpuLoc2 = new Vector2(-12.69f, 13.02f);

    private void OnLevelWasLoadedCustom(Scene scene, LoadSceneMode mode) {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;

        // Retrieving camera and player objects in cpu scene
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");

        if (levelIndex == 1) {
            // Setting location and rotation of player and camera

            if (GlobalState.spawnInMiddleCpu) {
                player.transform.position = cpuLocNewGame;
                cam.transform.position = cpuLocNewGame;
                GlobalState.spawnInMiddleCpu = false;
            }
            else {
                player.transform.position = cpuLoc1;
                cam.transform.position = cpuLoc1;
            }

            player.GetComponent<Rigidbody2D>().rotation = 90;


        }
        else if (levelIndex == 2) {
            // TODO
        }
    }

    public void OnEnable() {
        SceneManager.sceneLoaded += OnLevelWasLoadedCustom;
    }

    public void OnDisable() {
        SceneManager.sceneLoaded -= OnLevelWasLoadedCustom;
    }
}
