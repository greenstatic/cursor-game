using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChangerGate : MonoBehaviour {
    int levelIndex;
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            PlayTransition(); // (In case we decide to add more sofisticated animation)

            // Choose the level to change based on collider's tag
            int.TryParse(gameObject.tag, out levelIndex);
            SceneManager.LoadScene(levelIndex);
        }
    }

    public void PlayTransition() {
        animator.SetTrigger("TransitionIn");
    }
}
