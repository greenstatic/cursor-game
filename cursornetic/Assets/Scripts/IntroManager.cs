using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour {

    private VideoPlayer videoPlayer;
    private double length;

    public void Start() {
        if (GlobalState.introPlayed) {
            Destroy(gameObject);
        } else {
            videoPlayer = GetComponent<VideoPlayer>();
            length = videoPlayer.clip.length;

            StartCoroutine(WaitForVideo());
        }
    }

    IEnumerator WaitForVideo() {
        yield return new WaitForSeconds((int)length + 1);
        GlobalState.introPlayed = true;
        Destroy(gameObject);
    }

}
