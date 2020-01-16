using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject WinnerGameObject;

    public void Start() {
        if (GlobalState.hasWon) {
            WinScreenActivate();
        }
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameObject credits = GameObject.Find("Credits");
            if (credits != null) {
                credits.SetActive(false);
            }

            GameObject mainMenu = GameObject.Find("MainMenu");
            if (mainMenu != null) {
                mainMenu.SetActive(true);
            }
        }
    }

    public void NewGame() {
        Debug.Log("Loading level1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("Quiting game");
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void WinScreenActivate() {
        GameObject mainMenu = GameObject.Find("MainMenu");
        if (mainMenu != null) {
            mainMenu.SetActive(false);
        }

        if (WinnerGameObject != null)
            WinnerGameObject.SetActive(true);
    }

    public void WinScreenDeactivate() {
        if (WinnerGameObject != null)
            WinnerGameObject.SetActive(false);

        GameObject mainMenu = GameObject.Find("MainMenu");
        if (mainMenu != null) {
            mainMenu.SetActive(true);
        }
    }

}
