using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public static int PauseMenuSceneBuildIndex;
    public static PauseMenuActivate pauseMenuActiveScript;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Cursor.visible = false;
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
        Debug.Log("Resuming game");
        GameIsPaused = false;
        Time.timeScale = 1f;
        pauseMenuActiveScript.enabled = true;
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByBuildIndex(PauseMenuSceneBuildIndex));
    }

    public static void Pause(PauseMenuActivate p) {
        PauseMenuSceneBuildIndex = SceneManager.sceneCountInBuildSettings - 1; 
        
        pauseMenuActiveScript = p;
        pauseMenuActiveScript.enabled = false;
        Debug.Log("Pausing game");
        Time.timeScale = 0f;
        GameIsPaused = true;
        SceneManager.LoadSceneAsync(PauseMenuSceneBuildIndex, LoadSceneMode.Additive);
    }
}
