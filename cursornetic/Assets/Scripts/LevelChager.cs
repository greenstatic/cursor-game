using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChager : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;


    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButton(1))
            TransitionToLevel(1);
    }

    public void TransitionToLevel (int levelIndex) {

        levelToLoad = levelIndex;
        animator.SetTrigger("TransitionIn");
    }

    public void OnTransitionComplete () {

        SceneManager.LoadScene(levelToLoad);
    }
}
