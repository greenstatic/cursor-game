using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChager : MonoBehaviour {

    public Animator animator;
    int levelIndex;
    
    private void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Player")) {

            PlayTransition(); // In case we decide to add more sofisticated animation

            int.TryParse(gameObject.tag, out levelIndex);

            if (levelIndex == 2)
                SceneManager.LoadScene(levelIndex);
            else if (levelIndex == 3)
                SceneManager.LoadScene(levelIndex);
            else if (levelIndex == 4)
                SceneManager.LoadScene(levelIndex);
            else if (levelIndex == 5)
                SceneManager.LoadScene(levelIndex);
            else if (levelIndex == 6)
                SceneManager.LoadScene(levelIndex);
            else if (levelIndex == 7)
                SceneManager.LoadScene(levelIndex);
        }
    }

    public void PlayTransition() {
        animator.SetTrigger("TransitionIn");
    }
}
