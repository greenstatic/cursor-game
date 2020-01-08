using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public static int PauseMenuSceneBuildIndex;
    public static PauseMenuActivate pauseMenuActiveScript;

    private static float previousTimeScale = 0f;

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Resume();
        }
    }

    public void ResumeGame() {
        Resume();
    }

    public void QuitGame() {
        Application.Quit();
    }

    public static void Resume() {
        mouseCursor.Disable();
        GameIsPaused = false;
        
        Time.timeScale = previousTimeScale;

        pauseMenuActiveScript.enabled = true;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(PauseMenuSceneBuildIndex));
    }

    public static void Pause(PauseMenuActivate p) {
        PauseMenuSceneBuildIndex = SceneManager.sceneCountInBuildSettings - 1;
        mouseCursor.Enable();
        pauseMenuActiveScript = p;
        pauseMenuActiveScript.enabled = false;

        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        GameIsPaused = true;
        SceneManager.LoadSceneAsync(PauseMenuSceneBuildIndex, LoadSceneMode.Additive);
    }
}
