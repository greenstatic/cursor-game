using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleController : MonoBehaviour {

    public GameObject[] wires;
    public float puzzleTime; //if 0 there is no time limit
    private int buttonsOn;
    private IEnumerator coroutine;
    public GameObject door;

    //public GameObject timeBar;

    public void Update() {
        if (buttonsOn == wires.Length)
            door.SetActive(false);
    }

    public void ButtonPress(GameObject button) {
        buttonsOn++;
        if (IsRightButton(button)) {
            if (puzzleTime != 0)
                if (button == wires[0]) {
                    coroutine = Timer();
                    StartCoroutine(coroutine);
                }
        } else {
            if (coroutine != null)
                StopCoroutine(coroutine);
            buttonsOn = 0;
            foreach (GameObject w in wires)
                w.GetComponent<WireController>().TurnOff();
            door.SetActive(true);
        }
    }

    public bool IsRightButton(GameObject newButton) {
        if (wires[buttonsOn - 1] == newButton)
            return true;
        else
            return false;
    }

    IEnumerator Timer() {
        yield return new WaitForSeconds(puzzleTime);
        buttonsOn = 0;
        foreach (GameObject w in wires) {
            w.GetComponent<WireController>().TurnOff();
        }
        door.SetActive(true);
    }
}
