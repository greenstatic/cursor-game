using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleController : MonoBehaviour {

    private WireController[] wires;
    public int count = 0;

    public void Start() {
        GameObject[] button = GameObject.FindGameObjectsWithTag("ButtonWire");
        for (int i=0; i<button.Length; i++) {
            wires[i] = button[i].GetComponent<WireController>();
        }
    }

    public void Update() {
        if (AllWiresOn(0)) {
            return;
        }
    }

    //WIP
    public bool AllWiresOn(int index) {
        if (wires[index].state == true) {
            if (index < wires.Length)
                return wires[index].state;
            else
                return AllWiresOn(index++);
        }
    }
}
