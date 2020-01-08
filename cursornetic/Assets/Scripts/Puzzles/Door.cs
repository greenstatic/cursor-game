using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [HideInInspector]
    public bool open;

    public void Open() {
        gameObject.SetActive(false);
    }

    public void Close() {
        gameObject.SetActive(true);
    }


}
