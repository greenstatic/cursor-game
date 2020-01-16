using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Add this script to each scene you wish the PauseMenu will be available

public class PauseMenuActivate : MonoBehaviour
{
    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GlobalState.hasWon) {
                GlobalState.Reset();
                SceneManager.LoadScene(0);
                return;
            }

            PauseMenu.Pause(this);
        }
    }

}
